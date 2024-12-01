using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAreaNPC
{
    public void FirstAccept(); //첫번째 선택지(선택지 3개일 경우 NPC에서 1개 추가)
    public void EndTalk(); //대화 종료 선택지'
    public void InteractionPlayer(); // NPC와의 상호작용
}
