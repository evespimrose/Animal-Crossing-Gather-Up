using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
	public SellUI sellUI;
	public PauseOptionUI pauseOptionUI;
	public DateTimeUI dateTimeUI;

	// UI States
	private bool isInventoryOpen = false;
	private bool isPurchaseOpen = false;
	private bool isDialogOpen = false;
	private bool isOptionOpen = false;
	private bool isSellOpen = false;
	private bool isPauseOptionOpen = false;

	public string currentOption = "";

	[Header("Money UI Position")]
	public Vector2 moneyPanelOnInventory = new Vector2(-400, 50);
	public Vector2 moneyPanelOnShop = new Vector2(750, 400);

	protected override void Awake()
	{
		base.Awake();
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
		if (inventoryUI == null)
			inventoryUI = FindAnyObjectByType<InventoryUI>();
		if (purchaseUI == null)
			purchaseUI = FindAnyObjectByType<PurchaseUI>();
		if (dialogUI == null)
			dialogUI = FindAnyObjectByType<DialogUI>();
		if (optionUI == null)
			optionUI = FindAnyObjectByType<OptionUI>();
		if (moneyUI == null)
			moneyUI = FindAnyObjectByType<MoneyUI>();
		if (pauseOptionUI == null)
			pauseOptionUI = FindAnyObjectByType<PauseOptionUI>();
	}

	#region Inventory Management
	public void OpenInventory()
	{
		if (!isInventoryOpen && !isPurchaseOpen && !isSellOpen)
		{
			isInventoryOpen = true;
			inventoryUI.InventoryOpen();
		}
	}
	public void CloseInventory()
	{
		if (isInventoryOpen)
		{
			isInventoryOpen = false;
			inventoryUI.InventoryClose();
		}
	}
	public void ToggleInventory()
	{
		if (isInventoryOpen)
		{
			CloseInventory();

		}
		else
		{
			OpenInventory();
		}
	}
	#endregion

	#region SellUI Management
	public void OpenSellPanel()
	{
		if (!IsAnyUIOpen())
		{
			isSellOpen = true;
			sellUI.SellPanelOpen();
		}
	}
	public void CloseSellPanel()
	{
		if (isSellOpen)
		{
			isSellOpen = false;
			sellUI.SellPanelClose();
		}
	}
	#endregion

	#region PurchaseUI Management
	public void OpenPurchasePanel()
	{
		if (!isInventoryOpen && !isPurchaseOpen && !isSellOpen)
		{
			isPurchaseOpen = true;
			purchaseUI.PurchasePanelOpen();
		}
	}
	public void ClosePurchasePanel()
	{
		if (isPurchaseOpen)
		{
			isPurchaseOpen = false;
			purchaseUI.PurchasePanelClose();
		}
	}
	#endregion

	#region Dialog System Management
	public void ShowDialog()
	{
		isDialogOpen = true;
		dialogUI.dialogPanel.SetActive(true);
	}
	public void CloseDialog()
	{
		isDialogOpen = false;
		dialogUI.dialogPanel.SetActive(false);
		dialogUI.enterPanel.SetActive(false);
	}
	#endregion

	#region OptionUI Management
	public void ShowOptions(string[] options)
	{
		isOptionOpen = true;
		optionUI.PanelActive(true);
		optionUI.SetOptions(options);
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
		isOptionOpen = false;
		optionUI.optionPanel.SetActive(false);
		optionUI.cursor.SetActive(false);
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
		else if (isPurchaseOpen || isSellOpen)
		{
			moneyUI.ShowMoney(moneyPanelOnShop);
		}
	}
	public void HideMoney()
	{
		moneyUI.HideMoney();
	}
	#endregion

	#region PauseOptionUI Management
	public void ShowPauseOptionPanel()
	{
		pauseOptionUI.gameObject.SetActive(true);
	}

	#endregion

	// General UI State Check
	public bool IsAnyUIOpen()
	{
		return isInventoryOpen || isPurchaseOpen || isDialogOpen || isOptionOpen || isSellOpen || isPauseOptionOpen;
	}
}
