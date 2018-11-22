using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {


    static public Inventory instance;

    public enum ItemType
    {
        heal,
        stamina,
        buff,
        debuff,
        damage,
    }

    [SerializeField]
    List<Items> itemsInventory;

    List<GameObject> listInventory;
    

    //[SerializeField]
    //GameObject test;
    Vector3 stepXY = new Vector3(0.0f, 0.0f);
    float step = 150.0f;
    int jumpLine = 0;

    // Use this for initialization
    void Start ()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        listInventory = new List<GameObject>();
        

        for (int i = 0; i < itemsInventory.Count; i++)
        {
            GameObject go = Instantiate(itemsInventory[i].gameObject, this.transform);
            listInventory.Add(go);
            go.GetComponent<Image>().rectTransform.localPosition = stepXY;
            stepXY.x += step;
            go.GetComponent<Image>().sprite = itemsInventory[i].GetComponent<Image>().sprite;

            jumpLine++;
            if (jumpLine % 5 == 0)
            {
                stepXY.y += -step;
                stepXY.x = 0.0f;
            }
        }
        gameObject.SetActive(false);
    }

    public void AddItemInventory(Items item)
    {
        if (item.maxStack >= 1 && item.stack < item.maxStack)
        {
            for (int i = 0; i < listInventory.Count; i++)
            {
                if (listInventory[i] == item)
                {
                    listInventory[i].GetComponent<Items>().stack += item.stack;

                    if (listInventory[i].GetComponent<Items>().stack > listInventory[i].GetComponent<Items>().maxStack)
                    {
                        listInventory[i].GetComponent<Items>().stack = listInventory[i].GetComponent<Items>().maxStack;
                    }
                    return;
                }
            }
            listInventory.Add(item.gameObject);
        }
        else
        {
            Debug.LogError("Error : There is a problem with the item you try to add to the inventory");
        }
    }

    public void DestroyItemAndSort()//vérifier autre part si un item est à 0 puis parcourir la liste, le remove et le sort
    {

        for (int i = 0; i < listInventory.Count; i++)
        {
            if (listInventory[i].GetComponent<Items>().stack <= 0)
            {
                Destroy(listInventory[i]);
                listInventory.RemoveAt(i);
                return;
            }
        }

        listInventory.Sort();
    }
}
