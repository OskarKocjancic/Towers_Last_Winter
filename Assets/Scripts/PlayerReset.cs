using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    [SerializeField] private Transform targetLocation;
    private Collider2D colliderPlayer;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        colliderPlayer = collider;
        Invoke("movePlayer",0.4f);
    }

    public void movePlayer() {
        colliderPlayer.transform.position = targetLocation.position;
        Rigidbody2D rb = colliderPlayer.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
    }

}
