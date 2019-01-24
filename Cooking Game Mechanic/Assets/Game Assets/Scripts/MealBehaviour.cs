using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to handle the behaviours of meal objects in the world.
public class MealBehaviour : MonoBehaviour {

    // Variable to hold the food index of the meal.
    public int foodIndex;
    // Variable to hold the state of the meal;
    public int state;
    // Varibles to hold the color values of the different states.
    public Color under, good, bad, burnt;
    // Variable to hold the food object.
    public Food food;
    
    //Variable to reference the recipe book.
    private RecipeBook recipeBook;

	void Start () {

        // Define colours.
        under = new Color(0.66f, 0.66f, 0.66f);
        good = new Color(0, 0.66f, 0);
        bad = new Color(1f, 0.5f, 0);
        burnt = new Color(0.66f, 0, 0);

        // Get food object from the index.
        food = recipeBook.foodItems[foodIndex];
    }
	
    // Function to setup the meal behaviour.
    public void SetMealBehaviour(int foodIndex, int state)
    {

        // Sets the food index.
        this.foodIndex = foodIndex;
        // Sets the food state.
        // 0 - Good, 1 - Bad, 2 - Burnt
        this.state = state;

        // Creates a recipe book.
        recipeBook = new RecipeBook();
        // Gets the food object from the index.
        food = recipeBook.foodItems[foodIndex];

        // Calls the start function.
        Start();
        // Calls the setup function.
        SetUp();
    }

    // Function to setup the meal object.
    private void SetUp()
    {
        // Rename the game object to match the meal name.
        gameObject.name = recipeBook.foodItems[foodIndex].name;

        // Checks the state of the meal and sets the colour of the material and name to match it.
        switch (state)
        {
            case 0:
                GetComponent<MeshRenderer>().material.color = under;
                gameObject.name += " (Under Cooked)";
                break;

            case 1:
                GetComponent<MeshRenderer>().material.color = good;
                break;

            case 2:
                GetComponent<MeshRenderer>().material.color = bad;
                gameObject.name += " (Over Cooked)";
                break;

            default:
                GetComponent<MeshRenderer>().material.color = burnt;
                gameObject.name = "Burnt Food";
                break;

        }
    }

}
