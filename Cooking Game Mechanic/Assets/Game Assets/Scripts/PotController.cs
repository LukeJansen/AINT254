using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotController : MonoBehaviour {
   
    public InteractBehaviour controller;
    public GameObject textPrefab;
    public GameObject potText;
    public ParticleSystem particle;

    public GameObject baseImage, cookingImage;
    public Sprite cookingBase, cookingGood, cookingBad, cookingBurnt;
    public Recipe currentRecipe;

    private RecipeBook recipeBook;
    private Dictionary<string, int> contents;
    private Image baseImageComponent, cookingImageComponent;
    private bool cooking, empty;
    private float cookingTime, startTime;
        
    // Use this for initialization
    void Start () {
        cooking = false;
        empty = true;

        contents = new Dictionary<string, int>();
        
        controller = Camera.main.GetComponent<InteractBehaviour>();
        baseImageComponent = baseImage.GetComponent<Image>();
        cookingImageComponent = cookingImage.GetComponent<Image>();
        recipeBook = new RecipeBook();
	}
	
	// Update is called once per frame
	void Update () {
        CookingUpdate();
        CookingImageUpdate();
	}

    private void CookingUpdate()
    {
        if (!cooking && !empty)
        {
            int result = recipeBook.RecipeCheck(contents);
            if (result != 100)
            {
                cooking = true;
                currentRecipe = recipeBook.recipeItems[result];
                cookingTime = recipeBook.recipeItems[result].cookingTime;
                startTime = Time.time;
            }
        }

        if (Cooking)
        {
            if (!particle.isPlaying) particle.Play();
        }
        else
        {
            if (particle.isPlaying) particle.Stop();
        }
    }

    private void CookingImageUpdate()
    {
        baseImage.SetActive(cooking);
        cookingImage.SetActive(cooking);

        if (cooking)
        {
            baseImage.transform.LookAt(Camera.main.transform);
            cookingImage.transform.LookAt(Camera.main.transform);

            float timeElapsed = Time.time - startTime;

            if (timeElapsed <= cookingTime)
            {
                baseImageComponent.sprite = cookingBase;
                cookingImageComponent.sprite = cookingGood;
                cookingImageComponent.fillAmount = (timeElapsed / cookingTime);
            }
            else if ((timeElapsed - cookingTime) <= cookingTime){
                baseImageComponent.sprite = cookingGood;
                cookingImageComponent.sprite = cookingBad;
                cookingImageComponent.fillAmount = ((timeElapsed - cookingTime) / cookingTime);
            }
            else if ((timeElapsed - (cookingTime * 2)) <= cookingTime)
            {
                baseImageComponent.sprite = cookingBad;
                cookingImageComponent.sprite = cookingBurnt;
                cookingImageComponent.fillAmount = ((timeElapsed - (cookingTime * 2)) / cookingTime);
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!cooking && collision.gameObject.tag == "PickUp")
        {
            Food food = collision.gameObject.GetComponent<FoodBehaviour>().foodObject;

            if (contents.ContainsKey(food.name))
            {
                contents[food.name] += 1;
            }
            else
            {
                contents.Add(food.name, 1);
                empty = false;
            }

            ShowItem(food.name);

            controller.DestroyPickup(collision.gameObject);
        }
    }

    private void ShowItem(string itemName)
    {
        potText = Instantiate(textPrefab, transform);
        potText.GetComponent<PotTextBehaviour>().objectName = itemName;
    }

    public void Clear()
    {
        cooking = false;
        empty = true;
        currentRecipe = null;
        contents = new Dictionary<string, int>();
    }

    public bool Cooking
    {
        get
        {
            return cooking;
        }
    }

    public float GetCookingTime()
    {
        return (Time.time - startTime) / cookingTime;
    }
}
