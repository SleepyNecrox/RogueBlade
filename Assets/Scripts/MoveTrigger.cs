using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    public SpriteRenderer Sprite;
    private void Start()
    {
        Sprite.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Debug.Log("Entered");
             if (Sprite != null)
            {
                Sprite.enabled = true;
            }
        }
    }

     private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
             if (Sprite != null)
            {
                Sprite.enabled = false;
            }
        }
    }
}
