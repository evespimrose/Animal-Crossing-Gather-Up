using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogData", menuName = "NPC/DialogData")]
public class NPCDialogData : ScriptableObject
{
    public string npcName; //npc �̸�
    public string[] dialogTexts; //ncp ��ȭ
    public bool isChooseActive; //�����г� Ȱ��ȭ
}
