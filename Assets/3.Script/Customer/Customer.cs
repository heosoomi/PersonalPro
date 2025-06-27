using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CustomerState
{
    Waiting,
    Angry,
    Leaving
}

public class Customer : MonoBehaviour
{
    private CustomerData data;

    // [SerializeField] private Image iconImage;
    // [SerializeField] private Text nameText;
    [SerializeField] private int remainingPatience;
    [SerializeField] private Animator animator;


    private float patienceTimer = 0f;
    private float patienceDecreaseInterval = 1f;  //몇초마다 감소할것인지

    private CustomerState state = new CustomerState();


    private PortionData desirePortion; // 손님이 원하는 포션

    [Header("말풍선 관련")]
    [SerializeField] private GameObject balloonPrefab; // 말풍선 프리팹
    private GameObject balloonInstance;
    public Transform balloonAnchor; // 위치용
    
    

    //SpawnManager에서 호출
    public void Setup(CustomerData customerData)
    {
        DropHandler dropHandler = FindObjectOfType<DropHandler>();
        if (dropHandler != null)
        {
            dropHandler.SetCustomer(this);
        }
        else
        {
            Debug.LogWarning("❌ DropHandler를 찾을 수 없어요! 연결 필요!");
        }

        data = customerData;
        //nameText.text = data.customerName;
        //iconImage.sprite = data.icon;
        remainingPatience = data.patience;

        // 원하는 레시피 중 하나 랜덤으로 선택
        if (data.LikePortion.Count > 0)
        {
            desirePortion = data.LikePortion[Random.Range(0, data.LikePortion.Count)];
        }

        SpawnBalloon();
        EnterState(CustomerState.Waiting);


    }

    private void SpawnBalloon()
    {
        if (balloonPrefab == null || desirePortion == null || balloonAnchor == null)
        {
            Debug.LogWarning("말풍선 프리팹 또는 위치 또는 레시피가 없음");
            return;
        }

        //말풍선 생성 및 붙이기
        balloonInstance = Instantiate(balloonPrefab, balloonAnchor.position, Quaternion.identity, balloonAnchor);
        Transform iconTransform = balloonInstance.transform.Find("BalloonBG/PotionIcon");

        if (iconTransform == null)
        {
            Debug.LogError("PotionIcon 경로를 찾지 못했어요! 경로 확인해줘: BalloonCanvas/BalloonBG/PotionIcon");
            return;
        }

        //말풍선 안에 포션 아이콘 넣기
        Image icon = iconTransform.GetComponent<Image>();
        if (icon == null)
        {
            Debug.LogError("PotionIcon에 Image 컴포넌트가 없어요!");
            return;
        }
        if (desirePortion.icon == null)
        {
            Debug.LogError($"{desirePortion.name} 의 icon 값이 null입니다! RecipeData에 연결했는지 확인해줘.");
            return;
        }
        icon.sprite = desirePortion.icon;
       
    }

    private void EnterState(CustomerState customerState)
    {
        state = customerState;

        switch (customerState)
        {
            case CustomerState.Waiting:
                StartCoroutine(waiting_co());
                break;
            case CustomerState.Angry:
                StartCoroutine(Angry_co());
                break;
            case CustomerState.Leaving:
                StartCoroutine(LeaveRoutine());
                break;
        }
    }

    IEnumerator waiting_co()
    {
        while (true)
        {
            patienceTimer += Time.deltaTime;

            if (patienceTimer >= patienceDecreaseInterval)
            {
                bool stillWaiting = DecreasePatience();

                //상태변경
                if (remainingPatience == data.patience / 2 && state == CustomerState.Waiting)
                {
                    EnterState(CustomerState.Angry);

                    yield break;
                }
            }


            yield return null;
        }
    }

    IEnumerator Angry_co()
    {
        Debug.Log($"{data.customerName} 화남!");
        animator.SetTrigger("ANGRY");

        while (true)
        {
            patienceTimer += Time.deltaTime;

            if (patienceTimer >= patienceDecreaseInterval)
            {
                bool stillWaiting = DecreasePatience();

                if (!stillWaiting)
                {
                    EnterState(CustomerState.Leaving);

                    yield break;
                }
            }
            yield return null;
        }
    }
    // 플레이어가 판매 버튼을 클릭했을 때 호출
    public bool TryPurchase(PortionData portion, int offeredPice)
    {
        if (data.LikePortion.Contains(portion) && offeredPice <= data.paymentThreshold)
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
    public bool TryReceivePortion(PortionData droppedPortion)
    {
        if (state == CustomerState.Leaving) return false;

        if (droppedPortion == desirePortion)
        {
            Debug.Log($"{data.customerName} 만족! 포션 일치");
            EnterState(CustomerState.Leaving);
            return true;
        }
        else
        {
            Debug.Log($"{data.customerName} 다른 포션이야!");
            return false;
        }
    }
    public bool DecreasePatience()
    {
        remainingPatience--;
        Debug.Log($"{data.customerName} 참을성: {remainingPatience}");
        return remainingPatience > 0;
    }

    // 떠나는 연출
    private IEnumerator LeaveRoutine()
    {
        Debug.Log($"{data.customerName} 떠나는 중...");
        if (animator) animator.SetTrigger("LEAVE");
        
        if (balloonInstance != null)
        {
            Destroy(balloonInstance);
        }
        yield return new WaitForSeconds(6f);  //애니메이션 시간

        // 말풍선 제거
        

        CustomerSpawnManager spawnManager = FindAnyObjectByType<CustomerSpawnManager>();
        if (spawnManager != null)
            spawnManager.OnCustomerGone();

        Destroy(gameObject);
    }
    public void OnReceivePotion(PortionData portion)
    {
        // 돈 획득 처리
        MoneyManager.Instance.AddMoney(portion.Price);

        // 리액션 / 애니메이션 등 추가


    }
    
}
