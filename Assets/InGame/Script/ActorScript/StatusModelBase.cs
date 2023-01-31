using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public abstract class StatusModelBase :MonoBehaviour, IDamageble
{
    public float MaxHpNum => _maxHp.Value;
    public IObservable<float> MaxHp => _maxHp;
    protected ReactiveProperty<float> _maxHp = new ReactiveProperty<float>();
    public float CurrentHpNum => _currentHp.Value; 
    public IObservable<float> CurrentHp => _currentHp;
    protected ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    public IObservable<float> Attack => _attack;
    protected ReactiveProperty<float> _attack = new ReactiveProperty<float>();
    public IObservable<float> Defense => _defense;
    protected ReactiveProperty<float> _defense = new ReactiveProperty<float>();

    void Awake() 
    {
        _maxHp.Value = 100;
        _currentHp.Value = 100; 
    }

    /// <summary>
    /// 攻撃の数値を変化される
    /// </summary>
    /// <param name="value">変化させる値</param>
    public void ChangeValueAttack(float value)
    {
        _attack.Value = value;
    }

    /// <summary>
    /// 防御の数値を変化される
    /// </summary>
    /// <param name="value">変化させる値</param>
    public void ChangeValueDefense(float value)
    {
        _defense.Value = value;
    }

    /// <summary>
    /// Hpの数値を変化される
    /// </summary>
    /// <param name="value">変化させる値</param>
    public void ChangeValueHealth(float value)
    {
        _currentHp.Value = value;
    }
}
