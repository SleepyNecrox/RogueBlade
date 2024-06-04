using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerEnd : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip Intro;

    public AudioClip Menu;
    
    public AudioClip Game;


    public AudioClip Outro;
   
    public AudioClip Collect;

    public AudioClip Jump ;

    public AudioClip Spring ;


    public AudioClip Dash;


    private void Start()
    {

            PlaySceneMusic();
    }

    private void PlaySceneMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {

            case "00 Lorenzo Valeda":
                musicSource.clip = Intro;
                break;

            case "01 Main Menu":
                musicSource.clip = Menu;
                break;

            case "02 Tutorial":
                musicSource.clip = Game;
                break;

            case "03.1 Level":
                musicSource.clip = Game;
                break;

            case "03.2 Level":
                musicSource.clip = Game;
                break;

            case "03.3 Level":
                musicSource.clip = Game;
                break;

            case "04 Demo Complete":
                musicSource.clip = Outro;
                break;
 
            default:
                musicSource.clip = Game;
                break;
        }

        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}


