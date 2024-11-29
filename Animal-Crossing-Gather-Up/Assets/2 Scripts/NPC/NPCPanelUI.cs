using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCPanelUI : MonoBehaviour
{
    [Header("Panel UI")]
    public GameObject dialogPanel;
    public GameObject choosePanel;
    public GameObject enterPanel;
    public GameObject cursorImage;

    [Header("Dialog Panel Text")]
    public TextMeshProUGUI dialogText;

    [Header("Choose Panel Text")]
    public TextMeshProUGUI firstChooseText; //ù��° ����? ������ �ؽ�Ʈ
    public TextMeshProUGUI secondChooseText; //�ʿ信 ���� �߰�(���� 2��)
    public TextMeshProUGUI thirdChooseText; //��ȭ ���� �ؽ�Ʈ
}
