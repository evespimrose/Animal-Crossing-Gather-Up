using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogData", menuName = "NPC/DialogData")]
public class NPCDialogData : ScriptableObject
{
    public string npcName; //npc 이름
    [TextArea]
    public string[] dialogTexts; //ncp 대화
    public bool isChooseActive; //선택패널 활성화 bool
    public bool isEnterActive; //엔터패널 활성화 bool
}
