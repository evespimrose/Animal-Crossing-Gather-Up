using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Transform player;
    private float interactionDistance = 2f;

    private Dictionary<Transform, INPCArea> npcInterfaces = new Dictionary<Transform, INPCArea>();

    private void Awake()
    {
        foreach (var npc in GetComponentsInChildren<INPCArea>())
        {
            Transform npcTransform = ((MonoBehaviour)npc).transform;
            npcInterfaces.Add(npcTransform, npc);
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }


        INPCArea nearestNPC = FindNearestNPC();
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (nearestNPC != null)
            {
                nearestNPC.NPCDialogStart();
            }
        }

    }

    private INPCArea FindNearestNPC()
    {
        float minDistance = interactionDistance;
        INPCArea nearestNPC = null;

        foreach (var npcPair in npcInterfaces)
        {
            float distance = Vector3.Distance(player.transform.position, npcPair.Key.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNPC = npcPair.Value;
            }
        }

        if (nearestNPC != null)
        {
            print("상호작용 가능한 거리");
        }

        return nearestNPC;
    }
}
