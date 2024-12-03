using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
	// UI Components
	public Canvas mainCanvas;
	public InventoryUI inventoryUI;
	public PurchaseUI purchaseUI;
	public OptionUI optionUI;
	public NPCPanelUI npcPanelUI;

	// UI States
	private bool isInventoryOpen = false;
	private bool isPurchaseOpen = false;
	private bool isDialogOpen = false;

	protected override void Awake()
	{
		base.Awake();
		InitializeUIComponents();
	}

	private void InitializeUIComponents()
	{
		// Find or create main canvas if not assigned
		if (mainCanvas == null)
		{
			mainCanvas = FindObjectOfType<Canvas>();
			if (mainCanvas == null)
			{
				GameObject canvasObj = new GameObject("MainCanvas");
				mainCanvas = canvasObj.AddComponent<Canvas>();
				canvasObj.AddComponent<CanvasScaler>();
				canvasObj.AddComponent<GraphicRaycaster>();
				mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
				canvasObj.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				canvasObj.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
			}
		}

		// Initialize UI components
		inventoryUI = FindAnyObjectByType<InventoryUI>();
		purchaseUI = FindAnyObjectByType<PurchaseUI>();
		optionUI = FindAnyObjectByType<OptionUI>();
		npcPanelUI = FindAnyObjectByType<NPCPanelUI>();

		// Set initial states
		if (inventoryUI)
		{
			inventoryUI.gameObject.SetActive(false);
		}
		if (purchaseUI)
		{
			purchaseUI.gameObject.SetActive(false);
		}
		if (optionUI)
		{
			optionUI.gameObject.SetActive(false);
		}
		if (npcPanelUI != null)
		{
			npcPanelUI.dialogPanel.SetActive(false);
			npcPanelUI.enterPanel.SetActive(false);
		}
	}

	// Inventory Management
	public void OpenInventory()
	{
		if (inventoryUI != null)
		{
			inventoryUI.gameObject.SetActive(true); // Show inventoryUI
			inventoryUI.UpdateAllSlotUIs(); // Update the slots
		}
	}
	public void CloseInventory()
	{
		if (inventoryUI != null)
		{
			inventoryUI.gameObject.SetActive(false);    // Hide inventoryUI
		}
	}

	// PurchaseUI Management
	public void OpenPurchasePanel()
	{
		if (purchaseUI != null)
		{
			isPurchaseOpen = true;
			purchaseUI.PurchasePanelOpen();
		}
	}
	public void ClosePurchasePanel()
	{
		if (purchaseUI != null)
		{
			isPurchaseOpen = false;
			purchaseUI.PurchasePanelClose();
		}
	}

	// Dialog System Management
	public void ShowDialog(string[] dialogTexts, int talkCount)
	{
		isDialogOpen = true;
		npcPanelUI.dialogPanel.SetActive(true);
		npcPanelUI.dialogText.text = dialogTexts[talkCount];
	}
	public void CloseDialog()
	{
		isDialogOpen = false;
		npcPanelUI.dialogPanel.SetActive(false);
		npcPanelUI.enterPanel.SetActive(false);
	}

	// OptionUI Management
	public void ShowOptions(string[] options)
	{
		optionUI.PanelActive(true);
		optionUI.SetOptions(options);
	}
	public void CloseOptions()
	{
		optionUI.PanelActive(false);
	}

	// General UI State Check
	public bool IsAnyUIOpen()
	{
		return isInventoryOpen || isPurchaseOpen || isDialogOpen;
	}
}
