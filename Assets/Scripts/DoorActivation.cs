using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivation : MonoBehaviour
{
    [SerializeField] private Sprite spriteAfter;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteAfter;
        audioSource.Play();
    }
}
