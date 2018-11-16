using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour {

    public RecipeBook recipeBook;
    public Food foodObject;
    public int foodIndex;

	// Use this for initialization
	void Start () {
        recipeBook = GameObject.Find("FoodStore").GetComponent<RecipeBook>();

        foodObject = recipeBook.foodItems[foodIndex];	
	}

}
