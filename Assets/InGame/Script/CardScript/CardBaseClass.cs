using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardBaseClass
{
    private int _id;
    private float _mainEffectPower;
    private float _subEffectPower;
    private float _cardDefence;
    private float _cardCost;
    public string Tartget => _target;
    private string _target;
    [Tooltip("�J�[�h�Ɋւ������")]
    private string _cardDescription;

    private PlayerStatus _playerStatus;
    private EnemyStaus _enemyStatus;

    [SerializeReference, SubclassSelector]
    List<ICardEffect> _effect = new List<ICardEffect>();


    public CardBaseClass(int id, float mainPower,float subPower, float defence, float cost, string target, string Description)
    {
        _id = id;
        _mainEffectPower = mainPower;
        _subEffectPower = subPower;
        _cardDefence = defence;
        _cardCost = cost;
        _target = target;
        _cardDescription = Description;
    }

    /// <summary>
    /// �ݒ肳�ꂽ�J�[�h�̌��ʂ��g��
    /// </summary>
    public void UseEffect(PlayerStatus player, EnemyStaus enemy) 
    {
        _playerStatus = player;
        _enemyStatus = enemy;
        //�J�[�h�ɐݒ肵�����ʂ����ɔ���
        for (int i = 0; i < _effect.Count; i++) 
        {
            _effect[i].UseEffect();
        }
    }
}
