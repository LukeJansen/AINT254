using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behaviour placed on a crate to allow it to store objects.
public class CrateObject : MonoBehaviour {

    // Variable to hold prefab that crate will create.
    public GameObject crateObject;
    // Variable to hold the food type of crateObject.
    public Food crateFood;

    //Variable to reference the recipe book.
    private RecipeBook recipeBook;

    private void Start()
    {
        // Creating new recipe book.
        recipeBook = new RecipeBook();

        // Gets the food type of the crate object.
        crateFood = recipeBook.foodItems[crateObject.GetComponent<FoodBehaviour>().foodIndex];
    }

}
