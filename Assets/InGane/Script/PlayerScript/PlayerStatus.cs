using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlayerStatus : MonoBehaviour
{
    [Header("ç≈ëÂÇÃHP")]
    [SerializeField] private float _maxHp;

    public IObservable<float> CurrentHp => _currentHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    public IObservable<float> Attack => _attack;
    private ReactiveProperty<float> _attack = new ReactiveProperty<float>();
    private ReactiveProperty<float> _defense = new ReactiveProperty<float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
