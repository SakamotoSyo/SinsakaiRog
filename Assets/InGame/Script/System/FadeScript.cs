using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class FadeScript : SingletonBehaviour<FadeScript>
{
    [SerializeField] private Animator _anim;
    [AnimationParameter]
    [SerializeField] private string _fadeOut;
    [AnimationParameter]
    [SerializeField] private string _fadeIn;
    [SerializeField] private Image _fadeImage;

    protected override void OnAwake()
    {
        
    }

    public async UniTask FadeOut() 
    {
        _fadeImage.enabled = true;
        _anim.SetTrigger(_fadeOut);
        await UniTask.WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
    }

    public async UniTask FadeIn() 
    {
        _anim.SetTrigger(_fadeIn);
        await UniTask.WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);
        _fadeImage.enabled = false;
    }

}
