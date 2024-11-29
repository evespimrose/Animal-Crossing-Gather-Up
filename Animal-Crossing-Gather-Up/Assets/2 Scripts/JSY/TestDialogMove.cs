using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogMove : MonoBehaviour
{
    NPCAreaController npcCtrl;
    public float moveSpeed;

    private void Start()
    {
        npcCtrl = FindObjectOfType<NPCAreaController>();
    }
    private void Update()
    {
        MoveTest();
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
            npcCtrl.AirplaneDialogActive();
        }
    }
}
