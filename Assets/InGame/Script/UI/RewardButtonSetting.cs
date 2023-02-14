using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardButtonSetting : MonoBehaviour
{
    [SerializeField] private Image _reWardImege;
    [SerializeField] private Text _reWardText;
    [SerializeField] private Button _reWardButton;
    [SerializeField] private List<ReWardSetting> _reWardSetting = new();

    /// <summary>
    /// É{É^ÉìÇÃImageÇ∆TextÇÃê›íËÇ∑ÇÈ
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Button ButtonSetting(ReWardType type) 
    {
        var rewardType = _reWardSetting.Find(x => x.Type == type);
        _reWardText.text = rewardType.Name;
        _reWardImege.sprite = rewardType.Sp;
        return _reWardButton;
    }
}


[System.Serializable]
public class ReWardSetting
{
    public Sprite Sp;
    public string Name;
    public ReWardType Type;
}
