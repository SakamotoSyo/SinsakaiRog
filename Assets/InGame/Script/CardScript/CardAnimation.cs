using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CardAnimation 
{
    [SerializeField] Animator _anim;
    [AnimationParameter]
    [SerializeField] string _selectParm;

    public void Init(Animator anim) 
    {
        _anim = anim;
    }

    /// <summary>
    /// selectAnimation‚ğÄ¶‚³‚¹‚é‚©‚Ç‚¤‚©
    /// </summary>
    /// <param name="play">Animation‚ğÄ¶‚³‚¹‚é‚©‚Ç‚¤‚©</param>
    public void SelectAnim(bool play) 
    {
        _anim.SetBool(_selectParm, play);
    }
}
