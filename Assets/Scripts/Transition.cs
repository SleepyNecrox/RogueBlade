using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private Animator transition;

    private void Awake()
    {
        transition = GetComponent<Animator>();
    }

    internal void StartTransition()
    {
        StartCoroutine(TransitionScene());
    }

    IEnumerator TransitionScene()
    {
        transition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        transition.SetBool("End", true);
        yield return new WaitForSeconds(0.5f);
        transition.SetBool("Start", false);
        transition.SetBool("End", false);


    }

}
