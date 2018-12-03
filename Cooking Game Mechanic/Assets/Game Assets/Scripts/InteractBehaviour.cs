using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractBehaviour : MonoBehaviour {

    public GameObject textObject, recipeBookImage;
    public float interactDistance = 10f;
    public GameObject[] meals;

    private Text text;
    private Ray ray;
    private RaycastHit hit;
    private bool holding, bookSetUp, bookOpen;
    private GameObject pickUp, collision, mealPrefab;
    private RecipeBook recipeBook;
    

	// Use this for initialization
	void Start () {
        holding = false;
        text = textObject.GetComponent<Text>();
        recipeBook = new RecipeBook();

        recipeBookImage.GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, -(Screen.height / 2), 0);
    }
	
	// Update is called once per frame
	void Update () {

        TextUpdate();

        RayShoot();

        RecipeBookManager();

        if (Input.GetKeyDown(KeyCode.E) && collision != null)
        {
            if (collision.tag == "PickUp")
            {
                pickUp = collision;
                holding = !holding;
                collision.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }         
            else if (collision.tag == "Crate")
            {
                pickUp = Instantiate(collision.GetComponent<CrateObject>().crateObject);
                pickUp.name = pickUp.name.Replace("(Clone)", "");
                holding = !holding;
            }
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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        
        if (holding)
        {
            pickUp.GetComponent<Transform>().position = transform.position + (transform.forward * 3);
        }
        else
        {
            pickUp = null;
        }
		
	}

    private void TextUpdate()
    {
        if (collision != null && collision.tag != "Untagged")
        {
            switch (collision.tag)
            {
                case ("PickUp"):
                    if (holding) text.text = "Press \"E\" to Drop " + collision.name;
                    if (!holding) text.text = "Press \"E\" to Pickup " + collision.name;
                    break;
                case ("Crate"):
                    text.text = "Press \"E\" to Dispense " + collision.GetComponent<CrateObject>().crateFood.name;
                    break;
                case ("Pot"):
                    PotController controller = collision.GetComponent<PotController>();
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
                    else
                    {
                        text.text = "Empty Pot! Add Ingredients.";
                    }
                    break;
                case ("ServingTable"):
                    text.text = "Drop Item onto Serving Table to Serve.";
                    break;
                case ("Bin"):
                    text.text = "Drop Item in Bin to Destroy";
                    break;
            }        

            textObject.SetActive(true);
        }
        else
        {
            textObject.SetActive(false);
        }
    }

    private void RayShoot()
    {
        ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * 4, Color.red, 0);

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            collision = hit.collider.gameObject;
        }
        else
        {
            collision = null;
        }
    }

    private void RecipeBookManager()
    {
        RectTransform component = recipeBookImage.GetComponent<RectTransform>();

        if (!bookSetUp)
        {
            component.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);

            component.position = new Vector3(Screen.width / 2, -(Screen.height / 2), 0);
            recipeBookImage.SetActive(true);
            bookSetUp = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!bookOpen)
            {
                Time.timeScale = 0f;
                bookOpen = true;
            }
            else
            {
                Time.timeScale = 1f;
                bookOpen = false;
            }
        }

        if (bookOpen && component.position.y <= Screen.height / 2)
        {
            component.Translate(new Vector3(0, 20, 0));
        }
        else if (!bookOpen && component.position.y >= -Screen.height / 2)
        {
            component.Translate(new Vector3(0, -20, 0));
        }
    }

    public void DestroyPickup(GameObject collider)
    {
        Destroy(collider);
        pickUp = null;
        holding = false;
    }
}
