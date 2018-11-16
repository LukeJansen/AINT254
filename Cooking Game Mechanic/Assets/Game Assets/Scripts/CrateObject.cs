using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateObject : MonoBehaviour {

    public GameObject crateObject;
    public Food crateFood;

    private RecipeBook recipeBook;

    private void Start()
    {
        recipeBook = new RecipeBook();

        crateFood = recipeBook.foodItems[0];
    }

}
