using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTakerController : MonoBehaviour
{
    //Sometimes I need know where on the screen to stand
    public Vector3 StartSpot;

    private void Start()
    {
        //Record where I started
        StartSpot = transform.position;
    }

    public virtual IEnumerator TakeTurn()
    {
        //Virtual function for handling a character's turn
        yield return null;
    }

    public IEnumerator HandleAction(Actions act, TurnTakerController target)
    {
        //Feed this function an action enum and a target and it'll resolve the action
        if (act == Actions.Attack)
            yield return StartCoroutine(AttackAction(target));
        if (act == Actions.Dance)
            yield return StartCoroutine(DanceAction(target));
    }

    //Code for running up and attacking someone
    public IEnumerator AttackAction(TurnTakerController target)
    {
        GameManager.GM.Readout.text = name + " attacks " + target.name;
        float speed = 1f;
        while (Vector3.Distance(transform.position, target.transform.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed*Time.deltaTime);
            speed *= 1.1f;
            yield return null;
        }
        GameManager.GM.Readout.text = name + " hits " + target.name + " for 1 damage!";
        while (Vector3.Distance(transform.position, StartSpot) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position,StartSpot,3f*Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position,StartSpot,0.01f);
            yield return null;
        }
        transform.position = StartSpot;
    }
    
    //Code for dancing in place
    public IEnumerator DanceAction(TurnTakerController target)
    {
        GameManager.GM.Readout.text = name + " dances at "+target.name+"!";
        float rotation = 0;
        while (rotation < 360 * 5)
        {
            transform.rotation = Quaternion.Euler(0,0,rotation);
            rotation += 5;
            yield return null;    
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

public enum Actions{
    None,
    Attack,
    Dance
}