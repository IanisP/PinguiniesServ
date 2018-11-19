using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJ : MonoBehaviour
{
    int nbTimeToSpeak = 0;


    public TextArray[] arrays;

    [SerializeField]
    bool isMoving;

    [SerializeField]
    TextBox tb;

    bool eventActiv = false;

    float timeTriggerEvent = 0;

    bool triggerPlayer = false;

    bool PNJIsBusy = false; // true, the character can't move

    float timeToMove = 2;
    float timeToStay = 1;
    int sensToMove;
    void DeplacementPNJ() //Mettre dans un script a part
    {
        timeToMove += Time.deltaTime;
        if (timeToMove < Random.Range(1.0f,4.0f))
        {
            Vector2 movement = Vector2.zero;
            switch (sensToMove)
            {
                case 0:
                    movement.x = Time.deltaTime * 50;
                    break;
                case 1:
                    movement.x = Time.deltaTime * -50;
                    break;
                case 2:
                    movement.y = Time.deltaTime * -50;
                    break;
                case 3:
                    movement.y = Time.deltaTime * 50;
                    break;
                default:
                    break;
            }
            GetComponent<Rigidbody2D>().velocity = movement;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            timeToStay += Time.deltaTime;
            if (timeToStay> Random.Range(1.0f, 4.0f))
            {
                timeToStay = 0;
                timeToMove = 0;
                sensToMove = Random.Range(0, 4);
            }
        }
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            triggerPlayer = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            triggerPlayer = false;
    }

    private void Update()
    {
        DataManager.SpriteSortingLayer(gameObject);
        if (!PNJIsBusy && isMoving)
        {
            DeplacementPNJ();
        }
        if (triggerPlayer)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (Input.GetButtonDown("Action") && !eventActiv && !PlayerController.instance.inMenu)
            {
                
                eventActiv = true;
                tb.keys = arrays[nbTimeToSpeak].keys;
                nbTimeToSpeak++;
                if (nbTimeToSpeak == arrays.Length)
                {
                    nbTimeToSpeak--;
                }
                PlayerController.instance.isBusy = true;
                tb.triggerEvent = true;
                tb.gameObject.SetActive(true);
            }
        }
        if (eventActiv)
        {
            if (timeTriggerEvent < 0.2f)
            {
                timeTriggerEvent += Time.deltaTime;
            }
            else if (tb.triggerEvent)
            {
                tb.triggerEvent = false;
            }


            //PlayerController.instance.isBusy = true;
            //tb.gameObject.SetActive(true);
            //tb.key = key[0];
            if (!tb.gameObject.activeSelf)
            {
                eventActiv = false;
                PlayerController.instance.isBusy = false;
                timeTriggerEvent = 0;
            }
        }

    }
}
