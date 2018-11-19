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
    Items item;
    [SerializeField]
    Image test;
    Vector3 stepXY = new Vector3(0.0f, 0.0f);
    float step = 150.0f;
    int jumpLine = 0;

    // Use this for initialization
    void Start ()
    {
        //itemsInventory = new List<Items>();

        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Debug.Log("coucou1"); 

        for (int i = 0; i < itemsInventory.Count; i++)
        {
            Image go = Instantiate(test, this.transform);
            go.rectTransform.localPosition = stepXY;
            stepXY.x += step;
            go.sprite = itemsInventory[i].GetComponent<Image>().sprite;

            jumpLine++;
            if (jumpLine %5 == 0)
            {
                stepXY.y += -step;
                stepXY.x = 0.0f;
            }
        }
        gameObject.SetActive(false);
    }

    public void AddItemInventory(Items[] item)
    {
        if (item.Length == 1)
        {
            itemsInventory.Add(item[0]);
            Image go = Instantiate(test, transform);
            go.rectTransform.localPosition = stepXY;
            stepXY.x += step;

            go.sprite = item[0].GetComponent<Image>().sprite;

            jumpLine++;
            if (jumpLine % 5 == 0)
            {
                stepXY.y += -step;
                stepXY.x = 0.0f;
            }
        }
        else if (item.Length > 1)
        {
            for (int i = 0; i < item.Length; i++)
            {
                itemsInventory.Add(item[i]);

                Image go = Instantiate(test, transform);
                go.rectTransform.localPosition = stepXY;
                stepXY.x += step;

                go.sprite = item[i].GetComponent<Image>().sprite;

                jumpLine++;
                if (jumpLine % 5 == 0)
                {
                    stepXY.y += -step;
                    stepXY.x = 0.0f;
                }
            }

            
        }
        else
        {
            Debug.LogError("Error : There is a problem with the item you try to add to the inventory");
        }
    }
}
