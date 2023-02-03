using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IReceivePlayerEffect
{
    public PlayerStatus PlayerStatus => _playerStatus;

    [SerializeField] private PlayerStatus _playerStatus = new();
    [SerializeField] private PlayerAnim _playerAnim = new();
    [SerializeField] private GameObject _playerDownPrefab;
    

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
        if (0 <= _playerStatus.CurrentHpNum - damage)
        {
            _playerStatus.ChangeValueHealth(_playerStatus.CurrentHpNum - damage);
            _playerAnim.DamageAnim();
        }
        else 
        {
            _playerAnim.DownAnim();
           
        }
    }

    public void DrawCard()
    {
        _playerStatus.DrawCard();
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
