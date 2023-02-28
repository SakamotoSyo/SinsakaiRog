using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPresenterName;

public class PlayerController : MonoBehaviour
{
    public IPlayerStatus PlayerStatus => _playerStatus;

    private IPlayerStatus _playerStatus;
    [SerializeField] private PlayerAnim _playerAnim = new();
    [SerializeField] private GameObject _playerDownPrefab;
    [SerializeField] private GameObject _cardInsPos;
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

    /// <summary>
    /// カードをドローする流れ
    /// </summary>
    /// <param name="num">ドローする枚数</param>
    public void DrawCard(float num = 1)
    {
        _playerStatus.DrawCard(num);
    }

    /// <summary>
    /// ゴールドをゲットする一連の流れの処理
    /// </summary>
    public void AddReWardGold(float gold)
    {
        _playerStatus.AddGold(gold);
    }

    public void PlayerTurnEnd() 
    {
       _playerStatus.DiscardAllHandCards();
       var controllerArray = _cardInsPos.GetComponentsInChildren<CardController>();
       for(int i = 0; i < controllerArray.Length; i++) 
        {
            controllerArray[i].ThrowCard();
        }
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
