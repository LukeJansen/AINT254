using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour {

    public List<Food> foodItems;
    public List<Recipe> recipeItems;

    private Dictionary<Food, int> tempIngredients;

    void Start()
    {
        foodItems = new List<Food>();
        recipeItems = new List<Recipe>();

        tempIngredients = new Dictionary<Food, int>();

        CreateFood();
        CreateRecipes();
    }

    void CreateFood()
    {
        foodItems.Add(new Food("Tomato"));
    }

    void CreateRecipes()
    {
        tempIngredients = new Dictionary<Food, int> { { foodItems[0], 2 } };
        recipeItems.Add(new Recipe(tempIngredients, 15));
    }
}
