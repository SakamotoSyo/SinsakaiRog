using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashCard : CardBaseClass
{
    private string _testString = "Slash";
    public override void UseEffect()
    {
        Debug.Log(_testString);
    }

}
