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

    private RecipeData lastMatchedRecipe;
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
    public void SetSelectedIngredients(List<IngredientData> ingredients)
    {
        selectedIngredients = ingredients;
        playerStirOrder.Clear();
        dragStartIndex = -1;
        currentOrderIndex = 0;
    }
    public void OnDragStart(int pointIndex)
    {
        dragStartIndex = pointIndex;
    }

    int expectedEnd;
    public void OnDragEnd(int pointIndex)
    {
        if (stirOrder == null || stirOrder.Count < 2) return;

        int expectedStart = stirOrder[currentOrderIndex];
        expectedEnd = stirOrder[currentOrderIndex + 1];

        Debug.Log($"expectedStart: {expectedStart}, expectedEnd: {expectedEnd}");
        spoonAnim.SetTrigger("ROTATE");
        if (dragStartIndex == expectedStart && pointIndex == expectedEnd)
        {
            Debug.Log($"드래그 성공: {expectedStart} → {expectedEnd}");


            playerStirOrder.Add(expectedStart); //cnrk
            currentOrderIndex++;
            if (currentOrderIndex == stirOrder.Count - 1)
            {
                playerStirOrder.Add(expectedEnd);

                Debug.Log("전체 성공!");

                Debug.Log("TryCrafting전 playerStirOrder: " + string.Join(",", playerStirOrder));
                //Debug.Log("TryCrafting전 selectedIngredients: " + string.Join(",", selectedIngredients.Select(i => i.name)));
                TryCrafting(selectedIngredients, playerStirOrder);
                ResetStir();
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
        playerStirOrder.Clear();
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
            Debug.Log(1);
            //1.재료 완전 일치
            if (!isIngredientMatch(recipe, selectedIngredients)) continue;
            Debug.Log(2);
            Debug.Log($"recipe stirOrder Count: {recipe.stirOrder.Count}, playerStirOrder Count: {playerStirOrder.Count}");
            //2.젓기 순서 완전 일치
            if (recipe.stirOrder.Count != playerStirOrder.Count) continue;
            Debug.Log(3);
            bool orderMatch = true;
            for (int i = 0; i < recipe.stirOrder.Count; i++)
            {
                Debug.Log(4);
                if (recipe.stirOrder[i] != playerStirOrder[i])
                {
                    Debug.Log(5);
                    orderMatch = false;
                    break;

                }
                Debug.Log(6);
            }
            Debug.Log(7);
            if (orderMatch)
                return recipe; // 일치하는 레시피 발견
Debug.Log(8);
        }
Debug.Log(9);
        return null;

    }
    public void TryCrafting(List<IngredientData> selected, List<int> stirOrder)
    {
        RecipeData matched = FindMatchingRecipe(selected, stirOrder);
        if (matched != null)
        {
            
            lastMatchedRecipe = matched;

            //***인벤추가 등 다른 처리도 여기서 하셈~!
        }
        else
        {
            Debug.Log("제작실패!");

            //*** 실패 팝업/ 효과 등 추가 처리
        }
    }
     public void OnSmokeOutEnd()
    {
       
       
        //마지막 tryCrafting에서 성공 RecipeData를 저장해두었다가 사용
        if (lastMatchedRecipe != null)
        {
            Debug.Log("smokeOut끝! 팝업 실행");
            popup.ShowPotionPopup(lastMatchedRecipe.resultPotion);
            lastMatchedRecipe = null; // 혹시 모를 중복 방지
        }
    }

}