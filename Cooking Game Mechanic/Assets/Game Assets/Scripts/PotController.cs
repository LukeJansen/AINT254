using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotController : MonoBehaviour {
   
    public PickUpBehaviour controller;
    public RecipeBook recipeBook;
    public GameObject textPrefab;
    public GameObject potText;

    public Image cookingImage;
    public Sprite cookingGood, cookingBad, cookingBurnt;

    private Dictionary<string, int> contents;
    private bool cooking = false;
    private float currentRecipe, timeCooking;
        
    // Use this for initialization
    void Start () {
        contents = new Dictionary<string, int>();

        controller = Camera.main.GetComponent<PickUpBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        CookingUpdate();
        CookingImageUpdate();
	}

    private void CookingUpdate()
    {

    }

    private void CookingImageUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            string name = collision.gameObject.name.Replace("(Clone)", "");

            if (contents.ContainsKey(name))
            {
                contents[name] += 1;
            }
            else
            {
                contents.Add(name, 1);
            }

            ShowItem(name);

            controller.DestroyPickup(collision.gameObject);
        }
    }

    private void ShowItem(string itemName)
    {
        potText = Instantiate(textPrefab, transform);
        potText.GetComponent<PotTextBehaviour>().objectName = itemName;
    }
}
