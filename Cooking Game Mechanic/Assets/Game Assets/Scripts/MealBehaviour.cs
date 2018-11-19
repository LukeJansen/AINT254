using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealBehaviour : MonoBehaviour {

    public int foodIndex;
    public Color under, good, bad, burnt;
    public Food food;
    
    private RecipeBook recipeBook;
    private int state;

	void Start () {

        under = new Color(0.66f, 0.66f, 0.66f);
        good = new Color(0, 0.66f, 0);
        bad = new Color(1f, 0.5f, 0);
        burnt = new Color(0.66f, 0, 0);

        food = recipeBook.foodItems[foodIndex];
    }
	
    public void SetMealBehaviour(int foodIndex, int state)
    {
        this.foodIndex = foodIndex;
        // 0 - Good, 1 - Bad, 2 - Burnt
        this.state = state;

        recipeBook = new RecipeBook();
        food = recipeBook.foodItems[foodIndex];

        Start();
        SetUp();
    }

    private void SetUp()
    {
        gameObject.name = recipeBook.foodItems[foodIndex].name;

        switch (state)
        {
            case 0:
                GetComponent<MeshRenderer>().material.color = under;
                gameObject.name += " (Under Cooked)";
                break;

            case 1:
                GetComponent<MeshRenderer>().material.color = good;
                break;

            case 2:
                GetComponent<MeshRenderer>().material.color = bad;
                gameObject.name += " (Over Cooked)";
                break;

            default:
                GetComponent<MeshRenderer>().material.color = burnt;
                gameObject.name = "Burnt Food";
                break;

        }
    }

}
