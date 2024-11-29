using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NPCAreaController : MonoBehaviour
{
    public GameObject dialogPanel; //UIManager�� �ű� ����
    DialogController dialogController;


    private void Start()
    {
        dialogPanel.SetActive(false);
        dialogController = GetComponent<DialogController>();
    }

    private void Update()
    {

    }

    public void AirplaneDialogActive() //�÷��̾� ��ũ��Ʈ���� Ű ������ �Լ� ����ǰ� �߰�
    {
        dialogPanel.SetActive(true);
        dialogController.DialogStart();
    }



}
