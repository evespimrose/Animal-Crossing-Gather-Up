using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateSystem : MonoBehaviour
{
    private int year = 1;
    private int month = 1;
    private int day = 1;
    private float previousTime;

    public static event Action<int, int, int> OnDateChanged;
    public static event Action<int> OnMonthChanged;

    public void Update()
    {
        float currentTime = TimeManager.Instance.CurrentTime;

        if (previousTime < 24f && currentTime == 0f)
        {
            AdvanceDay();
        }
        previousTime = currentTime;
    }

    private void AdvanceDay()
    {
        int previousMonth = month;
        day++;

        if (day > 30)
        {
            day =1;
            month++;

            if (month > 12)
            {
                month = 1;
                year++;
            }

            if (month != previousMonth)
            {
                OnMonthChanged.Invoke(month);
            }
        }
        OnDateChanged?.Invoke(year, month, day);
    }
}
