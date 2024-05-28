using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public UIState uiState;

    State hp { get { return uiState.hp; } }
    StateObjectInfo info {  get { return uiState.info; } }

    void Update()
    {
        hp.Subtract(hp.passiveValue);
        if(info != null) info.check();
    }
}
