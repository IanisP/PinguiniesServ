using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlayer : MonoBehaviour
{

    [SerializeField]
    DataManager.ROOM room;

    // Use this for initialization
    void Start()
    {
        if (DataManager.instance)
        {
            if (room == DataManager.instance.room)
            {
                PlayerController.instance.transform.position = transform.position;
            }
        }
    }

}
