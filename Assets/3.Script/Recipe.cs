using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    private RecipeData data;

    //[SerializeField] private Image iconImage;
    [SerializeField] private Text nameText;
    [SerializeField] Transform ingredientSlotParent;

    // 데이터를 받아서 UI세팅
    public void Setup(RecipeData recipeData)
    {
        data = recipeData;
        nameText.text = data.recipeName;
        //iconImage.sprite = data.icon;

        // 슬롯에 재료 아이콘 채우기
        // for (int i = 0; i < data.ingredients.Count; i++)
        // {
        //     var slot = ingredientSlotParent.GetChild(i).GetComponent<Image>();
        //     slot.sprite = data.ingredients[i].icon;
        //     slot.enabled = true;
        // }

    }

    // 제작 요청 시 CraftManager에 전달
    public void OnSartCraft()
    {
        //CraftManager.Instance.StartCrafting(data);
    }
}
