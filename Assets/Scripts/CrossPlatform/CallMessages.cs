using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum valueMessage
{
    //PC messages
    onCommandSent = 123,

    //Mobile messages
    OnNetInitialisationRadar,
    OnNetUpdateRadar,
}

public class MessageUpdateRadar : MessageBase //A appeler à l'instantiation d'un nouveau groupe de mob
{
    public string description;              //La description, jusqu'à "Ils seront là dans "
    public int nbRound;                     //Nombre de tour avant l'arrivée
                                            //   public directionRadar directionRadar;   //La direction d'arivée des mobs
    public string textInfoRadar;            //Le texte d'information du pack de mob
}

public class MessageInitRadar : MessageBase //A appeler à l'initialisation ou à la modification du radar, le met à jour sur le téléphone
{
    public int levelRadar;                  //Niveau général du radar
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

    public static void OnNetUpdateRadar(NetworkMessage msg)
    {
        MessageUpdateRadar msgRadar = msg.ReadMessage<MessageUpdateRadar>();
        RadarManager.instance.UpdateRadar(msgRadar);
    }

    public static void OnNetInitialisationRadar(NetworkMessage msg)
    {
        MessageInitRadar msgRadar = msg.ReadMessage<MessageInitRadar>();
        RadarManager.instance.InitRadar(msgRadar);

    }
}
