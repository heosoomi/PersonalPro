using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StirManager : MonoBehaviour
{
    public RecipeData recipeData;
    public List<RecipeData> allRecipes;
    private List<int> stirOrder;
    // StirManager 내부
    private int dragStartIndex = -1;
    private int currentOrderIndex = 0;

    //public List<StirPoint> stirPoints;
    private StirPoint prevHoverPoint;

    public Animator spoonAnim;

    public CraftUIManager popup;

    private List<IngredientData> selectedIngredients = new List<IngredientData>();
    private List<int> playerStirOrder = new List<int>();

    void Start()
    {
        stirOrder = new List<int>(recipeData.stirOrder ?? new List<int>());

    }

    void Update()
    {
        //드래그 중일때만 체크
        if (Input.GetMouseButton(0))
        {
            PointerEventData ped = new PointerEventData(EventSystem.current);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);

            StirPoint hoverPoint = null;
            foreach (var r in results)
            {
                hoverPoint = r.gameObject.GetComponent<StirPoint>();
                if (hoverPoint != null) break;
            }

            //이전 포인트 Hightlight 해제
            if (prevHoverPoint != null && prevHoverPoint != hoverPoint)
                prevHoverPoint.SetHighlight(false);

            //현재 포인트 Highlight 적용
            if (hoverPoint != null)
                hoverPoint.SetHighlight(true);

            prevHoverPoint = hoverPoint;
        }
        else
        {
            //드래그가 끝나면 전체 하이라이트 해제
            if (prevHoverPoint != null)
            {
                prevHoverPoint.SetHighlight(false);
                prevHoverPoint = null;
            }
        }

    }

    public void OnDragStart(int pointIndex)
    {
        dragStartIndex = pointIndex;
    }

    public void OnDragEnd(int pointIndex)
    {
        if (stirOrder == null || stirOrder.Count < 2) return;

        int expectedStart = stirOrder[currentOrderIndex];
        int expectedEnd = stirOrder[currentOrderIndex + 1];

        Debug.Log($"expectedStart: {expectedStart}, expectedEnd: {expectedEnd}");
        spoonAnim.SetTrigger("ROTATE");
        if (dragStartIndex == expectedStart && pointIndex == expectedEnd)
        {
            Debug.Log($"드래그 성공: {expectedStart} → {expectedEnd}");
            currentOrderIndex++;

            if (currentOrderIndex == stirOrder.Count - 1)
            {
                Debug.Log("전체 성공!");
                ResetStir();
                TryCrafting(selectedIngredients, playerStirOrder);
                spoonAnim.SetTrigger("OWARI");
            }
        }
        else
        {
            Debug.Log("드래그 실패 (중간에 잘못된 포인트 혹은 잘못된 순서)");
            ResetStir();
            spoonAnim.SetTrigger("OWARI");
        }
        dragStartIndex = -1;
    }
    private void ResetStir()
    {
        currentOrderIndex = 0;
        dragStartIndex = -1;
        // 실패 UI 처리 등
    }

    private bool isIngredientMatch(RecipeData recipe, List<IngredientData> selected)
    {
        var setA = new HashSet<IngredientData>(recipe.ingredients);
        var setB = new HashSet<IngredientData>(selected);
        return setA.SetEquals(setB);
    }
    public RecipeData FindMatchingRecipe(List<IngredientData> selectedIngredients, List<int> playerStirOrder)
    {
        foreach (var recipe in allRecipes)
        {
            //1.재료 완전 일치
            if (!isIngredientMatch(recipe, selectedIngredients)) continue;
            //2.젓기 순서 완전 일치
            if (recipe.stirOrder.Count != playerStirOrder.Count) continue;

            bool orderMatch = true;
            for (int i = 0; i < recipe.stirOrder.Count; i++)
            {
                if (recipe.stirOrder[i] != playerStirOrder[i])
                {
                    orderMatch = false;
                    break;
                }
            }
            if (orderMatch)
                return recipe; // 일치하는 레시피 발견
        }
        return null;

    }
    public void TryCrafting(List<IngredientData> selected, List<int> stirOrder)
    {
        RecipeData matched = FindMatchingRecipe(selected, stirOrder);
        if (matched != null)
        {
            Debug.Log($"성공! {matched.resultPotion.PortionName}");
            popup.ShowPotionPopup(matched.resultPotion);

            //***인벤추가 등 다른 처리도 여기서 하셈~!
        }
        else
        {
            Debug.Log("제작실패!");

             //*** 실패 팝업/ 효과 등 추가 처리
        }
    }

}