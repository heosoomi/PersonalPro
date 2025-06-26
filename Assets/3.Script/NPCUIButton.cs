using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCUIButton : MonoBehaviour
{
    
    [SerializeField] private GameObject popupUI;
    public Button guri;


    void OnEnable()
    {
        if (!TryGetComponent(out guri))
            Debug.Log("구리 없음");

        guri.enabled = false;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        guri.enabled = true;
    }
    public void OnClickNPC()
    {
        Time.timeScale = 1;
        popupUI.SetActive(false);

    }
}
