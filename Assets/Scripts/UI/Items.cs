using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    [SerializeField] Inventory.ItemType itemType;
    
    [SerializeField] int maxStack;
    [SerializeField] int stack;

    private void Start()
    {
        switch (itemType)
        {
            case Inventory.ItemType.heal:

                break;
            case Inventory.ItemType.stamina:

                break;
            case Inventory.ItemType.buff:

                break;
            case Inventory.ItemType.debuff:

                break;
            case Inventory.ItemType.damage:

                break;
        }
    }
}
