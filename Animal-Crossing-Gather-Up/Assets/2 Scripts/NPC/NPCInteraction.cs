using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Transform player;
    private float interactionDistance = 3f;

    private Dictionary<Transform, INPCDialog> npcs = new Dictionary<Transform, INPCDialog>();
    private Dictionary<Transform, NPCState> npcStates = new Dictionary<Transform, NPCState>();

    [HideInInspector]
    public bool isDialogActive = false;

    private void Awake()
    {
        foreach (var npc in GetComponentsInChildren<INPCDialog>())
        {
            Transform npcTransform = ((MonoBehaviour)npc).transform;

            npcs.Add(npcTransform, npc);

            NPCState npcState = ((MonoBehaviour)npc).GetComponent<NPCState>();
            if (npcState != null)
            {
                npcStates.Add(npcTransform, npcState);
            }
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        INPCDialog nearestNPC = FindNearestNPC();
        if (Input.GetKeyDown(KeyCode.F) && !isDialogActive && !GameManager.Instance.player.animReciever.isActing && !GameManager.Instance.player.isMoving)
        {
            if (nearestNPC != null)
            {
                Transform npcTransform = ((MonoBehaviour)nearestNPC).transform;
                if (npcStates.TryGetValue(npcTransform, out NPCState npcState))
                {
                    npcState.SetCurrentState(NPCStateType.Talk);
                }
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

        return nearestNPC;
    }
}
