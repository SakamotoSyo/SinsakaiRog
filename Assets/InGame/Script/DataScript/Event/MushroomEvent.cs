using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomEvent : IEventSelect
{
    public void IfNotSelected()
    {

    }

    public void IfSelected(Text text, EventSelectDataScript eventData)
    {
        var playerStatus = PlayerEventPresenter.PlayerStatus;
        var num = Random.Range(0, 100);
        if (eventData.ProbabilitySuccess < num)
        {
            playerStatus.GetStatusBase().Healing(10);
            Debug.Log(eventData.EventResults[0]);
            text.text = eventData.EventResults[0];
        }
        else 
        {
            playerStatus.GetStatusBase().AddDamage(10);
            text.text = eventData.EventResults[1];
        }
       
    }
}