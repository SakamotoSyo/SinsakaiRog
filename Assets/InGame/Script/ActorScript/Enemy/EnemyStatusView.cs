using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusView : MonoBehaviour
{

    [Header("HpのRectTransform")]
    [SerializeField] private RectTransform _rectCurrent;
    [SerializeField] private Text _maxHpText;
    [SerializeField] private Text _currentHpText;
    [Tooltip("Hpバー最長の長さ")]
    private float _maxHpWidth;
    [Tooltip("Hpバーの最大値")]
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
        //バーの長さを更新
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
    /// 現在の値をRectにセットする
    /// </summary>
    /// <param name="width"></param>
    public static void SetWidth(this RectTransform rect, float width)
    {
        Vector2 s = rect.sizeDelta;
        s.x = width;
        rect.sizeDelta = s;
    }
}