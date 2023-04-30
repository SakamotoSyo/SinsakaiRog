using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPresenterName;

public class ItemButtonEffectScript : MonoBehaviour
{
    private IPlayerStatus _playerStaus;
    private float _gold;
    void Start()
    {
        Debug.Log("うわあああああああああああああ");
    }

    /// <summary>
    /// ゴールドを外部から受け取る
    /// </summary>
    public void SetGold(float num) 
    {
        _gold = num;
    }

    /// <summary>
    /// ゴールドを取得する処理
    /// </summary>
    public void PlayAddGold() 
    {
        _playerStaus.AddGold(_gold);
    }
}
