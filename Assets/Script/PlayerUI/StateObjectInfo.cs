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
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
             text.text = hitInfo.collider.gameObject.name;
        }else
        {
            text.text = "";
        }
    }

    
}
