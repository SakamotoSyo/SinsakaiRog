using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusView : MonoBehaviour
{

    [Header("Hp��RectTransform")]
    [SerializeField] private RectTransform _rectCurrent;
    [SerializeField] private Text _maxHpText;
    [SerializeField] private Text _currentHpText;
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

    public void SetHpCurrent(float currentHp)
    {
        //�o�[�̒������X�V
        _rectCurrent.SetWidth(GetWidth(currentHp));
        _currentHpText.text = currentHp.ToString("0");
        if (currentHp < 0)
        {
            
        }
    }

    private float GetWidth(float value)
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