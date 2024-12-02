using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogData", menuName = "NPC/DialogData")]
public class NPCDialogData : ScriptableObject
{
    public string npcName; //npc �̸�
    [TextArea]
    public string[] dialogTexts; //ncp ��ȭ

    [TextArea]
    public string[] nextDialogTexts; //ù��° �ɼ� ���� ���� ��ȭ

    //[TextArea]
    //public string[] nextDialogTexts; // �����г� �� �ι�° ��ȭ -> �ʿ��ϴٸ� ��� ����

    public bool isChooseActive; //�����г� Ȱ��ȭ bool
    public bool isEnterActive; //�����г� Ȱ��ȭ bool
}
