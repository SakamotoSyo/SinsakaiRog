using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceivePlayerEffect
{
    /// <summary>
    /// ƒ_ƒ[ƒW‚ğó‚¯‚éˆ—
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage);

    public void DrowCard();

    public bool UseCost(float useCost);

    public void ResetCost();
}
