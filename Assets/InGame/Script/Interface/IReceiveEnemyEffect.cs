using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiveEnemyEffect
{
    public void AddDamage(float damage);
    /// <summary>
    /// ���̃^�[���̍s�������肵�čU������
    /// </summary>
    public void Attack();
}
