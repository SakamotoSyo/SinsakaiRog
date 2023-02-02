using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public abstract class StatusModelBase
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

    public virtual void Init() 
    {
        //ToDo:�����̓f�o�b�N�p�ɐ��l��^���Ă���
        _maxHp.Value = 100;
        _currentHp.Value = 100; 
    }

    /// <summary>
    /// �U���̐��l��ω�������
    /// </summary>
    /// <param name="value">�ω�������l</param>
    public virtual void ChangeValueAttack(float value)
    {
        _attack.Value = value;
    }

    /// <summary>
    /// �h��̐��l��ω�������
    /// </summary>
    /// <param name="value">�ω�������l</param>
    public virtual void ChangeValueDefense(float value)
    {
        _defense.Value = value;
    }

    /// <summary>
    /// Hp�̐��l��ω�������
    /// </summary>
    /// <param name="value">�ω�������l</param>
    public virtual void ChangeValueHealth(float value)
    {
        _currentHp.Value = value;
    }
}
