using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour {


    public Food foodObject;
    public int foodIndex;

    private RecipeBook recipeBook;

    // Use this for initialization
    void Start () {
        recipeBook = new RecipeBook();

        foodObject = recipeBook.foodItems[foodIndex];
	}

}
