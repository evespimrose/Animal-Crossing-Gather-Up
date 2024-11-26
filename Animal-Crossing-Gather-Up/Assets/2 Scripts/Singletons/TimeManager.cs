using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : SingletonManager<TimeManager>
{
    public float DayNightCycleDuration = 60f; 
    public float CurrentTime { get; private set; }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime > DayNightCycleDuration)
            CurrentTime = 0;

        //OnTimeChanged?.Invoke(CurrentTime);
        int minute = (int)(CurrentTime / 60);
        int second = (int)(CurrentTime % 60);

        string timeText = $"{minute:D2} : {second:D2}";
    }

    public bool IsDay() => CurrentTime < DayNightCycleDuration / 2;
}
