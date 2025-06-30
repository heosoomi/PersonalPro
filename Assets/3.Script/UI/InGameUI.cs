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

    public Button guri;
    [SerializeField] private GameObject popupUI;

    private bool isOpen;

    // void OnEnable()
    // {
    //     // if (!TryGetComponent(out guri))
    //     //     Debug.Log("구리 없음");

    //     guri.enabled = false;
    // }

    void OnEnable()
    {
        isOpen = false;
        guri.onClick.RemoveAllListeners();
        guri.onClick.AddListener(OnClickNPC);
    }
    public void OnClickNPC()
    {
        if (isOpen == false)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }

        popupUI.SetActive(!isOpen);
        isOpen = !isOpen;
    }



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
        SaveManager.Instance.Save();
    }

    public void OnLoadBotton()
    {
        SaveManager.Instance.Load();
    }
    
}
