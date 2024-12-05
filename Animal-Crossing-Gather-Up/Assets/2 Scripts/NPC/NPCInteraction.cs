using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Transform player;
    private float interactionDistance = 2f;

    private Dictionary<Transform, INPCDialog> npcs = new Dictionary<Transform, INPCDialog>();
    //private Dictionary<Transform, INPCState> npcStates = new Dictionary<Transform, INPCState>();

    [HideInInspector]
    public bool isDialogActive = false;

    private void Awake()
    {
        foreach (var npc in GetComponentsInChildren<INPCDialog>())
        {
            Transform npcTransform = ((MonoBehaviour)npc).transform;

            npcs.Add(npcTransform, npc);
        }

        //foreach (var npc in GetComponentsInChildren<INPCState>())
        //{
        //    Transform npcs = ((MonoBehaviour)npc).transform;
        //    npcStates.Add(npcs, npc);
        //}
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }


        INPCDialog nearestNPC = FindNearestNPC();
        INPCState npcState;
        if (Input.GetKeyDown(KeyCode.R) && !isDialogActive)
        {
            if (nearestNPC != null)
            {
                isDialogActive = true;
                nearestNPC.NPCDialogStart();
            }
        }

    }

    private INPCDialog FindNearestNPC()
    {
        float minDistance = interactionDistance;
        INPCDialog nearestNPC = null;

        foreach (var npc in npcs)
        {
            float distance = Vector3.Distance(player.transform.position, npc.Key.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNPC = npc.Value;
            }
        }

        if (nearestNPC != null)
        {
            print("상호작용 가능 거리");
        }

        print(nearestNPC);
        return nearestNPC;
    }
}
