using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpPlatform : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(Vector2.up * 150, ForceMode.Impulse);
        }
    }
}
