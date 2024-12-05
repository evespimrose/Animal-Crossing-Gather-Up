using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Transform player;
    private float interactionDistance = 4f;

    private Dictionary<Transform, INPCArea> npcs = new Dictionary<Transform, INPCArea>();

    [HideInInspector]
    public bool isDialogActive = false;

    private void Awake()
    {
        foreach (var npc in GetComponentsInChildren<INPCArea>())
        {
            Transform npcTransform = ((MonoBehaviour)npc).transform;

            npcs.Add(npcTransform, npc);
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }


        INPCArea nearestNPC = FindNearestNPC();
        if (Input.GetKeyDown(KeyCode.R) && !isDialogActive)
        {
            if (nearestNPC != null)
            {
                isDialogActive = true;
                nearestNPC.NPCDialogStart();
            }
        }

    }

    private INPCArea FindNearestNPC()
    {
        float minDistance = interactionDistance;
        INPCArea nearestNPC = null;

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
            print("상호작용 가능한 거리");
        }

        print(nearestNPC);
        return nearestNPC;
    }
}
