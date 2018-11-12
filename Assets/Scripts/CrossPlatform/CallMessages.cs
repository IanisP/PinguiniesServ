using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum valueMessage
{
    //A virer quand ça sera refait
    onCommandSent = 123,
    //A virer quand ça sera refait
    OnNetInitialisationRadar = 124,
    OnNetTakeLanguageString = 125,
}

public class MessageInitRadar : MessageBase //A appeler à l'instantiation d'un nouveau groupe de mob
{
    public string description;              //la description, jusqu'à "Ils seront là dans "
    public int nbRound;                     //nombre de tour avant l'arrivée
 //   public directionRadar directionRadar;   //La direction d'arivée des mobs
}

public class SettingsRadar : MessageBase //A appeler à l'initialisation ou à la modification du radar, le met à jour sur le téléphone
{
    public int levelRadar;                  //niveau général du radar
    public bool hasFishingUpgrade;          //Si l'amélioration de pêche à été débloqué
}

//A virer quand ça sera refait
public class MessageManager : MessageBase //Peut servir à tout
{
    public string msgString;
    public int msgInt;
    public float msgFloat;
}


public class CallMessages
{
    /// Message pour l'ordinateur
    ///

    //A virer quand ça sera refait
    public static void OnCommandSent(NetworkMessage msg)
    {
        MessageManager myMsg = msg.ReadMessage<MessageManager>();

    }



    /// Message pour le téléphone
    /// 
    
    public static void OnNetInitialisationRadar(NetworkMessage msg)
    {
        SettingsRadar settingsRadar = msg.ReadMessage<SettingsRadar>();

    }
}
