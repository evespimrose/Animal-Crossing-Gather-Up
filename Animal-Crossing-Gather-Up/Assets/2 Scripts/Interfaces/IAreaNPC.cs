using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAreaNPC
{
    public void FirstAccept(); //ù��° ������(������ 3���� ��� NPC���� 1�� �߰�)
    public void EndTalk(); //��ȭ ���� ������'
    public void InteractionPlayer(); // NPC���� ��ȣ�ۿ�
}
