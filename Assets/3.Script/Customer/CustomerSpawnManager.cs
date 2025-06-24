
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    [Header("Customer Data")]
    public CustomerData[] customers;

    [Header("Spawn Settings")]
    public GameObject customerPrefab;
    public Transform spawnPoint;
   
    
    public float spawnIntervalMin = 0.1f;
    public float spawnIntervalMax = 10.0f;

    

    public int visitMin = 1;
    public int visitMax = 10;

    private GameObject go;
    private float totalVisitProbability;
    private int visitCount;

    private bool isVisit;




    void Start()
    {
        // 모든 고객의 방문 확률의 합 계산
        totalVisitProbability = (int)Random.Range(visitMin, visitMax);

        //스폰 루틴 시작
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (visitCount <= totalVisitProbability)
        {
            //랜덤 간격 대비
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            isVisit = true;

            SpawnCustomer();

            yield return new WaitUntil(()=> !isVisit);
        }
    }

    void SpawnCustomer()
    {
        if (go != null)
        {
            Debug.Log("손님이 존재! 중복 생성 ㄴㄴ");
            return;
        }
        // 가중치 기반 랜덤 고객 선택
        CustomerData data = GetRandomCustomerData();

        //스폰 지점 선택
        Transform point = spawnPoint;

        //프리팹 인스턴스화
        go = Instantiate(customerPrefab, point.position, Quaternion.identity);


        //customer 컴포넌트에 데이터 세팅
        Customer customer = go.GetComponent<Customer>();
        if (customer != null)
            customer.Setup(data);


        isVisit = true;

        Animator animator = go.GetComponent<Animator>();
        if (animator != null)
        {
            
        }

       
    }

    //방문확률 가중 랜덤 선택 함수
    CustomerData GetRandomCustomerData()
    {
        float roll = Random.Range(0f, totalVisitProbability);
        float accum = 0f;
        foreach (var c in customers)
        {
            accum += c.visitProbability;
            if (roll <= accum)
                return c;
        }
        return customers[0];
    }

    public void OnCustomerGone()
    {
        isVisit = false;
        visitCount++;
    }
}
