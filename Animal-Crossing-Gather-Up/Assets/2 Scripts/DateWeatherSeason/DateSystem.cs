using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateSystem : MonoBehaviour
{ 
    [SerializeField] private int month = 12;
    [SerializeField] private int day = 1;
    private float previousTime;

    public static event Action<int, int> OnDateChanged;
    public static event Action<int> OnMonthChanged;
   
    public int CurrentMonth => month;
    public int CurrentDay => day;

    private void Start()
    {
        previousTime = TimeManager.Instance.CurrentTime;
        // �ʱ� ��¥ �̺�Ʈ �߻�
        OnDateChanged?.Invoke(month, day);
        OnMonthChanged?.Invoke(month);
    }
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
            day = 1;
            month++;

            if (month > 12)
            {
                month = 1;  // 12���� ������ 1���� ���ư�
            }

            if (month != previousMonth)
            {
                OnMonthChanged.Invoke(month);
            }
        }
        OnDateChanged?.Invoke(month, day);
    }

    public string GetFormattedDate()
    {
        return $" {month}�� {day}��";
    }

    // ��¥ ���� �޼��� (����׿�)
    public void SetDate(int newMonth, int newDay)
    {
        month = Mathf.Clamp(newMonth, 1, 12);
        day = Mathf.Clamp(newDay, 1, 30);

        OnDateChanged?.Invoke(month, day);
        OnMonthChanged?.Invoke(month);
    }
}
