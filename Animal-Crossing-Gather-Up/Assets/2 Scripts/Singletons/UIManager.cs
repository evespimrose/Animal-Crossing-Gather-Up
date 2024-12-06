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
	public DialogUI dialogUI;
	public OptionUI optionUI;
	public MoneyUI moneyUI;

	// UI States
	private bool isInventoryOpen = false;
	private bool isPurchaseOpen = false;
	private bool isDialogOpen = false;
	private bool isOptionOpen = false;

	public string currentOption = "";

	[Header("Money UI Position")]
	public Vector2 moneyPanelOnInventory = new Vector2(-400, 50);
	public Vector2 moneyPanelOnShop = new Vector2(750, 400);

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
		dialogUI = FindAnyObjectByType<DialogUI>();
		optionUI = FindAnyObjectByType<OptionUI>();
		moneyUI = FindAnyObjectByType<MoneyUI>();
	}

	#region Inventory Management
	public void OpenInventory()
	{
		if (inventoryUI != null && isPurchaseOpen == false)
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
	public void ToggleInventory()
	{
		if (inventoryUI != null)
		{
			if (isInventoryOpen)
			{
				isInventoryOpen = false;
				inventoryUI.InventoryClose();
			}
			else if (isPurchaseOpen == false)
			{
				isInventoryOpen = true;
				inventoryUI.InventoryOpen();
			}
		}
	}
	#endregion

	#region PurchaseUI Management
	public void OpenPurchasePanel()
	{
		if (purchaseUI != null && isInventoryOpen == false && isPurchaseOpen == false)
		{
			isPurchaseOpen = true;
			purchaseUI.PurchasePanelOpen();
		}
	}
	public void ClosePurchasePanel()
	{
		if (purchaseUI != null && isPurchaseOpen)
		{
			isPurchaseOpen = false;
			purchaseUI.PurchasePanelClose();
		}
	}
	#endregion

	#region Dialog System Management
	public void ShowDialog(string[] dialogTexts, int talkCount)
	{
		if (dialogUI != null)
		{
			isDialogOpen = true;
			dialogUI.dialogPanel.SetActive(true);
			dialogUI.dialogText.text = dialogTexts[talkCount];
		}
	}
	public void CloseDialog()
	{
		if (dialogUI != null)
		{
			isDialogOpen = false;
			dialogUI.dialogPanel.SetActive(false);
			dialogUI.enterPanel.SetActive(false);
		}
	}
	#endregion

	#region OptionUI Management
	public void ShowOptions(string[] options)
	{
		if (optionUI != null)
		{
			isOptionOpen = true;
			optionUI.PanelActive(true);
			optionUI.SetOptions(options);
		}
	}
	public string GetSelectedOption()
	{
		return optionUI.currentOption;
	}
	public void SetSelectedOptionInit()
	{
		optionUI.currentOption = "";
	}
	public void CloseOptions()
	{
		if (optionUI != null)
		{
			isOptionOpen = false;
			optionUI.optionPanel.SetActive(false);
			optionUI.cursor.SetActive(false);
		}
	}
	public bool GetOptionActive()
	{
		return isOptionOpen;
	}
	#endregion

	#region MoneyUI Management
	public void ShowMoney()
	{
		if (isInventoryOpen)
		{
			moneyUI.ShowMoney(moneyPanelOnInventory);
		}
		else if (isPurchaseOpen)
		{
			moneyUI.ShowMoney(moneyPanelOnShop);
		}
	}
	public void HideMoney()
	{
		moneyUI.HideMoney();
	}
	#endregion

	// General UI State Check
	public bool IsAnyUIOpen()
	{
		return isInventoryOpen || isPurchaseOpen || isDialogOpen;
	}
}
