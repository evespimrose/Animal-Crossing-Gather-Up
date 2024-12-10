using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogData", menuName = "NPC/DialogData")]
public class NPCDialogData : ScriptableObject
{
    public string npcName; //npc �̸�

    [Header("Dialog")]
    [TextArea]
    public string[] dialogTexts; //ncp ��ȭ

    [TextArea]
    public string[] nextDialogTexts; //ù��° �ɼ� ���� ���� ��ȭ

    [TextArea]
    public string[] thirdDialogTexts;

    [TextArea]
    public string[] fourthDialogTexts;

    [Header("DialogIndex")]
    public int dialogIndex;

    [Header("bool")]
    public bool isChooseActive; //�����г� Ȱ��ȭ bool
    public bool isEnterActive; //�����г� Ȱ��ȭ bool

    [Header("Option")]
    public string currentOption;
}
