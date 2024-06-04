using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        StartCoroutine(Wait());
        Application.Quit();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
}
