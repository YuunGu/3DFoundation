using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateDash : MonoBehaviour
{
    public GameObject dashBar;

    public GameObject dashPrefab;

    private Queue<GameObject> dashList = new Queue<GameObject>();
    public void AddDashCount()
    {        
        GameObject dashCount = Instantiate(dashPrefab, dashBar.transform);
        dashList.Enqueue(dashCount);
    }

    public bool DashCheck()
    {
        if (dashList.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReturnDash()
    {
        GameObject removedDash = dashList.Dequeue();
        Destroy(removedDash);
    }
}
