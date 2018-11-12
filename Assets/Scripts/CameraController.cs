using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    PlayerController player;
    // Use this for initialization
    void Start()
    {
        player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = player.transform.position;
        Deplacement();
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

            // movement.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ speed;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //movement.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ speed;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            //movement.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ speed;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            //movement.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ speed;
        }

        GetComponent<Rigidbody2D>().velocity = movement;
    }
}
