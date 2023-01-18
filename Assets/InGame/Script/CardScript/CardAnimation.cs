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
    /// selectAnimation���Đ������邩�ǂ���
    /// </summary>
    /// <param name="play">Animation���Đ������邩�ǂ���</param>
    public void SelectAnim(bool play) 
    {
        _anim.SetBool(_selectParm, play);
    }
}
