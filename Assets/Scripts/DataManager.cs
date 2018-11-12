using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    public enum LANGUAGE
    {
        FRANCAIS,
        ENGLISH,
    }

    public enum SENS
    {
        Haut,
        Bas,
        Gauche,
        Droite,
    }

    public enum ROOM
    {
        Village,
        Maison1,
        Maison2,
    }

    public LANGUAGE language = LANGUAGE.FRANCAIS;

    public static DataManager instance;

    public TextAsset csvFilePNJ;
    public TextAsset csvFileUI;
    public TextAsset csvFileITEMS;
    public TextAsset csvFileENEMY;
    public TextAsset csvFileRADAR;

    public string [,] myTabPNJ;
    public string [,] myTabUI;
    public string [,] myTabITEMS;
    public string [,] myTabENEMY;
    public string [,] myTabRADAR;

    public ROOM room = ROOM.Village;

    /// 
    /// References to other scripts
    RadarInfos radarInfos;

    // splits a CSV file into a 2D string array
    static public string [,] SplitCsvGrid(string csvText)
    {
        string [] lines = csvText.Split("\n" [0]);

        // finds the max width of row
        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string [] row = SplitCsvLine(lines [i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        string [,] outputGrid = new string [width + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string [] row = SplitCsvLine(lines [y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid [x, y] = row [x];

                // This line was to replace "" with " in my output. 
                // Include or edit it as you wish.
                outputGrid [x, y] = outputGrid [x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    // splits a CSV row 
    static public string [] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups [1].Value).ToArray();
    }

    // Use this for initialization
    void Start()
    {
        //Load File With text
        myTabPNJ = SplitCsvGrid(csvFilePNJ.text);
        myTabUI = SplitCsvGrid(csvFileUI.text);
        myTabITEMS = SplitCsvGrid(csvFileITEMS.text);
        myTabENEMY = SplitCsvGrid(csvFileENEMY.text);
        myTabRADAR = SplitCsvGrid(csvFileRADAR.text);
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


        radarInfos = new RadarInfos();
    }

    public static void SpriteSortingLayer(GameObject go)
    {
        if (go.GetComponent<SpriteRenderer>())
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(go.transform.position.y * 100f) * -1;
        }
        else
        {

            foreach (SpriteRenderer sr in go.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.sortingOrder = Mathf.RoundToInt(go.transform.position.y * 100f) * -1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
