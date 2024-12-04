using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogData", menuName = "NPC/DialogData")]
public class NPCDialogData : ScriptableObject
{
    public Vector3 position; //npc 위치

    public string npcName; //npc 이름
    [TextArea]
    public string[] dialogTexts; //ncp 대화

    [TextArea]
    public string[] nextDialogTexts; //첫번째 옵션 선택 이후 대화

    [TextArea]
    public string[] thirdDialogTexts;


    [Header("talkCount")]
    public int[] dialogIndex;

    [Header("bool")]
    public bool isChooseActive; //선택패널 활성화 bool
    public bool isEnterActive; //엔터패널 활성화 bool

    [Header("Option")]
    public string currentOption;
}
