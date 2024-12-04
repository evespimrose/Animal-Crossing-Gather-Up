using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
	[Header("UI Components")]
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
		// Keep UIManager and Canvas persistent across scenes
		DontDestroyOnLoad(gameObject);
		if (mainCanvas != null)
		{
			DontDestroyOnLoad(mainCanvas.gameObject);
		}
		InitializeUIComponents();
	}

	private void InitializeUIComponents()
	{
		// Setup mainCanvas with default settings if not assigned
		if (mainCanvas == null)
		{
			mainCanvas = FindObjectOfType<Canvas>();
			if (mainCanvas == null)
			{
				// Create new canvas with necessary components
				GameObject canvasObj = new GameObject("MainCanvas");
				mainCanvas = canvasObj.AddComponent<Canvas>();
				canvasObj.AddComponent<CanvasScaler>();
				canvasObj.AddComponent<GraphicRaycaster>();

				// Configure canvas settings
				mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

				// Configure canvasScaler settings
				canvasObj.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				canvasObj.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);

				DontDestroyOnLoad(canvasObj);
			}
		}

		// Find all UI components in the scene
		inventoryUI = FindAnyObjectByType<InventoryUI>();
		purchaseUI = FindAnyObjectByType<PurchaseUI>();
		optionUI = FindAnyObjectByType<OptionUI>();
		npcPanelUI = FindAnyObjectByType<NPCPanelUI>();
	}

	#region Inventory Management
	public void OpenInventory()
	{
		if (inventoryUI != null)
		{
			isInventoryOpen = true;
			inventoryUI.InventoryOpen();
		}
	}
	public void CloseInventory()
	{
		if (inventoryUI != null)
		{
			isInventoryOpen = false;
			inventoryUI.InventoryClose();
		}
	}
	#endregion

	#region PurchaseUI Management
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
	#endregion

	#region Dialog System Management
	public void ShowDialog(string[] dialogTexts, int talkCount)
	{
		if (npcPanelUI != null)
		{
			isDialogOpen = true;
			npcPanelUI.dialogPanel.SetActive(true);
			npcPanelUI.dialogText.text = dialogTexts[talkCount];
		}
	}
	public void CloseDialog()
	{
		if (npcPanelUI != null)
		{
			isDialogOpen = false;
			npcPanelUI.dialogPanel.SetActive(false);
			npcPanelUI.enterPanel.SetActive(false);
		}
	}
	#endregion

	#region OptionUI Management
	public void ShowOptions(string[] options)
	{
		if (optionUI != null)
		{
			optionUI.PanelActive(true);
			optionUI.SetOptions(options);
		}
	}
	public void CloseOptions()
	{
		if (optionUI != null)
		{
			optionUI.PanelActive(false);
		}
	}
	#endregion

	// General UI State Check
	public bool IsAnyUIOpen()
	{
		return isInventoryOpen || isPurchaseOpen || isDialogOpen;
	}
}
