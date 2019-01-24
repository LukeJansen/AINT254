using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Game_Assets.Scripts
{
    // Class to hold information about a recipe.
    public class Recipe
    {
        // Variable to hold needed ingredients.
        public Dictionary<string, int> ingredients;
        // Variable to hold the time needed to cook.
        public int cookingTime;
        // Variable to hold the meal index.
        public int meal;

        // Constructor to define the above variables.
        public Recipe(Dictionary<string, int> ingredients, int cookingTime, int meal)
        {
            this.ingredients = ingredients;
            this.cookingTime = cookingTime;
            this.meal = meal;
        }
    }
}
