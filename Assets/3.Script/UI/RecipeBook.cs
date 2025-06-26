using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    public Button recipe01, recipe02;

    public GameObject healPotionR;
    //public GameObject LuckPotionR;


    void Start()
    {
        healPotionR.SetActive(false);
        //LuckPotionR.SetActive(false);
    }
    public void OnClickRecipe01()
    {
        healPotionR.SetActive(true);
    }
    public void OnClosedRecipe()
    {
        healPotionR.SetActive(false);
    }



}
