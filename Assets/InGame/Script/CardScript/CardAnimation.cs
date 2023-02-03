using UnityEngine;
using System;
using DG.Tweening;

[Serializable]
public class CardAnimation 
{
    [SerializeField] private Animator _anim;
    [AnimationParameter]
    [SerializeField] private string _selectParm;
    [Tooltip("�h���[�����ۂ�Animation��Position��ς����")]
    [SerializeField] private Vector3 _drawAnimOffSet;
    [SerializeField] private CardEvent _cardEvent;
    private Transform _parentTransform;

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

    /// <summary>
    /// ���������ꏊ��Transform
    /// </summary>
    /// <param name="transform"></param>
    public void SetParentTransform(Transform transform) 
    {
        _parentTransform = transform;
    }

    public void DrawAnim(Transform transform) 
    {
        DOTween.To(() => transform.transform.localPosition - _parentTransform.localPosition - _drawAnimOffSet,
            x => transform.transform.localPosition = x,
            transform.transform.localPosition, 0.5f)
            .OnStart(() => _cardEvent.enabled = false)
            .OnComplete(() => _cardEvent.enabled = true);
    }
}
