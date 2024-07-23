using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    private GameObject currentTeleporter;
    private float teleportCooldown = 1f; 
    private float lastTeleportTime;
    [SerializeField] private TrailRenderer tr;
    private Transition transition;
    private Rigidbody2D rb;

    private void Awake()
    {
        transition = FindObjectOfType<Transition>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && CanTeleport())
        {
            if (currentTeleporter != null)
            {
                transition.StartTransition();
                FreezeRigidbody();
                tr.enabled = false;
                StartCoroutine(Wait());
            }
        }
    }

    private bool CanTeleport()
    {
        return Time.time - lastTeleportTime >= teleportCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
        lastTeleportTime = Time.time;
        tr.enabled = true;
        UnfreezeRigidbody();
    }

    private void FreezeRigidbody()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void UnfreezeRigidbody()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
