using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPCController : MonoBehaviour, INPCDialog
{
    private NPCInteraction interaction;
    public string[] shopOptionTexts = { "�Ȱ� �;�!", "������ ������!", "�ƹ��͵� �Ƴ�" };
    private bool isSelecting = false;
    private string selectedOption = "";

    private void Start()
    {
        interaction = GetComponentInParent<NPCInteraction>();
    }

    public void NPCDialogStart()
    {
        interaction.isDialogActive = false;
        OpenShop();
    }
    private void OpenShop()
    {
        isSelecting = true;
        UIManager.Instance.ShowOptions(shopOptionTexts);
        StartCoroutine(WaitForSelectEndCoroutine());
    }


    private IEnumerator WaitForSelectEndCoroutine()
    {
        while (isSelecting)
        {
            selectedOption = UIManager.Instance.GetSelectedOption();
            UIManager.Instance.SetSelectedOptionInit();

            if (selectedOption == "")
            {
                yield return new WaitForEndOfFrame();
            }
            else if (selectedOption == "�Ȱ� �;�!")
            {
                UIManager.Instance.OpenSellPanel();
                isSelecting = false;
                selectedOption = "";
            }
            else if (selectedOption == "������ ������!")
            {
                UIManager.Instance.OpenPurchasePanel();
                isSelecting = false;
                selectedOption = "";
            }
            else if (selectedOption == "�ƹ��͵� �Ƴ�")
            {
                isSelecting = false;
                selectedOption = "";
            }
        }
    }
}
