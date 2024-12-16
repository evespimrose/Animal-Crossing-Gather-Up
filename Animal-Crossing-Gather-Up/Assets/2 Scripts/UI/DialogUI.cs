using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    [Header("Panel UI")]
    public GameObject dialogPanel;
    public GameObject enterPanel;

    [Header("Dialog Panel Text")]
    public TextMeshProUGUI dialogText;

    private void Start()
    {
        dialogPanel.SetActive(false);
        enterPanel.SetActive(false);
    }
}
