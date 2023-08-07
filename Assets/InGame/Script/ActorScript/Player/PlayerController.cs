using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPresenterName;
using Cysharp.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public IPlayerStatus PlayerStatus => _playerStatus;
    public PlayerAnim PlayerAnim => _playerAnim;

    private IPlayerStatus _playerStatus;
    [SerializeField] private PlayerAnim _playerAnim = new();
    [SerializeField] private GameObject _playerDownPrefab;
    [SerializeField] private GameObject _cardInsPos;
    //TODO:�Q�Ƃ�IplayerStatus���玝���Ă���;
    private IStatusBase _statusBase;

    private void Start()
    {
        _statusBase = _playerStatus.GetStatusBase();
    }

    /// <summary>
    /// �_���[�W���󂯂鏈���̗���
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        AudioManager.Instance.PlaySound(SoundPlayType.EnemyAttack);
        if (_statusBase.DownJudge(damage))
        {
            _statusBase.AddDamage(damage);
            _playerAnim.DamageAnim();
        }
        else
        {
            _statusBase.AddDamage(damage);
            GameManager.ResetCurrentLevel();
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
    /// �J�[�h���h���[���闬��
    /// </summary>
    /// <param name="num">�h���[���閇��</param>
    public void DrawCard(float num = 1)
    {
        _playerStatus.DrawCard(num);
    }

    /// <summary>
    /// �S�[���h���Q�b�g�����A�̗���̏���
    /// </summary>
    public void AddReWardGold(float gold)
    {
        _playerStatus.AddGold(gold);
    }

    /// <summary>
    /// �^�[���̏I���Ɏ�D�̃J�[�h�����ׂĎ̂Ă鏈��
    /// </summary>
    public void PlayerTurnEnd() 
    {
       _playerStatus.DiscardAllHandCards();
       var controllerArray = _cardInsPos.GetComponentsInChildren<CardController>();
       for(int i = 0; i < controllerArray.Length; i++) 
        {
            controllerArray[i].ThrowCard().Forget();
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

    public void SetPlayerStatus(IPlayerStatus player) 
    {
        _playerStatus = player;
    }
}
