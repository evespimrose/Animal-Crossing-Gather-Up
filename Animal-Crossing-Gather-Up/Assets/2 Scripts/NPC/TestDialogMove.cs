using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogMove : MonoBehaviour
{
    [Header("Interaction")]
    public float interactionDistance = 2f; //상호작용 거리
    private DialogController nearestNPC;

    NPCState moriState;
    public float moveSpeed;
    private MoriController moriCtrl;
    private void Start()
    {
        moriCtrl = FindObjectOfType<MoriController>();
        moriState = FindObjectOfType<NPCState>();
    }
    private void Update()
    {
        MoveTest();
        //InteractionNPC();
        //NearstNPC();
    }

    private void InteractionNPC()
    {
        float minDistance = interactionDistance;
        nearestNPC = null;

        DialogController[] allNpc = FindObjectsOfType<DialogController>();
        foreach (DialogController npc in allNpc)
        {
            float distance = Vector3.Distance(transform.position, npc.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNPC = npc;
            }

        }
    }

    private void NearstNPC()
    {
        switch (nearestNPC)
        {
            case MoriController mori:
                mori.NPCDialogStart();
                break;
            case RoadriController roadri:
                roadri.NPCDialogStart();
                break;
            case TimmyController timmy:
                timmy.NPCDialogStart();
                break;



        }
    }

    private void MoveTest()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += new Vector3(x * moveSpeed * Time.deltaTime, 0, z * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPCMori"))
        {
            //NPC별 태그 추가해야함
            moriCtrl.NPCDialogStart();
        }
    }

}
