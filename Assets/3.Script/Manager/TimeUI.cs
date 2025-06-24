using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private void Start()
    {
        TimeManager.Instance.OnTimeChanged += UpdateTimeUI;
    }
    private void UpdateTimeUI(int hour, int minute)
    {
        timeText.text = $"{hour:D2}:{minute:D2}";
    }
}
