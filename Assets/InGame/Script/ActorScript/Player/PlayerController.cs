using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public IPlayerStatus PlayerStatus => _playerStatus;

    private IPlayerStatus _playerStatus;
    [SerializeField] private PlayerAnim _playerAnim = new();
    [SerializeField] private GameObject _playerDownPrefab;
    //TODO:参照はIplayerStatusから持ってくる;
    private IStatusBase _statusBase;

    private void Start()
    {
        _playerStatus = PlayerPresenter.PlayerStatus;
        _statusBase = _playerStatus.GetStatusBase();
    }

    /// <summary>
    /// ダメージを受ける処理の流れ
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        if (_statusBase.DownJudge(damage))
        {
            _statusBase.AddDamage(damage);
            _playerAnim.DamageAnim();
        }
        else 
        {
            _playerAnim.DownAnim();
           
        }
    }

    public void DefenseIncrease(float num) 
    {
        if (_statusBase.GetDefenceNum() <= 0) 
        {
            _playerAnim.ActiveDefence();
        }
        _statusBase.DefenseIncrease(num);
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
