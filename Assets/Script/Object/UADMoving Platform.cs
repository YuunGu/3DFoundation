using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpDownMoveingPlatform : MonoBehaviour
{
    public float speed;
    public float cycle;
    private float maxY;
    private float minY;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        maxY = transform.position.y + 3;
        minY = transform.position.y+0.5f;
    }

    private void FixedUpdate()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        if (rb.transform.position.y >= maxY) cycle = -Mathf.Abs(cycle);
        else if (rb.transform.position.y <= minY) cycle = Mathf.Abs(cycle);
        rb.velocity = new Vector3(rb.velocity.x, cycle * speed, rb.velocity.z);
    }


}
