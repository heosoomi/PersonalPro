using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    private CustomerData data;

    [SerializeField] private Image iconImage;
    [SerializeField] private Text nameText;
    [SerializeField] private int remainingPatience;
    [SerializeField] private Animator animator;

    //SpawnManager에서 호출
    public void Setup(CustomerData customerData)
    {
        data = customerData;
        nameText.text = data.customerName;
        iconImage.sprite = data.icon;
        remainingPatience = data.patience;

        //**이곳에 고객 이동 애니메이션, 대기 애니메이션 등 추가 

        
    }

    // 플레이어가 판매 버튼을 클릭했을 때 호출
    public bool TryPurchase(RecipeData recipe, int offeredPice)
    {
        if (data.LikeRecipes.Contains(recipe) && offeredPice <= data.paymentThreshold)
        {
            // 구매 성공
            return true;
        }
        else
        {
            // 구매 거부 불만족 로직 
            return false;
        }

    }
    public bool DecreasePatience()
    {
        remainingPatience--;
        if (remainingPatience <= 0)
        {
            // 손님 퇴장
            Destroy(gameObject);
            return false;
        }
        return true;
    }

    // void OnDestroy()
    // {
    //    FindObjectOfType<CustomerSpawnManager>().OnCustomerGone();
    // }
}
