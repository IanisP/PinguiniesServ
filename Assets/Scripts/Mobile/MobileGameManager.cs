using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public enum gameStates
{
    mainMenu,
    onQRReading,
    onPlay,
}

public class MobileGameManager : MonoBehaviour
{
    public static gameStates gameState;
    public static NetworkClient mobileDevice;

    // Use this for initialization
    void Start()
    {
        gameState = gameStates.mainMenu;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
