using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement : MonoBehaviour
{
   
    private Vector3 startingPosition;
    [SerializeField] private float path=6f;
    [SerializeField] private float step=0.25f;
    [SerializeField] private bool moveSideways = true;
    private void Awake()
    {
        startingPosition = transform.position;
    }
    private void Update()
    {
        if (moveSideways) LeftRight();
        else UpDown();
        
            
    }

    private void UpDown()
    {
        float diff = Mathf.Abs(startingPosition.y - transform.position.y);
        if (diff >= 0f && diff <= path)
            transform.position = new Vector3(transform.position.x , transform.position.y + step);
        else if (diff >= 0f)
        {
            step *= -1;
            transform.position = new Vector3(transform.position.x , transform.position.y + step); ;
        }
    }
    private void LeftRight()
    {
        float diff = Mathf.Abs(startingPosition.x - transform.position.x);
        if (diff >= 0f && diff <= path)
            transform.position = new Vector3(transform.position.x + step, transform.position.y);
        else if (diff >= 0f)
        {
            step *= -1;
            transform.position = new Vector3(transform.position.x + step, transform.position.y); ;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = collision.gameObject;
        player.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject player = collision.gameObject;
        player.transform.parent = null;
    }
}
