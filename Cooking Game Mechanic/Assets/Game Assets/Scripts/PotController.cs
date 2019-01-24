using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to control the pot object.
public class PotController : MonoBehaviour {
   
    // Variable to reference the interaction behavior of the player.
    public InteractBehaviour controller;
    // Variable to reference the text prefab for the pot.
    public GameObject textPrefab;
    // Variable to reference the pot text after it is created.
    public GameObject potText;
    // Variable to reference the pot's particle system.
    public ParticleSystem particle;

    // Variable to reference the two image objects above the pot.
    public GameObject baseImage, cookingImage;
    // Variable to hold all of the sprites for the two image objects.
    public Sprite cookingBase, cookingGood, cookingBad, cookingBurnt;
    // Variable to hold the current recipe being cooked.
    public Recipe currentRecipe;

    // Variable to reference the recipe book.
    private RecipeBook recipeBook;
    // Variable to hold the current contents of the pot.
    private Dictionary<string, int> contents;
    // Variables to reference the image components of the image objects.
    private Image baseImageComponent, cookingImageComponent;
    // Variables to hold the current states of the pot.
    private bool cooking, empty;
    // Variables to control timings of cooking.
    private float cookingTime, startTime;
        
    void Start () {
        // Set up the state of the pot.
        cooking = false;
        empty = true;

        // Initialise contents variable.
        contents = new Dictionary<string, int>();
        
        // Grab the interaction behaviour from the player.
        controller = Camera.main.GetComponent<InteractBehaviour>();
        // Grab the image componenets from the image objects.
        baseImageComponent = baseImage.GetComponent<Image>();
        cookingImageComponent = cookingImage.GetComponent<Image>();
        // Create a recipe book.
        recipeBook = new RecipeBook();
	}
	
	void Update () {
        // Call Update Functions.
        CookingUpdate();
        CookingImageUpdate();
	}

    // Function to handle updates of cooking.
    private void CookingUpdate()
    {
        // Checks if the pot is not cooking but contains ingredients.
        if (!cooking && !empty)
        {
            // Checks the recipe book for a matching recipe.
            int result = recipeBook.RecipeCheck(contents);
            // Checks if the recipe is valid.
            if (result != 100)
            {
                // Starts cooking.
                cooking = true;
                currentRecipe = recipeBook.recipeItems[result];
                cookingTime = recipeBook.recipeItems[result].cookingTime;
                startTime = Time.time;
            }
        }

        // Manages the particle system to play when cooking.
        if (Cooking)
        {
            if (!particle.isPlaying) particle.Play();
        }
        else
        {
            if (particle.isPlaying) particle.Stop();
        }
    }

    // Function to handle the cooking indicator above the pot.
    private void CookingImageUpdate()
    {
        // If cooking show the indicators otherwise hide them.
        baseImage.SetActive(cooking);
        cookingImage.SetActive(cooking);

        // Checks if the pot is cooking.
        if (cooking)
        {
            // Makes the indicators follow the player.
            baseImage.transform.LookAt(Camera.main.transform);
            cookingImage.transform.LookAt(Camera.main.transform);

            // Calculates the time since cooking started.
            float timeElapsed = Time.time - startTime;

            // Checks if the food is undercooked and updates the indicators.
            if (timeElapsed <= cookingTime)
            {
                baseImageComponent.sprite = cookingBase;
                cookingImageComponent.sprite = cookingGood;
                cookingImageComponent.fillAmount = (timeElapsed / cookingTime);
            }
            // Checkes if the food is cooked and updates the indicators.
            else if ((timeElapsed - cookingTime) <= cookingTime){
                baseImageComponent.sprite = cookingGood;
                cookingImageComponent.sprite = cookingBad;
                cookingImageComponent.fillAmount = ((timeElapsed - cookingTime) / cookingTime);
            }
            // Checks if the food is overcooked and updates the indicators.
            else if ((timeElapsed - (cookingTime * 2)) <= cookingTime)
            {
                baseImageComponent.sprite = cookingBad;
                cookingImageComponent.sprite = cookingBurnt;
                cookingImageComponent.fillAmount = ((timeElapsed - (cookingTime * 2)) / cookingTime);
            }
        }


    }

    // Event that is called when an object collides with the pot.
    private void OnCollisionEnter(Collision collision)
    {
        // Checks if not cooking and if the collision object is a pickup.
        if (!cooking && collision.gameObject.tag == "PickUp")
        {
            // Grabs the food objects from the collision.
            Food food = collision.gameObject.GetComponent<FoodBehaviour>().foodObject;

            // Adds the food to contents.
            if (contents.ContainsKey(food.name))
            {
                contents[food.name] += 1;
            }
            else
            {
                contents.Add(food.name, 1);
                empty = false;
            }

            // Calls the show item function.
            ShowItem(food.name);

            // Destroys the item.
            controller.DestroyPickup(collision.gameObject);
        }
    }

    // Function to show text stating what has been added to the pot.
    private void ShowItem(string itemName)
    {
        potText = Instantiate(textPrefab, transform);
        potText.GetComponent<PotTextBehaviour>().objectName = itemName;
    }

    // Function to reset the pot to its default state.
    public void Clear()
    {
        cooking = false;
        empty = true;
        currentRecipe = null;
        contents = new Dictionary<string, int>();
    }

    public bool Cooking
    {
        get
        {
            return cooking;
        }
    }

    public float GetCookingTime()
    {
        return (Time.time - startTime) / cookingTime;
    }
}
