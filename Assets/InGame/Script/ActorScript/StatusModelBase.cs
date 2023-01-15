using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public abstract class StatusModelBase : MonoBehaviour, IDamageble
{
    [Header("ç≈ëÂÇÃHP")]
    [SerializeField] private float _maxHp;

    public IObservable<float> CurrentHp => _currentHp;
    protected ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    public IObservable<float> Attack => _attack;
    protected ReactiveProperty<float> _attack = new ReactiveProperty<float>();
    public IObservable<float> Defense => _defense;
    protected ReactiveProperty<float> _defense = new ReactiveProperty<float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeValueAttack(float value)
    {
        _attack.Value = value;
    }

    public void ChangeValueDefense(float value)
    {
        _defense.Value = value;
    }

    public void ChangeValueHealth(float value)
    {
        _currentHp.Value = value;
    }
}
