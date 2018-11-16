using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateObject : MonoBehaviour {

    public RecipeBook recipeBook;

    public GameObject crateObject;
    public Food crateFood;

    private void Start()
    {
        crateFood = recipeBook.foodItems[0];
    }

}
