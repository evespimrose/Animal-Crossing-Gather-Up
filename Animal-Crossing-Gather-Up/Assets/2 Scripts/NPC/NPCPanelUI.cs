using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCPanelUI : MonoBehaviour
{
    [Header("Panel UI")]
    public GameObject dialogPanel;
    public GameObject choosePanel;
    public GameObject enterPanel;
    public GameObject cursorImage;

    [Header("Dialog Panel Text")]
    public TextMeshProUGUI dialogText;

    [Header("Choose Panel Text")]
    public TextMeshProUGUI firstChooseText; //첫번째 수락? 선택지 텍스트
    public TextMeshProUGUI secondChooseText; //필요에 따라 추가(보통 2개)
    public TextMeshProUGUI thirdChooseText; //대화 종료 텍스트
}
