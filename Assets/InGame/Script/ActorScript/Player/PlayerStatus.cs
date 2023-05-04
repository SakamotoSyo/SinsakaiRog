using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


[Serializable]
public class PlayerStatus : StatusModelBase, IPlayerStatus
{
    [SerializeField] private float _maxCost;
    [Tooltip("�J�[�h�Ɏg�p�̍ۂɕK�v�ȃR�X�g")]
    private ReactiveProperty<float> _cost = new();
    private ReactiveProperty<float> _gold = new();
    [Tooltip("��D�̃J�[�h���X�g")]
    private ReactiveCollection<CardBaseClass> _handCardList = new();
    [Tooltip("�R�D�̃J�[�h���X�g")]
    private ReactiveCollection<CardBaseClass> _deckCardList = new();
    [Tooltip("�̂ĎD�𒙂߂Ă���List")]
    private ReactiveCollection<CardBaseClass> _graveyardCards = new();

    public PlayerStatus() 
    {
        Init();
    }

    public override void Init()
    {
        SetPlayerSaveData(GameManager.SaveData);
    }

    public void DeckInit() 
    {

    }

    /// <summary>
    /// �J�[�h���h���[����
    /// </summary>
    public void DrawCard(float num = 1)
    {
        for (int i = 0; i < num; i++)
        {
            if (_deckCardList.Count == 0 && _graveyardCards.Count != 0)
            {
                //Draw����J�[�h���Ȃ��Ȃ������̂ĎD���R�D�ɖ߂�
                for (int j = 0; j < _graveyardCards.Count; j++)
                {
                    _deckCardList.Add(_graveyardCards[j]);
                }
                _graveyardCards.Clear();
                _handCardList.Add(_deckCardList[0]);
            }
            else if (_deckCardList.Count == 0 && _graveyardCards.Count == 0)
            {
                Debug.LogWarning("�R�D�ɖ߂��J�[�h�͂���܂���");
            }
            else
            {
                _handCardList.Add(_deckCardList[0]);
                _deckCardList.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// ��D�����ׂĎ̂Ă�
    /// </summary>
    public void DiscardAllHandCards() 
    {
        for (int i = 0; i < _handCardList.Count; i++) 
        {
            _graveyardCards.Add(_handCardList[i]);
        }
        _handCardList.Clear();
    } 

    /// <summary>
    /// �J�[�h���̂ĎD�ɉ����鏈��
    /// </summary>
    /// <param name="cardBase"></param>
    public void GraveyardCardsAdd(CardBaseClass cardBase)
    {
        _graveyardCards.Add(cardBase);
        _handCardList.Remove(cardBase);
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
    /// �f�b�L�ɃJ�[�h��ǉ�����
    /// </summary>
    /// <param name="cardBase">�ǉ�����J�[�h�̏��</param>
    public void AddDeckCard(CardBaseClass cardBase) 
    {
        _deckCardList.Add(cardBase);
    }

    public void AddGold(float gold)
    {
        _gold.Value += gold;
    }

    public bool UseGold(float useGold)
    {
        if (useGold <= _gold.Value) 
        {
            _gold.Value -= useGold;
            return true;
        }

        return false;
    }

    #region�@Get�֐�
    public IObservable<float> GetCostOb()
    {
        return _cost;
    }

    public IReactiveCollection<CardBaseClass> GetHandCardListOb()
    {
        return _handCardList;
    }

    public IReactiveCollection<CardBaseClass> GetGraveyardCardsCountOb()
    {
        return _graveyardCards;
    }

    public IReactiveCollection<CardBaseClass> GetDeckCardListOb()
    {
        return _deckCardList;
    }
    public IStatusBase GetStatusBase()
    {
        return this;
    }

    public IObservable<float> GetGoldOb()
    {
        return _gold;
    }

    public ReactiveCollection<CardBaseClass> GetGraveyardCardList()
    {
        return _graveyardCards;
    }

    public ReactiveCollection<CardBaseClass> GetDeckCardList()
    {
        return _deckCardList;
    }
    #endregion

    public PlayerStatusSaveData GetPlayerSaveData() 
    {
        PlayerStatusSaveData saveData = new();
        saveData.MaxHp = _maxHp.Value;
        saveData.Currenthp = _currentHp.Value;
        saveData.Nowcost = _cost.Value;
        saveData.MaxCost = _maxCost;
        saveData.Gold = _gold.Value;
        saveData.DeckCardList = new List<CardBaseClass>(_deckCardList);
        for (int i = 0; i < _handCardList.Count; i++) 
        {
            saveData.DeckCardList.Add(_handCardList[i]);
        }

        for (int i = 0; i < _graveyardCards.Count; i++) 
        {
            saveData.DeckCardList.Add(_graveyardCards[i]);
        }
        return saveData;
    }

    /// <summary>
    /// �Z�[�u�f�[�^���X�e�[�^�X�ɃZ�b�g����
    /// </summary>
    /// <param name="playerData"></param>
    public void SetPlayerSaveData(PlayerStatusSaveData playerData) 
    {
        _maxHp.Value = playerData.MaxHp;
        _currentHp.Value = playerData.Currenthp;
        _cost.Value = playerData.Nowcost;
        _maxCost = playerData.MaxCost;
        _gold.Value = playerData.Gold;
        _deckCardList = new ReactiveCollection<CardBaseClass>(playerData.DeckCardList);
    }
}
