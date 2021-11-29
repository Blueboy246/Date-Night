using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton static variable
    public static GameManager GM;
    //Our UI
    public TextMeshPro Readout;
    //Lists of all turn takers
    //Make sure to populate this in the editor
    public List<TurnTakerController> Everyone;
    public List<TurnTakerController> PendingTurns;
    //A link to the player character, so NPCs know who to attack
    public PlayerController Player;
    
    void Awake()
    {
        //Set our singleton so we're findable
        GM = this;
    }

    private void Start()
    {
        //Start the turn-taking process
        //We only need to call this in Start()--it'll loop itself once called once
        NextTurn();
    }

    public void NextTurn()
    {
        //Clean up the UI now that the old turn ended
        Readout.text = "";
        //If we've finished a full round, add everyone back to the turn order
        if (PendingTurns.Count == 0)
            PendingTurns.AddRange(Everyone);
        //Figure out who's next, then kick off their turn
        TurnTakerController current = PendingTurns[0];
        StartCoroutine(RunTurn(current));
    }
    
    public IEnumerator RunTurn(TurnTakerController who)
    {
        //Remove the current actor from the turn order
        PendingTurns.Remove(who);
        //And tell them to handle their turn
        yield return StartCoroutine(who.TakeTurn());
        //Once this is over, do the next turn
        NextTurn();
    }

}
