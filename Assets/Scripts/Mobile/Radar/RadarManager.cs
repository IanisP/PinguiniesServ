using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum directionRadar
{
    east,
    south_east,
    south,
    south_west,
    west,
    north_west,
    north,
    north_east,
}

public enum zoneRadar
{
    east_green,
    east_orange,
    east_red,
    south_east_green,
    south_east_orange,
    south_east_red,
    south_green,
    south_orange,
    south_red,
    south_west_green,
    south_west_orange,
    south_west_red,
    west_green,
    west_orange,
    west_red,
    north_west_green,
    north_west_orange,
    north_west_red,
    north_green,
    north_orange,
    north_red,
    north_east_green,
    north_east_orange,
    north_east_red,
}


public class RadarManager : MonoBehaviour
{
    public static RadarManager instance;

    public int nbRound;
    public int nbRoundMax;
    public directionRadar myDirectionRadar;
    RadarOnUI radarUI;
    RadarUpgrades radarUpgrades;
    List<directionRadar> zones = new List<directionRadar>();
    [SerializeField] Text infoRadar;
    // Use this for initialization
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        radarUI = GetComponent<RadarOnUI>();
        radarUpgrades = GetComponent<RadarUpgrades>();
        myDirectionRadar = directionRadar.north;
        nbRound = 10;
        nbRoundMax = 10;

        zones.Clear();
        GetRandomZoneRadar();

        infoRadar.text = "Default";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitRadar(MessageInitRadar msg)
    {

    }

    public void UpdateRadar(MessageUpdateRadar msg)
    {
        infoRadar.text = msg.textInfoRadar;
    }

    void GetRandomZoneRadar()
    {
        int nbZonesToGet = 9 - radarUpgrades.lvlRadar;
        int nbZoneLeft = Random.Range(0, nbZonesToGet);
        int tempo = 0;
        zones.Clear();
        for (int i = 0; i < nbZoneLeft; i++)
        {
            if ((int) myDirectionRadar - i <= 0)
            {
                zones.Add((directionRadar) ((int) directionRadar.north_east - tempo));
                tempo++;
            }
            else
            {
                zones.Add(myDirectionRadar - 1 - i); //Le -1 est pour ne pas dupliquer la zone d'origine
            }
        }
        tempo = 0;
        for (int i = 0; i < nbZonesToGet - nbZoneLeft; i++)
        {
            if ((int) myDirectionRadar + i > (int) directionRadar.north_east)
            {
                zones.Add((directionRadar) ((int) directionRadar.east + tempo));
                tempo++;
            }
            else
            {
                zones.Add(myDirectionRadar + i);
            }
        }
        tempo = 0;
    }
}
