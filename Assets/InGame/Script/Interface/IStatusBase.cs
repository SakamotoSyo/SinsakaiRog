using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
public interface IStatusBase
{
    public IObservable<float> GetDefeceOb();
    public IObservable<float> GetMaxHpOb();
    public IObservable<float> GetCurrentHpOb();
    public float GetDefenceNum();
    public float GetMaxHpNum();
    public float GetCurrentHpNum();
    public void ChangeValueDefense(float value);
    public void AddDamage(float damage);
    public void Healing(float damage);
    public void DefenseIncrease(float num);
    public bool DownJudge(float damage);

}
