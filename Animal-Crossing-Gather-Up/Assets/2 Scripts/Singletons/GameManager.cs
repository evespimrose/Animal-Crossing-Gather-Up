using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{
	public Player player;
	public Inventory inventory;
	public MMFCamera mmfCamera;

    private void Start()
	{
		player = FindObjectOfType<Player>();
		inventory = FindObjectOfType<Inventory>();
        mmfCamera = FindAnyObjectByType<MMFCamera>();

        player.OnItemCollected += inventory.AddItem;
	}
}
