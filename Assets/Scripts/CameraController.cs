using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    bool onPlayer;
    public static bool canMove = true;
    public static bool onTrigger = false;
    static bool stopUp = false;
    static bool stopDown = false;
    static bool stopRight = false;
    static bool stopLeft = false;
    PlayerController player;
    // Use this for initialization
    void Start()
    {
        player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //  transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.1f);
        //transform.position = player.transform.position;
        if (!onPlayer)
        {
            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.1f);
            }
            else
            {
                if (stopDown && stopRight || stopDown && stopLeft || stopUp && stopRight || stopUp && stopLeft)
                {

                }
                else
                {
                    switch (PlayerController.instance.sens)
                    {
                        case DataManager.SENS.Haut:
                            if (!stopDown && !stopUp)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z), 0.1f);
                            }
                            break;
                        case DataManager.SENS.Bas:
                            if (!stopDown && !stopUp)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z), 0.1f);
                            }
                            break;
                        case DataManager.SENS.Gauche:
                            if (!stopLeft && !stopRight)
                            {

                                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), 0.1f);
                            }
                            break;
                        case DataManager.SENS.Droite:
                            if (!stopLeft && !stopRight)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), 0.1f);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

          

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (PlayerController.instance.sens)
        {
            case DataManager.SENS.Haut:
                stopUp = true;
                break;
            case DataManager.SENS.Bas:
                stopDown = true;
                break;
            case DataManager.SENS.Gauche:
                stopLeft = true;
                break;
            case DataManager.SENS.Droite:
                stopRight = true;
                break;
            default:
                break;
        }

          Debug.Log("touch");
        Debug.Log("Haut :  " + stopUp + "/  Bas :  " + stopDown + "/  DROIT :  " + stopRight + "/  GAUCHE :  " + stopLeft);
        canMove = false;
        onTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (PlayerController.instance.sens)
        {
            case DataManager.SENS.Haut:
                stopDown = false;
                break;
            case DataManager.SENS.Bas:
                stopUp = false;
                break;
            case DataManager.SENS.Gauche:
                stopRight = false;
                break;
            case DataManager.SENS.Droite:
                stopLeft = false;
                break;
            default:
                break;
        }
             Debug.Log("exit");
        Debug.Log("Haut :  " + stopUp + "/  Bas :  " + stopDown + "/  DROIT :  " + stopRight + "/  GAUCHE :  " + stopLeft);
        canMove = true;
        onTrigger = false;
    }

    private void Deplacement()
    {
        /* //déplacement tout les cotés
        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * (Input.GetButton("Run") ? 2 * speed : speed); // run avec 2 ou 2.5f depend de ce qu'on fera
        movement.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * (Input.GetButton("Run") ? 2 * speed : speed);// run avec 2 ou 2.5f depend de ce qu'on fera
        rb.velocity = movement;
        */
        //déplacement 4 coté
        Vector2 movement = Vector2.zero;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {

            movement.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ PlayerController.instance.speed;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ PlayerController.instance.speed;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            movement.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ PlayerController.instance.speed;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            movement.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ PlayerController.instance.speed;
        }

        GetComponent<Rigidbody2D>().velocity = movement;
    }
}
