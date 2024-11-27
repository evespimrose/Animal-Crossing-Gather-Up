using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;



public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI dialogText; //UIManager로 옮길 예정
    public GameObject choosePanel; //UIManager로 옮길 예정

    private Coroutine currentCoroutine;
    private NPCAreaController npctrl;
    private bool isChooseActive = false;
    private int talkCount = 0;

    private void Start()
    {
        npctrl = FindObjectOfType<NPCAreaController>();
        dialogText.text = "";
        choosePanel.SetActive(false);
    }

    public void DialogStart()
    {
        talkCount = 0;
        ChangeText();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (talkCount == 1 && currentCoroutine == null)
            {
                string text2 = "~고객님께 미소를 전하는 안전한 날개~\n도도항공의 탑승 수속 담당자 모리입니다!\n잘 부탁드립니다!";
                currentCoroutine = StartCoroutine(TypingDialog(text2));
                talkCount++;
            }

            else if (talkCount == 2 && currentCoroutine == null)
            {
                string text3 = "다른 섬 방문, 이 섬에 아는 분을 초대하고 싶을 때는\n저희 비행장을 이용해주세요!";
                currentCoroutine = StartCoroutine(TypingDialog(text3));
                talkCount++;
            }

            else if (talkCount == 3 && currentCoroutine == null)
            {
                string text4 = "고객님 앞으로 너굴 사장님이 마일 여행권을 맡겨두셨습니다!\n마일 여행권을 이용하고 싶으시면 제게 외출할래 라고 말씀해주세요";
                currentCoroutine = StartCoroutine(TypingDialog(text4));
                talkCount++;
                isChooseActive = true;
            }
        }
    }

    public void ChangeText()
    {
        if (currentCoroutine == null)
        {
            string firstText = "세계로 통하는 하늘의 입구\n머지 비행장에 오신 걸 환영합니다!\n아, 저는...";
            talkCount++;
            currentCoroutine = StartCoroutine(TypingDialog(firstText));
        }

    }

    private IEnumerator TypingDialog(string text)
    {
        dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        currentCoroutine = null;

        if (isChooseActive)
        {
            choosePanel.SetActive(true);
        }
    }

    public void GoToMileIsland()
    {
        Debug.Log("마일섬 출발");
        choosePanel.SetActive(false);
        npctrl.dialogPanel.SetActive(false);
    }

    public void EndTalk()
    {
        Debug.Log("NPC 모리 대화 종료");
        choosePanel.SetActive(false);
        npctrl.dialogPanel.SetActive(false);
    }
}
