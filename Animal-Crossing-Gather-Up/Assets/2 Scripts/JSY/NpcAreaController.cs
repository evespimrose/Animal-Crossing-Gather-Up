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
    public GameObject dialogPanel; //UIManager로 옮길 예정
    DialogController dialogController;


    private void Start()
    {
        dialogPanel.SetActive(false);
        dialogController = GetComponent<DialogController>();
    }

    private void Update()
    {

    }

    public void AirplaneDialogActive() //플레이어 스크립트에서 키 누르면 함수 실행되게 추가
    {
        dialogPanel.SetActive(true);
        dialogController.DialogStart();
    }



}
