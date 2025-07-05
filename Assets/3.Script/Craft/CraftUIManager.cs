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

    public GameObject switchGO;

    public GameObject SwitchPage;

    public GameObject Pan;
    public Animator craftAnimator; // 항아리, 뚜껑 등 애니메이터
    public GameObject inventoryPanel;
    public GameObject jar;
    public GameObject rerecipebook;
    public AnimationClip jarAni;

    public StirManager stirManager;


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
        popupOBJ.SetActive(false);

        //switchGO.SetActive(true);
        

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
    public void SetSelectedIngredients(List<IngredientData> ingredients)
    {
        selectedIngredients = ingredients;
    }

    public void GoToCraftStep()
    {
        stirManager.SetSelectedIngredients(selectedIngredients);
        //제작단계로 이동하는 코드
        craftAnimator.SetTrigger("DONE");
        switchGO.SetActive(false);


    }

    public void ShowPotionPopup(PortionData potion)
    {
        if (potion == null)
        {
            Debug.LogWarning("포션데이터가 null이라 팝업 ㄴㄴ");
            return;
        }

        if (popupOBJ == null) Debug.LogError("popupOBJ가 Inspector에 연결 안됨!");
        if (iconImage == null) Debug.LogError("iconImage 연결 안됨!");
        if (nameText == null) Debug.LogError("nameText 연결 안됨!");

        popupOBJ.SetActive(true);
        iconImage.sprite = potion.icon;
        nameText.text = potion.PortionName;
        

        Debug.Log($"icon: {potion.icon}, name: {potion.PortionName}");
    }
    public void HidePopup()
    {
        popupOBJ.SetActive(false);
    }
}
