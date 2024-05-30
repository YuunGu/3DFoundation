using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BAFMovingPlatform: MonoBehaviour
{
    public float speed;
    public float cycle;
    private float maxX;
    private float minX;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        maxX = transform.position.x + 3;
        minX = transform.position.x + 0.5f;
    }

    private void FixedUpdate()
    {
        MoveObject();
        
    }

    private void MoveObject()
    {
        if (rb.transform.position.x >= maxX) cycle = -Mathf.Abs(cycle);
        else if (rb.transform.position.x <= minX) cycle = Mathf.Abs(cycle);
        rb.velocity = new Vector3(cycle * speed, rb.velocity.y, rb.velocity.z);
    }

    

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.velocity += rb.velocity;
            Vector3 playerPosition = collision.transform.position;
            playerPosition.x += cycle * speed * Time.fixedDeltaTime;
            collision.transform.position = playerPosition;
        }
    }
}
