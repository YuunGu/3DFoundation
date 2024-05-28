using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateObjectInfo : MonoBehaviour
{
    public Camera camera;

    public TMP_Text text;

    public void check()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
             text.text = hitInfo.collider.gameObject.name;
        }else
        {
            text.text = "";
        }
    }

    
}
