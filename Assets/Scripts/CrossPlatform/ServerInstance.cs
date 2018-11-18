using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using ZXing;
using ZXing.QrCode;



public class ServerInstance : MonoBehaviour
{
    NetworkClient mobileDevice;
    //string externalIP;
    private void Start()
    {
        NetworkServer.Listen(7777);
        OnRegister();

        //externalIP = IPManager.GetIP(ADDRESSFAM.IPv4);
    }

    private void OnRegister()
    {
        NetworkServer.RegisterHandler((int) valueMessage.onCommandSent, CallMessages.OnCommandSent);

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MessageManager msg = new MessageManager();
            msg.msgString = "osef";
            NetworkServer.SendToAll((int) valueMessage.OnNetInitialisationRadar, msg);
        }
    }





























    /// <summary>
    /// Generation du QR Code
    /// </summary>
    private void OnGUI()
    {
        Texture2D myQR = generateQR(IPManager.GetIP(ADDRESSFAM.IPv4));
        //Texture2D myQR = generateQR(externalIP);

        if (GUI.Button(new Rect(Screen.width / 2 - 128, Screen.height / 2 - 128, 256, 256), myQR, GUIStyle.none)) { }
    }
    private static Color32 [] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }
}
/// <summary>
/// Manager d'adresse IP
/// </summary>
public enum ADDRESSFAM
{
    IPv4, IPv6
}

public class IPManager
{
    public static string GetIP(ADDRESSFAM Addfam)
    {
        //Return null if ADDRESSFAM is Ipv6 but Os does not support it
        if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }
        string output = "";
        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;
            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    //IPv4
                    if (Addfam == ADDRESSFAM.IPv4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                    //IPv6
                    else if (Addfam == ADDRESSFAM.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
        }
        return output;
    }

    public static string GetPublicIP()
    {
        string url = "http://checkip.dyndns.org";
        System.Net.WebRequest req = System.Net.WebRequest.Create(url);
        System.Net.WebResponse resp = req.GetResponse();
        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
        string response = sr.ReadToEnd().Trim();
        string [] a = response.Split(':');
        string a2 = a [1].Substring(1);
        string [] a3 = a2.Split('<');
        string a4 = a3 [0];
        return a4;
    }
}