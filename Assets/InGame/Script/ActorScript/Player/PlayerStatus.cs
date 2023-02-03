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
    public IReactiveCollection<CardBaseClass> HandCardList => _handCardList;
    public List<CardBaseClass> DeckCardList => _deckCardList;


    [SerializeField] private float _maxCost;
    [Tooltip("�J�[�h�Ɏg�p�̍ۂɕK�v�ȃR�X�g")]
    private ReactiveProperty<float> _cost = new();
    [Tooltip("��D�̃J�[�h���X�g")]
    private ReactiveCollection<CardBaseClass> _handCardList = new();
    [Tooltip("�R�D�̃J�[�h���X�g")]
    [SerializeField] private List<CardBaseClass> _deckCardList = new();
    [Tooltip("�̂ĎD�𒙂߂Ă���List")]
    private List<CardBaseClass> _discardedCard = new();

    public override void Init()
    {
        base.Init();
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

    //TODO:������Remove�R�����g���O��
    /// <summary>
    /// �J�[�h���h���[����
    /// </summary>
    public void DrawCard()
    {
        if (_deckCardList.Count != 0)
        {
            _handCardList.Add(_deckCardList[0]);
            _discardedCard.Add(_deckCardList[0]);
            _deckCardList.RemoveAt(0);
        }
        else 
        {
            //Draw����J�[�h���Ȃ��Ȃ������̂ĎD���R�D�ɖ߂�
            _deckCardList = new List<CardBaseClass>(_discardedCard);
           _discardedCard.Clear();

           _handCardList.Add(_deckCardList[0]);
            _discardedCard.Add(_deckCardList[0]);
           _deckCardList.RemoveAt(0);
            Debug.Log("�J�[�h��߂���");
        }
    }
}
