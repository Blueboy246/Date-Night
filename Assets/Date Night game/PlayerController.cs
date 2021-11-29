using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : TurnTakerController
{
    //A link to the UI that lets me pick an action
    public GameObject Choices;
    //The action I'm currently choosing to perform--None until set
    public Actions ChosenAction;
    //The target I'm currently choosing to act on--null until set
    public TurnTakerController ChosenTarget;

    public override IEnumerator TakeTurn()
    {
        //At the start of my turn, turn on my action choice buttons
        Choices.SetActive(true);
        //Also reset my chosen action/target
        ChosenAction = Actions.None;
        ChosenTarget = null;
        //And give me some UI telling me what to do
        GameManager.GM.Readout.text = "Choose your action!";
        while (ChosenAction == Actions.None)
        {
            //Wait for an action to get chosen
            //This happens in the ActionButton script
            yield return null;   
        }
        //Update the UI to clear out old buttons and tell me what to do
        GameManager.GM.Readout.text = "Choose your target!";
        Choices.SetActive(false);
        while (ChosenTarget == null)
        {
            //Wait for a target to get clicked on--this happens in EnemyController
            yield return null;
        }
        //Okay, we've picked both an action and a target! Do them!
        yield return StartCoroutine(HandleAction(ChosenAction, ChosenTarget));
    }
}