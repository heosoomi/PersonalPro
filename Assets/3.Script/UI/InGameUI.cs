using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Button shop;
    public Button craft;
    public Button recipe;
    public Button inventory;
    public Button Customer;
    public Button exit;
    public Button save;

    public Button Load;

    public SaveManager saveManager;



    public void OnCraftButton()
    {
        SceneManager.LoadScene("CraftRoom");
    }
    public void OnRecipeButton()
    {

    }
    public void OnInventoryButton()
    {

    }

    public void OnSaveBotton()
    {
        saveManager.Save();
    }

    public void OnLoadBotton()
    {
        saveManager.Load();
    }
    
}
