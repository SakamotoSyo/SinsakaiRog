using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IReceivePlayerEffect
{
    public PlayerStatus PlayerStatus => _playerStatus;

    [SerializeField] private PlayerStatus _playerStatus = new();
    [SerializeField] private PlayerAnim _playerAnim = new();

    private void Awake()
    {
        _playerStatus.Init();
    }

    /// <summary>
    /// ƒ_ƒ[ƒW‚ğó‚¯‚éˆ—
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage) 
    {
        _playerStatus.ChangeValueHealth(_playerStatus.CurrentHpNum - damage);
        _playerAnim.DamageAnim();
    }

    public void DrowCard() 
    {
        _playerStatus.DrowCard();
    }

    public bool UseCost(float useCost)
    {
        return _playerStatus.UseCost(useCost);
    }

    public void ResetCost()
    {
        _playerStatus.ResetCost();
    }
}
