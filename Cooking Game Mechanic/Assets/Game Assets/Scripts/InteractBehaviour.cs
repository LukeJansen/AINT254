using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Class to handle the players interactions with the world and its objects.
public class InteractBehaviour : MonoBehaviour {

    // Variables to hold game objects.
    public GameObject textObject, recipeBookImage;
    // Variable to hold the maximum distance you can interact with objects from.
    public float interactDistance = 10f;
    // Variable to hold a list of meals.
    public GameObject[] meals;

    // Variable to hold the text component of the main UI.
    private Text text;
    // Variable to hold the raycast used to detect objects in front of the player.
    private Ray ray;
    // Variable to hold the information about the raycast and what it hits.
    private RaycastHit hit;
    // Variables to hold boolean values that control certain functions.
    private bool holding, bookSetUp, bookOpen;
    // Variables to reference game objects in the world.
    private GameObject pickUp, collision, mealPrefab;
    // Variable to reference the recipe book.
    private RecipeBook recipeBook;
    

	void Start () {
        // Set the boolean value.
        holding = false;
        // Grab the text component from the same object.
        text = textObject.GetComponent<Text>();
        // Creates a new recipe book.
        recipeBook = new RecipeBook();

        // Scale the recipe book to the users screen.
        recipeBookImage.GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, -(Screen.height / 2), 0);        
    }
	
	void Update () {

        // Calls a function to update the UI Text.
        TextUpdate();

        // Calls a function to shoot the raycast.
        RayShoot();

        // Calls a function to manage the recipe book.
        RecipeBookManager();

        // Checks if you press the interact key and that you are looking at an object.
        if (Input.GetKeyDown(KeyCode.E) && collision != null)
        {
            // Checks if you are holding an item and if you are drops it.
            if (holding)
            {
                holding = !holding;
            }
            // Checks if the object you are looking at is a PickUp and if it is picks it up.
            else if (collision.tag == "PickUp")
            {
                pickUp = collision;
                holding = !holding;
                collision.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }         
            // Checks if the object you are looking at is a Crate and if it is takes an item from it.
            else if (collision.tag == "Crate")
            {
                pickUp = Instantiate(collision.GetComponent<CrateObject>().crateObject);
                pickUp.name = pickUp.name.Replace("(Clone)", "");
                holding = !holding;
            }
            // Checks if the object you are looking at is a Pot and if so takes the meal in its current state from it.
            else if (collision.tag == "Pot")
            {
                PotController controller = collision.GetComponent<PotController>();

                int index = controller.currentRecipe.meal;
                int state = Mathf.FloorToInt(controller.GetCookingTime());

                mealPrefab = meals[index - recipeBook.foodCount];

                pickUp = Instantiate(mealPrefab);
                pickUp.GetComponent<MealBehaviour>().SetMealBehaviour(index, state);
                holding = !holding;

                controller.Clear();
            }
        }
        // Checks if the current key down is Escape and if so returns to menu.
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        // Checks if you are holding and object and if so moves the object infront of the player.
        if (holding)
        {
            pickUp.GetComponent<Transform>().position = transform.position + (transform.forward * 5);
        }
        // Otherwise clears the pickUp variable.
        else
        {
            pickUp = null;
        }
		
	}

    // Function to handle updating the UI Text.
    private void TextUpdate()
    {
        // Checks if holding an item and if so defaults to showing drop message.
        if (holding)
        {
            text.text = "Press \"E\" to Drop " + pickUp.name;
        }
        else
        {
            // Checks if there is an object being looked at and that it has a tag.
            if (collision != null && collision.tag != "Untagged")
            {
                // Checks through each of the available tags:
                switch (collision.tag)
                {
                    // Checks if the tag is PickUp if so shows pickup message.
                    case ("PickUp"):
                        if (!holding) text.text = "Press \"E\" to Pickup " + collision.name;
                        break;
                    // Checks if the tag is Crate if so shows dispense message.
                    case ("Crate"):
                        text.text = "Press \"E\" to Dispense " + collision.GetComponent<CrateObject>().crateFood.name;
                        break;
                    // Checks if the tag is Pot is so shows a message depending on the state of the pot.
                    case ("Pot"):
                        PotController controller = collision.GetComponent<PotController>();
                        // Checks if the pot is currently cooking if so it shows the collect message with meal name and current state.
                        if (controller.Cooking)
                        {
                            string message = "Press \"E\" to Collect ";

                            if (controller.GetCookingTime() <= 1)
                            {
                                message += recipeBook.foodItems[controller.currentRecipe.meal].name + " (Under Cooked)";
                            }
                            else if (controller.GetCookingTime() <= 2)
                            {
                                message += recipeBook.foodItems[controller.currentRecipe.meal].name + " (Cooked)";
                            }
                            else if (controller.GetCookingTime() <= 3)
                            {
                                message += recipeBook.foodItems[controller.currentRecipe.meal].name + " (Over Cooked)";
                            }
                            else
                            {
                                message += "Burnt Food";
                            }

                            text.text = message;
                        }
                        // Otherwise shows the no recipe messsage.
                        else
                        {
                            text.text = "No Recipe Matched! Add Ingredients.";
                        }
                        break;
                    // Checks if the tag is ServingTable and shows the table message.
                    case ("ServingTable"):
                        text.text = "Drop Item onto Serving Table to Serve.";
                        break;
                }
                // Show the text object.
                textObject.SetActive(true);
            }
            // Hides the text object.
            else
            {
                textObject.SetActive(false);
            }
        }
    }

    // Function to manage ray shooting.
    private void RayShoot()
    {
        // Create new raycast in the direction the player is looking.
        ray = new Ray(transform.position, transform.forward);

        // If the raycasts hits an object save its collision into the collision variable.
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            collision = hit.collider.gameObject;
        }
        // Otherwise reset the collision variable.
        else
        {
            collision = null;
        }
    }

    // Function to manage the recipe book on screen.
    private void RecipeBookManager()
    {
        RectTransform component = recipeBookImage.GetComponent<RectTransform>();

        // Checks if the book has not yet been setup
        if (!bookSetUp)
        {
            // Resizes book and places it in the correct place on screen.
            component.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);

            component.position = new Vector3(Screen.width / 2, -(Screen.height / 2), 0);

            // Shows book.
            recipeBookImage.SetActive(true);
            // Marks book as set up.
            bookSetUp = true;
        }

        // Checks if player presses the book button.
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Checks if the book is closed if so opens it and freezes the game.
            if (!bookOpen)
            {
                Time.timeScale = 0f;
                bookOpen = true;
            }
            // Checks if the book is open if so closes it and resumes the game.
            else
            {
                Time.timeScale = 1f;
                bookOpen = false;
            }
        }

        // Checks if the book is open and not in the right position if so moves it into place.
        if (bookOpen && component.position.y <= Screen.height / 2)
        {
            component.Translate(new Vector3(0, 20, 0));
        }
        // Checks if the book is close and not in the right position if so moves it into place.
        else if (!bookOpen && component.position.y >= -Screen.height / 2)
        {
            component.Translate(new Vector3(0, -20, 0));
        }
    }

    // Function to destroy the pickup gameobject and clear the variables related to it.
    public void DestroyPickup(GameObject collider)
    {
        Destroy(collider);
        pickUp = null;
        holding = false;
    }
}
