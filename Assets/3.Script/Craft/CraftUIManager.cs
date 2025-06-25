using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CraftUIManager : MonoBehaviour
{
    public Button createButton;
    public Animator craftAnimator; // 항아리, 뚜껑 등 애니메이터
    public GameObject inventoryPanel;
    public GameObject jar;
    public AnimationClip jarAni;

    public List<IngredientData> selectedIngredients = new List<IngredientData>();

    void Start()
    {
        //createButton.onClick.AddListener(OnCreateButtonClicked);
        inventoryPanel.SetActive(false); // 처음엔 인벤토리 끔
    }

    public void OnCreateButtonClicked()
    {
        StartCoroutine(jar_co());

    }
    public void OnClosedButton()
    {
        inventoryPanel.SetActive(false);
    }

    IEnumerator jar_co()
    {

        createButton.gameObject.SetActive(false);
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
    }
}
