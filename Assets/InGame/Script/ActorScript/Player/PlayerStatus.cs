using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


[Serializable]
public class PlayerStatus : StatusModelBase, IPlayerStatus
{
    public float Cost => _myCost.Value;
    public IObservable<float> CostOb => _myCost; 
    public IReactiveCollection<CardBaseClass> HandCardList => _handCardList;
    public List<CardBaseClass> DeckCardList => _deckCardList;

    [Header("�J�[�h�Ɏg�p�̍ۂɕK�v�ȃR�X�g")]
    [SerializeField] private ReactiveProperty<float> _myCost;
    [Tooltip("��D�̃J�[�h���X�g")]
    private ReactiveCollection<CardBaseClass> _handCardList = new ReactiveCollection<CardBaseClass>();
    [Tooltip("�R�D�̃J�[�h���X�g")]
    [SerializeField] private List<CardBaseClass> _deckCardList = new List<CardBaseClass>();

    /// <summary>
    /// �R�X�g���g�p����
    /// </summary>
    /// <param name="useCost"></param>
    public void UseCost(float useCost) 
    {
        _myCost.Value -= useCost;
        Debug.Log(_myCost);
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
    public void DrowCard()
    {
        _handCardList.Add(_deckCardList[0]);
        //_deckCardList.RemoveAt(0);
    }
}
