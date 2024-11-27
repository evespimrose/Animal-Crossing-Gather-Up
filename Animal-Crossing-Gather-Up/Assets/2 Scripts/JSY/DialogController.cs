using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;



public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI dialogText; //UIManager�� �ű� ����
    public GameObject choosePanel; //UIManager�� �ű� ����

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
                string text2 = "~���Բ� �̼Ҹ� ���ϴ� ������ ����~\n�����װ��� ž�� ���� ����� ���Դϴ�!\n�� ��Ź�帳�ϴ�!";
                currentCoroutine = StartCoroutine(TypingDialog(text2));
                talkCount++;
            }

            else if (talkCount == 2 && currentCoroutine == null)
            {
                string text3 = "�ٸ� �� �湮, �� ���� �ƴ� ���� �ʴ��ϰ� ���� ����\n���� �������� �̿����ּ���!";
                currentCoroutine = StartCoroutine(TypingDialog(text3));
                talkCount++;
            }

            else if (talkCount == 3 && currentCoroutine == null)
            {
                string text4 = "���� ������ �ʱ� ������� ���� ������� �ðܵμ̽��ϴ�!\n���� ������� �̿��ϰ� �����ø� ���� �����ҷ� ��� �������ּ���";
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
            string firstText = "����� ���ϴ� �ϴ��� �Ա�\n���� �����忡 ���� �� ȯ���մϴ�!\n��, ����...";
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
        Debug.Log("���ϼ� ���");
        choosePanel.SetActive(false);
        npctrl.dialogPanel.SetActive(false);
    }

    public void EndTalk()
    {
        Debug.Log("NPC �� ��ȭ ����");
        choosePanel.SetActive(false);
        npctrl.dialogPanel.SetActive(false);
    }
}
