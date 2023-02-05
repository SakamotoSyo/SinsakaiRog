using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorViewBase : MonoBehaviour
{
    [SerializeField] private GameObject _defenceObj;
    [Header("Hp��RectTransform")]
    [SerializeField] protected RectTransform _rectCurrent;
    [SerializeField] protected Image _hpImage;
    [SerializeField] protected Image _hpOutLineImage;
    #region Color
    [SerializeField] protected Color _hpDefaultColor;
    [SerializeField] protected Color _hpOutLineDefaultColor;
    [SerializeField] protected Color _hpDefenceColor;
    [SerializeField] protected Color _hpOutLineDefenceColor;
    #endregion
    [SerializeField] private Text _maxHpText;
    [SerializeField] private Text _currentHpText;
    [SerializeField] private Text _defenceText;
    [Tooltip("Hp�o�[�Œ��̒���")]
    private float _maxHpWidth;
    [Tooltip("Hp�o�[�̍ő�l")]
    private float _maxTime;

    private void Awake()
    {
        _maxHpWidth = _rectCurrent.sizeDelta.x;
    }

    public void MaxHpSet(float MaxHp)
    {
        _maxTime = MaxHp;
        _maxHpText.text = MaxHp.ToString("0");
    }

    public virtual void SetHpCurrent(float currentHp)
    {
        //�o�[�̒������X�V
        _rectCurrent.SetWidth(GetWidth(currentHp));
        _currentHpText.text = currentHp.ToString("0");
        if (currentHp < 0)
        {

        }
    }

    /// <summary>
    /// �h�䂪�ς�������̏���
    /// </summary>
    /// <param name="num"></param>
    public virtual void SetDefense(float num) 
    {
        if (0 < num)
        {
            _defenceObj.SetActive(true);
            _defenceText.text = num.ToString("0");
            _hpImage.color = _hpDefenceColor;
            _hpOutLineImage.color = _hpOutLineDefenceColor;
        }
        else 
        {
            _defenceObj.SetActive(false);
            _hpImage.color = _hpDefaultColor;
            _hpOutLineImage.color = _hpOutLineDefaultColor;
        }
        
    }

    protected float GetWidth(float value)
    {
        float width = Mathf.InverseLerp(0, _maxTime, value);
        return Mathf.Lerp(0, _maxHpWidth, width);
    }
}

public static class UIExtensions
{
    /// <summary>
    /// ���݂̒l��Rect�ɃZ�b�g����
    /// </summary>
    /// <param name="width"></param>
    public static void SetWidth(this RectTransform rect, float width)
    {
        Vector2 s = rect.sizeDelta;
        s.x = width;
        rect.sizeDelta = s;
    }
}