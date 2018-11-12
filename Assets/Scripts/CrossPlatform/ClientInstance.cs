using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using ZXing;
using ZXing.QrCode;


public class ClientInstance : MonoBehaviour
{
    ///For QR Reading//////////////////////////////////////////////////////////////////
    private WebCamTexture camTexture;
    private Rect screenRect;
    ///For QR Reading//////////////////////////////////////////////////////////////////
    int delayQRReading = 0;


    // Use this for initialization
    void Start()
    {
        ///For QR Reading//////////////////////////////////////////////////////////////////
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
        ///For QR Reading//////////////////////////////////////////////////////////////////
    }

    // Update is called once per frame
    void Update() //A reprendre quand on aura de vraies states ou plusieurs scene
    {

        if (MobileGameManager.gameState == gameStates.onQRReading)
        {
            if (camTexture != null)
            {
                camTexture.Play();
            }
        }
        else if (MobileGameManager.gameState == gameStates.onPlay)
        {
            //Un exemple d'envoi de message au serveur
            if (Input.GetMouseButtonDown(0))
            {
                MessageManager msg = new MessageManager();
                msg.msgString = "\nis working";
                MobileGameManager.mobileDevice.Send((int) valueMessage.onCommandSent, msg);
            }
        }
    }



















    /// <summary>
    /// QR Reading and client initialisation
    /// </summary>
    void OnGUI()
    {
        if (MobileGameManager.gameState == gameStates.onQRReading)
        {
            // drawing the camera on screen
            GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
            // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
            delayQRReading++;
            if (delayQRReading >= 100)
            {
                delayQRReading = 0;
                try
                {
                    IBarcodeReader barcodeReader = new BarcodeReader();
                    // decode the current frame
                    var result = barcodeReader.Decode(camTexture.GetPixels32(),
                      camTexture.width, camTexture.height);
                    if (result != null)
                    {
                        MobileGameManager.mobileDevice = new NetworkClient();
                        OnRegisteredMobile();
                        MobileGameManager.mobileDevice.Connect(result.Text, 7777);
                        MobileGameManager.gameState = gameStates.onPlay;
                    }
                }
                catch (Exception ex) { Debug.LogWarning(ex.Message); }
            }
        }
    }
    void OnRegisteredMobile()
    {
        MobileGameManager.mobileDevice.RegisterHandler((int) valueMessage.OnNetInitialisationRadar, CallMessages.OnNetInitialisationRadar);
    }
}

