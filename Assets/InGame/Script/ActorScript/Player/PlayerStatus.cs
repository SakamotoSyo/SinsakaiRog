using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


[Serializable]
public class PlayerStatus : StatusModelBase, IPlayerStatus
{
    public float MaxCost => _maxCost;
    public float Cost => _cost.Value;
    public IObservable<float> CostOb => _cost;
    public IReactiveCollection<CardBaseClass> DiscardedCount => _discardedCard;
    public IReactiveCollection<CardBaseClass> HandCardList => _handCardList;
    public IReactiveCollection<CardBaseClass> DeckCardList => _deckCardList;


    [SerializeField] private float _maxCost;
    [Tooltip("�J�[�h�Ɏg�p�̍ۂɕK�v�ȃR�X�g")]
    private ReactiveProperty<float> _cost = new();
    [Tooltip("��D�̃J�[�h���X�g")]
    private ReactiveCollection<CardBaseClass> _handCardList = new();
    [Tooltip("�R�D�̃J�[�h���X�g")]
    private ReactiveCollection<CardBaseClass> _deckCardList = new();
    /// <summary>�f�o�b�N�p�ϐ�</summary>
    [SerializeField] private List<CardBaseClass> _deckCopyList = new();
    [Tooltip("�̂ĎD�𒙂߂Ă���List")]
    private ReactiveCollection<CardBaseClass> _discardedCard = new();

    public override void Init()
    {
        base.Init();
        _deckCardList = new ReactiveCollection<CardBaseClass>(_deckCopyList);
        _cost.Value = _maxCost;
    }

    /// <summary>
    /// �R�X�g���g�p����
    /// </summary>
    /// <param name="useCost"></param>
    public bool UseCost(float useCost)
    {
        if (useCost <= _cost.Value)
        {
            _cost.Value -= useCost;
            return true;
        }
        return false;
    }

    public void ResetCost()
    {
        _cost.Value = _maxCost;
    }

    /// <summary>
    /// DataBase���烉���_���ɃJ�[�h��ǉ�����
    /// </summary>
    /// <param name="value"></param>
    public void TestAddCardList()
    {
        _deckCardList.Add(DataBaseScript.CardBaseClassList[0]);
    }

    /// <summary>
    /// �J�[�h���h���[����
    /// </summary>
    public void DrawCard()
    {
        if (_deckCardList.Count == 0 && _discardedCard.Count != 0)
        {
            //Draw����J�[�h���Ȃ��Ȃ������̂ĎD���R�D�ɖ߂�
            for (int i = 0; i < _discardedCard.Count; i++) 
            {
                _deckCardList.Add(_discardedCard[i]);
            }
            _discardedCard.Clear();
            _handCardList.Add(_deckCardList[0]);
            _deckCardList.RemoveAt(0);
            Debug.Log(_deckCardList.Count);
        }
        else if (_deckCardList.Count == 0 && _discardedCard.Count == 0)
        {
            Debug.LogWarning("�R�D�ɖ߂��J�[�h�͂���܂���");
        }
        else 
        {
            _handCardList.Add(_deckCardList[0]);
            _deckCardList.RemoveAt(0);
        }
    }

    /// <summary>
    /// �J�[�h���̂ĎD�ɉ����鏈��
    /// </summary>
    /// <param name="cardBase"></param>
    public void DiscardedCardAdd(CardBaseClass cardBase) 
    {
        _discardedCard.Add(cardBase);
    }
}
