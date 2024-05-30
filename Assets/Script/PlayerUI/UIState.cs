using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState : MonoBehaviour
{
    public State hp;
    public StateObjectInfo info;
    public StateDash dash;

    void Start()
    {
        CharacterManager.Instance.Player.state.uiState = this;
        dash.AddDashCount(1);
    }
}
