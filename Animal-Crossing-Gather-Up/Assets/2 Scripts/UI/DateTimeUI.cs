using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateTimeUI : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private Text dateText;

    [SerializeField] private CanvasGroup canvasGroup;  // UI 페이드를 위한 CanvasGroup
    [SerializeField] private float fadeSpeed = 5f;     // 페이드 속도

    private DateSystem dateSystem;

    private void Start()
    {
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // DateSystem 찾기
        dateSystem = FindObjectOfType<DateSystem>();

        if (dateSystem != null)
        {
            UpdateTimeUI(TimeManager.Instance.CurrentTime);
            UpdateDateUI(dateSystem.CurrentMonth, dateSystem.CurrentDay);

            TimeManager.Instance.OnTimeChanged += UpdateTimeUI;
            DateSystem.OnDateChanged += UpdateDateUI;
        }
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChanged -= UpdateTimeUI;
        DateSystem.OnDateChanged -= UpdateDateUI;
    }
    private void Update()
    {
        // UI 상태에 따라 페이드 인/아웃
        float targetAlpha = UIManager.Instance.IsAnyUIOpen() ? 0f : 1f;
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
    }
    private void UpdateTimeUI(float time)
    {
        if (timeText == null) return;

        int hours = (int)time;                  // 캐스팅 사용
        int minutes = (int)((time % 1) * 60);   // 캐스팅 사용
        string period = hours < 12 ? "AM" : "PM";
        hours = hours % 12 == 0 ? 12 : hours % 12;

        timeText.text = string.Format("{0}:{1:00} {2}", hours, minutes, period);
    }
    private void UpdateDateUI(int month, int day)
    {
        if (dateText != null)
        {
            dateText.text = $"{month}월 {day}일";
        }
    }

}
