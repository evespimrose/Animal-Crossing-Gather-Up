using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogData", menuName = "NPC/DialogData")]
public class NPCDialogData : ScriptableObject
{
    public Vector3 position; //npc ��ġ

    public string npcName; //npc �̸�
    [TextArea]
    public string[] dialogTexts; //ncp ��ȭ

    [TextArea]
    public string[] nextDialogTexts; //ù��° �ɼ� ���� ���� ��ȭ

    [TextArea]
    public string[] thirdDialogTexts;


    [Header("talkCount")]
    public int[] dialogIndex;

    [Header("bool")]
    public bool isChooseActive; //�����г� Ȱ��ȭ bool
    public bool isEnterActive; //�����г� Ȱ��ȭ bool

    [Header("Option")]
    public string currentOption;
}
