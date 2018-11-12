using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarOnUI : MonoBehaviour
{
    [SerializeField]
    Image [] spritesRadarParts;

    List<Image> images;

    private void Start()
    {
        images = new List<Image>();
        images.Clear();
    }

    public void ChangeRadarZone(List<directionRadar> _zones)
    {
        int value = RadarManager.nbRoundMax - RadarManager.nbRound;
        if (images.Count > 0)
        {
            for (int i = 0; i < images.Count; i++)
            {
                images [i].color = Color.white;
            }
            images.Clear();
        }
        for (int i = 0; i < _zones.Count; i++)
        {
            if (value > RadarManager.nbRoundMax / 3 * 2)
            {
                spritesRadarParts [(int) _zones [i] * 3].color = Color.green;
                images.Add(spritesRadarParts [(int) _zones [i] * 3]);
            }
            else if (value > RadarManager.nbRoundMax / 3)
            {
                spritesRadarParts [((int) _zones [i] * 3) + 1].color = Color.yellow;
                images.Add(spritesRadarParts [((int) _zones [i] * 3) + 1]);
            }
            else
            {
                spritesRadarParts [((int) _zones [i] * 3) + 2].color = Color.red;
                images.Add(spritesRadarParts [((int) _zones [i] * 3) + 2]);
            }
        }
    }
}
