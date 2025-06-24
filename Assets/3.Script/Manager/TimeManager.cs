using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public float currentTime { get; private set; } = 0f; // 경과시간

    [Header("가상 시간 설정")]
    public int currentHour = 9; // 시작 시간(9시)
    public int currentMinute = 0;
    public float realSecondsPerGameMinute = 1f; // 실제 1초 = 가상 1분

    

    public event Action<int, int> OnTimeChanged;

    private float timer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= realSecondsPerGameMinute)
        {
            timer = 0f;
            AdvanceTime();
        }

    }

    private void AdvanceTime()
    {
        currentMinute++;
        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;

            if (currentHour >= 24)
                currentHour = 0; // 다음날로 순환 
        }
        OnTimeChanged?.Invoke(currentHour, currentMinute);
    }

    public string GetFormattedTime()
    {
        return $"{currentHour:D2} : {currentMinute:D2}";
    }
    public void SetSpeed(float secondsPerMinute)
    {
        realSecondsPerGameMinute = secondsPerMinute;
    }

}
