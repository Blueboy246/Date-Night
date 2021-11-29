using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : TurnTakerController
{
    public override IEnumerator TakeTurn()
    {
        //This is some real simple AI--just always attack
        //Decide what you want to do and who to target, then run HandleAction
        yield return StartCoroutine(HandleAction(Actions.Attack, GameManager.GM.Player));
    }
    
    private void OnMouseDown()
    {
        //If I get clicked on, tell the player
        //Make sure my GameObject has a collider, so this can get called
        GameManager.GM.Player.ChosenTarget = this;
    }
}
