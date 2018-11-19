using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField]
    public float speed = 1;

    [SerializeField]
    Sprite[] Apparence;

    Rigidbody2D rb;


    //public int life;
    //public int dmg;

    //bool inCombat = true;
    //public bool myturn = true;
    public DataManager.SENS sens = DataManager.SENS.Gauche; //0gauche 1droite 2 haut 3 bas
    public bool isBusy = false; // true == player can't move
    public bool inMenu = false;
    // Use this for initialization
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (inCombat)
        //{
        //    if (myturn)
        //    {

        //    }
        //}
        //else
        //{

        //}
        DataManager.SpriteSortingLayer(gameObject);
        if (!isBusy)
        {
            Deplacement();
            Action();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CameraController.canMove = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CameraController.onTrigger == false)
        {
            CameraController.canMove = true;
        }
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
            sens = DataManager.SENS.Droite;
            movement.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ speed;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sens = DataManager.SENS.Gauche;
            movement.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ speed;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            sens = DataManager.SENS.Haut;
            movement.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ speed;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            sens = DataManager.SENS.Bas;
            movement.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * /*(Input.GetButton("Run") ? 2 * speed :*/ speed;
        }
        if (Apparence.Length > 0)
        {
            GetComponent<SpriteRenderer>().sprite = Apparence[(int)sens];
        }

        rb.velocity = movement;
    }

    //Action execute when key is pressed (menu personnage)
    private void Action()
    {

    }
}
