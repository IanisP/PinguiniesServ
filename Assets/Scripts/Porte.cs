using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Porte : MonoBehaviour
{

    [SerializeField]
    DataManager.ROOM destinationRoom;
    [SerializeField]
    DataManager.ROOM actualRoom;

    [SerializeField]
    DataManager.SENS sens;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (PlayerController.instance.sens == sens)
            {
                DataManager.instance.room = actualRoom;
                SceneManager.LoadScene(destinationRoom.ToString());
            }
        }
    }
}
