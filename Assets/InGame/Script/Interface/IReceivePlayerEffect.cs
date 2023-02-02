using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceivePlayerEffect
{
    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage);

    public void DrowCard();
}
