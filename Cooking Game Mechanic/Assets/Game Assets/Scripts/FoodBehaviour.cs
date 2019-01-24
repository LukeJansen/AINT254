using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to be put on food that exists in the world.
public class FoodBehaviour : MonoBehaviour {

    // Variable to keep track of type of food.
    public Food foodObject;
    // Variable to keep track of index of food item.
    public int foodIndex;

    //Variable to reference the recipe book.
    private RecipeBook recipeBook;

    void Start () {
        // Create a new recipe book.
        recipeBook = new RecipeBook();

        // Get the food object from the index provided.
        foodObject = recipeBook.foodItems[foodIndex];
	}

}
