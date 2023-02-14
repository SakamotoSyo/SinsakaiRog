using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButtonEffectScript : MonoBehaviour
{
    private IPlayerStatus _playerStaus;
    private float _gold;
    void Start()
    {
        _playerStaus = PlayerPresenter.PlayerStatus;
    }

    /// <summary>
    /// �S�[���h���O������󂯎��
    /// </summary>
    public void SetGold(float num) 
    {
        _gold = num;
    }

    /// <summary>
    /// �S�[���h���擾���鏈��
    /// </summary>
    public void PlayAddGold() 
    {
        _playerStaus.AddGold(_gold);
    }
}