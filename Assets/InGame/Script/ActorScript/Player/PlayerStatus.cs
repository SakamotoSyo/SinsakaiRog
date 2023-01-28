using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerStatus : StatusModelBase, IPlayerStatus
{
   [Tooltip("��D�̃J�[�h���X�g")]
   public IReactiveCollection<CardBaseClass> HandCardList => _handCardList;
   private ReactiveCollection<CardBaseClass> _handCardList = new ReactiveCollection<CardBaseClass>();

   [Tooltip("�R�D�̃J�[�h���X�g")]
   public List<CardBaseClass> DeckCardList => _deckCardList;
   [SerializeField]private List<CardBaseClass> _deckCardList = new List<CardBaseClass>();

    /// <summary>
    /// DataBase���烉���_���ɃJ�[�h��ǉ�����
    /// </summary>
    /// <param name="value"></param>
    public void TestAddCardList(int value) 
    {
        for (int i = 0; i < value; i++) 
        {
            //_deckCardList.Add();
        }
    }

    /// <summary>
    /// �J�[�h���h���[����
    /// </summary>
    public void DrowCard() 
    {
        _handCardList.Add(_deckCardList[0]);
        _deckCardList.RemoveAt(0);
    }


}
