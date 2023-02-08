using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public abstract class StatusModelBase : IStatusBase
{
    public float MaxHpNum => _maxHp.Value;
    protected ReactiveProperty<float> _maxHp = new ReactiveProperty<float>();
    public float CurrentHpNum => _currentHp.Value; 
    protected ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    protected ReactiveProperty<float> _attack = new ReactiveProperty<float>();
    public float DefenceNum => _defence.Value;
    protected ReactiveProperty<float> _defence = new ReactiveProperty<float>();

    public virtual void Init() 
    {
        //ToDo:ここはデバック用に数値を与えている
        _maxHp.Value = 100;
        _currentHp.Value = 100; 
    }

    /// <summary>
    /// 攻撃の数値を変化させる
    /// </summary>
    /// <param name="value">変化させる値</param>
    public virtual void ChangeValueAttack(float value)
    {
        _attack.Value = value;
    }

    /// <summary>
    /// 防御の数値を変化させる
    /// </summary>
    /// <param name="value">変化させる値</param>
    public virtual void ChangeValueDefense(float value)
    {
        _defence.Value = value;
    }

    /// <summary>
    /// Hpの数値を変化させる
    /// </summary>
    /// <param name="value">変化させる値</param>
    public virtual void ChangeValueHealth(float value)
    {
        _currentHp.Value = value;
    }


    //TODO;overrideして正常に動くかテストする
    public virtual void AddDamage(float value) 
    {
        var num = value - _defence.Value;

        if (0 < num)
        {
            _currentHp.Value -= num;
            _defence.Value = 0;
        }
        else 
        {
           _defence.Value = num * -1;
        }
    }

    public void DefenseIncrease(float num)
    {
        _defence.Value += num;
    }

    public IObservable<float> GetDefeceOb()
    {
        return _defence;
    }

    public IObservable<float> GetMaxHpOb()
    {
        return _maxHp;
    }

    public IObservable<float> GetCurrentHpOb()
    {
        return _currentHp;
    }

    /// <summary>
    /// 攻撃でダウンするか判定する
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool DownJudge(float damage)
    {
        return 0 <= _currentHp.Value + _defence.Value - damage;
    }

    public float GetDefenceNum()
    {
        return _defence.Value;
    }

    public float GetMaxHpNum()
    {
        return _maxHp.Value;
    }

    public float GetCurrentHpNum()
    {
        return _currentHp.Value;
    }
}
