using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : MonoBehaviour
{
    public float speed;
    public float highLow;
    public float maxHigh;
    public float minLow;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        if (rb.transform.position.y >= maxHigh) highLow = -Mathf.Abs(highLow);
        else if (rb.transform.position.y <= minLow) highLow = Mathf.Abs(highLow);
        rb.velocity = new Vector3(rb.velocity.x, highLow * speed, rb.velocity.z);
    }
}
