using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PortionSlotData : MonoBehaviour
{
    public int count = 1; //현재수량
    public Image iconImage;
    public TextMeshProUGUI countText;

    public PortionData portionData;

    [SerializeField] private GameObject darkOverlay;

    private void Start()
    {
        UpdateCountUI();
    }

    public void UsePortion()
    {
        count--;
        if (count <= 0)
        {
            count = 0;
            UpdateCountUI();
            SetDepletedVisual(); // 품절 상태 연출

        }
        else
        {
            UpdateCountUI();
        }
    }

    public void UpdateCountUI()
    {
        countText.text = count.ToString();
    }

    public void SetDepletedVisual()
    {
        darkOverlay.SetActive(true);

        countText.text = "Slod Out";

        GetComponent<PortionDragHandler>().enabled = false;
    }
}

