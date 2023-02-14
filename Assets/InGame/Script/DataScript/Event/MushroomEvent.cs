using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEvent : IEventSelect
{
    public void IfNotSelected()
    {
       
    }

    public void IfSelected()
    {
        var playerStatus = PlayerEventPresenter.PlayerStatus;
        playerStatus.GetStatusBase().Healing(10);
        Debug.Log("10‚Ìƒ_ƒ[ƒW‚Ì’Ç‰Á");
    }
}
