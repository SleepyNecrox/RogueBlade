using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public GameObject Keys;

    public bool keyStatus;

    AudioManager audiomanager;

    private void Start()
    {
        keyStatus = false;
    }

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    Exit exit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
             if (Keys != null)
            {
                audiomanager.PlaySFX(audiomanager.Collect);
                keyStatus = true;
                Keys.SetActive(false);
                
            }
        }
     }
}

