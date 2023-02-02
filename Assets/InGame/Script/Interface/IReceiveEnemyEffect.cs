using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiveEnemyEffect
{
    public void AddDamage(float damage);
    /// <summary>
    /// このターンの行動を決定して攻撃する
    /// </summary>
    public void Attack();
}
