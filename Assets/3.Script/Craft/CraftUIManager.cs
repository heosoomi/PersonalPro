using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Unity.VisualScripting;

public class CraftUIManager : MonoBehaviour
{
    public Button createButton;
    public Button recipeButton;
    public Button prevButton;
    public Button recipeClose;

    //public Button switchGO;

    public GameObject SwitchPage;

    public GameObject Pan;
    public Animator craftAnimator; // 항아리, 뚜껑 등 애니메이터
    public GameObject inventoryPanel;
    public GameObject jar;
    public GameObject rerecipebook;
    public AnimationClip jarAni;

    [Header("성공/실패 PopUP 창")]
    public GameObject popupOBJ;
    public Image iconImage;
    public Text nameText;



    public List<IngredientData> selectedIngredients = new List<IngredientData>();

    void Start()
    {
        //createButton.onClick.AddListener(OnCreateButtonClicked);
        inventoryPanel.SetActive(false); // 처음엔 인벤토리 끔\
        rerecipebook.SetActive(false);
        Pan.SetActive(false);

    }

    public void OnCreateButtonClicked()
    {
        StartCoroutine(jar_co());

    }
    public void OnClosedButton()
    {
        inventoryPanel.SetActive(false);
    }
    public void OnRecipeBookOpen()
    {
        rerecipebook.SetActive(true);
        Pan.SetActive(true);
        //inventoryPanel.SetActive(false);
    }
    public void OnRecipeBookClosed()
    {
        rerecipebook.SetActive(false);
        Pan.SetActive(false);
        //inventoryPanel.SetActive(true);
    }
    public void OnSwitchGoButton()
    {
        SwitchPage.SetActive(true);
        jar.SetActive(false);
    }
    public void OnPrevPage()
    {
        SceneManager.LoadScene("MainGame");
    }

    IEnumerator jar_co()
    {

        SwitchPage.SetActive(false);


        jar.SetActive(true);

        yield return new WaitForSeconds(jarAni.length);
        OpenInventory();


    }

    void OpenInventory()
    {
        inventoryPanel.SetActive(true);
    }
    public void GoToCraftStep()
    {
        //제작단계로 이동하는 코드
        craftAnimator.SetTrigger("DONE");
    }

    public void ShowPotionPopup(PortionData potion)
    {
        if (potion == null) return;

        popupOBJ.SetActive(true);
        iconImage.sprite = potion.icon;
        nameText.text = potion.PortionName;
    }
    public void HidePopup()
    {
        popupOBJ.SetActive(false);
    }
}
