using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour {

    [SerializeField]
    GameObject inventory;

    [SerializeField]
    Items[] items;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
            }
            else
            {
                inventory.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Inventory.instance.AddItemInventory(items);
        }
	}
}
