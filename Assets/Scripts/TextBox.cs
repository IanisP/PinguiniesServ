using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    int actualKey = 0;
    public int[] keys;
    public bool triggerEvent = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Action") && !triggerEvent)
        {
            actualKey++;
            if (actualKey == keys.Length)
            {
                gameObject.SetActive(false);
            }
            else
            {
                ChangeText();
            }
        }
    }

    public void ChangeText()
    {
        Text t = GetComponentInChildren<Text>();
        t.text = DataManager.instance.myTab[(int)DataManager.instance.language + 1, keys[actualKey]];
    }

    private void OnEnable()
    {
        actualKey = 0;
        ChangeText();
        PlayerController.instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
