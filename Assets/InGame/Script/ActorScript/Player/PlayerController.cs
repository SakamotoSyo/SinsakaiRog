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
    /// ƒ_ƒ[ƒW‚ğó‚¯‚éˆ—‚Ì—¬‚ê
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        if (0 <= _playerStatus.CurrentHpNum + _playerStatus.DefenceNum - damage)
        {
            _playerStatus.AddDamage(damage);
            _playerAnim.DamageAnim();
        }
        else 
        {
            _playerAnim.DownAnim();
           
        }
    }

    public void DefenseIncrease(float num) 
    {
        if (_playerStatus.DefenceNum <= 0) 
        {
            _playerAnim.ActiveDefence();
        }
        _playerStatus.DefenseIncrease(num);
    }  

    public void DrawCard(float num = 1)
    {
        _playerStatus.DrawCard(num);
    }

    public bool UseCost(float useCost)
    {
        if (_playerStatus.UseCost(useCost)) 
        {
            return true;
        }
        return false;
    }

    public void ResetCost()
    {
        _playerStatus.ResetCost();
    }
}
