using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBaseClass
{
    private float _cardPower;
    private float _cardDefence;
    private float _cardCost;

    [SerializeReference, SubclassSelector]
    ICardEffect _effect;


    public void Init(float power,float defence, float cost) 
    {
        _cardPower = power;
        _cardDefence = defence;
        _cardCost = cost;
    }

    public void UseEffect() 
    {
        _effect.UseEffect();
    }
}
