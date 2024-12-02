using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{
	private Player player;
	public Inventory inventory;

	private void Start()
	{
		player = FindObjectOfType<Player>();
		inventory = FindObjectOfType<Inventory>();
		player.OnItemCollected += inventory.AddItem;
	}
}
