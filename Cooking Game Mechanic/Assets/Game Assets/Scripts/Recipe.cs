using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Game_Assets.Scripts
{
    public class Recipe
    {
        public Dictionary<string, int> ingredients;
        public int cookingTime;

        public Recipe(Dictionary<string, int> ingredients, int cookingTime)
        {
            this.ingredients = ingredients;
            this.cookingTime = cookingTime;
        }
    }
}
