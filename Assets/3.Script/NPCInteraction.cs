using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class NPCInteraction : MonoBehaviour
{
    private Animator animator;
    

    [SerializeField] private GameObject popupUI; // Inspector에서 연결
    private SpriteRenderer guri;
    private SphereCollider guriCol;


    private void Awake()
    {
        if (!TryGetComponent(out guri))
            Debug.Log("구리 없음");

        if (!TryGetComponent(out guriCol))
            Debug.Log("구리콜 없음");


        guri.color = new Vector4(1, 1, 1, 1);

        popupUI.SetActive(false);               // 시작할 때는 UI는 꺼두기
    }



    private void Start()
    {

        //animator = GetComponent<Animator>();
        

    }

    void Update()
    {
        // 모바일 빌드용 터치 인풋
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == this.transform)
                {
                    OnMouseDown();
                }
            }
        }

        if (!popupUI.activeSelf)
        {
            guriCol.enabled = true;
            guri.color = new Vector4(1, 1, 1, 1);
        }
        else
        {
            guriCol.enabled = false;
            guri.color = new Vector4(1, 1, 1, 0);
        }

    }

    private void OnMouseDown()
    {

        ShowPopup();
        
    }



    private void ShowPopup()
    {

        popupUI.SetActive(true);
        Time.timeScale = 0;
        
    }

   
}
