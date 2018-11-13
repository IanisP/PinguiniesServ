using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringAssemblyRadar : MonoBehaviour
{
    public string ConcateneString()
    {
        int nbFoes = 3;
        int lvlRadar = RadarInfos.levelRadar;

        string str = DataManager.instance.myTabRADAR [(int) DataManager.instance.language + 1, 1] + "\n";
        if (lvlRadar >= 2)
        {
            str += DataManager.instance.myTabRADAR [(int) DataManager.instance.language + 1, 2];
            str += "128 ";
            if (lvlRadar >= 3)
            {
                str += DataManager.instance.myTabRADAR [(int) DataManager.instance.language + 1, 3];
                str += "3 ";
                str += DataManager.instance.myTabRADAR [(int) DataManager.instance.language + 1, 4] + "\n";
            }
            else
            {
                str += ".";
            }
        }
        if (lvlRadar >= 5)
        {
            str += DataManager.instance.myTabRADAR [(int) DataManager.instance.language + 1, 5] + "\n";
            for (int i = 0; i < nbFoes; i++)
            {
                str += DataManager.instance.myTabRADAR [(int) DataManager.instance.language + 1, 6];
                str += "tiramisouris";
                if (lvlRadar >= 6)
                {
                    str += " ";
                    str += DataManager.instance.myTabRADAR [(int) DataManager.instance.language + 1, 7];
                    str += "42.\n";
                }
                else
                {
                    str += ".\n";
                }
            }
            str += "\n";
        }
        if (lvlRadar == 8)
        {
            str += DataManager.instance.myTabRADAR [(int) DataManager.instance.language + 1, 8];
            str += "42 ";
            str += DataManager.instance.myTabRADAR [(int) DataManager.instance.language + 1, 9];
        }
        return str;
    }
    private void Start()
    {
        ConcateneString();
    }
}
