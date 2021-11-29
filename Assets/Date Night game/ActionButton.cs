using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public Actions Type;
    
    private void OnMouseDown()
    {
        GameManager.GM.Player.ChosenAction = Type;
    }
}
