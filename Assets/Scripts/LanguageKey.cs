using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageKey : MonoBehaviour {

    [SerializeField]
    int key = 0;

	// Use this for initialization
	void Start () {

        ChangeText();
    }
	
	// Update is called once per frame
	public void ChangeText() {
        Text t = GetComponent<Text>();

        t.text = DataManager.instance.myTabUI[(int)DataManager.instance.language + 1, key];
    }

    public void SetKey(int value)
    {
        key = value;
        ChangeText();
    }
}
