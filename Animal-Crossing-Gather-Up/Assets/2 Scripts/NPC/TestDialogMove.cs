using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogMove : MonoBehaviour
{
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

    private void MoveTest()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += new Vector3(x * moveSpeed * Time.deltaTime, 0, z * moveSpeed * Time.deltaTime);
    }

    private void InteractionTest()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            moriCtrl.NPCDialogStart();
        }
    }
}
