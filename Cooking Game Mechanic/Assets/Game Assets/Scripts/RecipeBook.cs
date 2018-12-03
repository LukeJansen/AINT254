using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook {

    public List<Food> foodItems;
    public List<Recipe> recipeItems;
    public int foodCount, mealCount;

    private Dictionary<string, int> tempIngredients;

    public RecipeBook()
    {
        foodCount = 2;
        mealCount = 3;

        SetUp();
    }

    private void SetUp()
    {
        foodItems = new List<Food>();
        recipeItems = new List<Recipe>();

        CreateFood();
        CreateRecipes();
    }

    void CreateFood()
    {
        foodItems.Add(new Food("Tomato"));
        foodItems.Add(new Food("Beef"));
        foodItems.Add(new Food("Tomato Soup"));
        foodItems.Add(new Food("Cooked Beef"));
        foodItems.Add(new Food("Burnt Food"));
    }

    void CreateRecipes()
    {
        tempIngredients = new Dictionary<string, int> { { foodItems[0].name, 1 } };
        recipeItems.Add(new Recipe(tempIngredients, 10, 2));

        tempIngredients = new Dictionary<string, int> { { foodItems[1].name, 1 } };
        recipeItems.Add(new Recipe(tempIngredients, 15, 3));
    }
    
    public int RecipeCheck(Dictionary<string, int> potContents)
    {
        for (int i = 0; i < recipeItems.Count; i++)
        {
            string name = new List<string>(recipeItems[i].ingredients.Keys)[0];
            //Debug.Log("Recipe: " + new List<string>(recipeItems[i].ingredients.Keys)[0]);
            //Debug.Log("Pot: " + new List<string>(potContents.Keys)[0]);

            if (potContents.ContainsKey(name) && recipeItems[i].ingredients[name] == potContents[name])
            {
                return i;
            }
        }
        return 100;
    }
}
