using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyStatusView : ActorViewBase
{
    [SerializeField] private List<EffectTypeData> _effectTypeData = new();
    [Tooltip("�U���̃A�C�R���𐶐�����ꏊ")]
    [SerializeField] private GameObject _attackIconInsPos;
    [Tooltip("�U���̃A�C�R���̌��ƂȂ�Prefab")]
    [SerializeField] private GameObject _attackIconPrefab;
    private List<IconScript> _iconScriptList = new(); 
    public override void SetHpCurrent(float currentHp)
    {
        base.SetHpCurrent(currentHp);
    }

    /// <summary>
    /// Enemy�̍s����Icon�Ƃ��Đ�������
    /// </summary>
    /// <param name="effectData"></param>
    public void SetAttackIcon(EnemyEffectData effectData) 
    {
        var icon = Instantiate(_attackIconPrefab).GetComponent<IconScript>();
        _iconScriptList.Add(icon);
        icon.SetEffectPower(effectData.EffectPower);
        icon.transform.SetParent(_attackIconInsPos.transform);
        icon.SetImage(GetIconSprite(effectData.ImageType));
    }

    /// <summary>
    /// �s�����I�������Animation��ǂ�ŊY���̃A�C�R�����폜����
    /// </summary>
    /// <param name="effectData"></param>
    public void DeleteIcon(EnemyEffectData effectData) 
    {
        _iconScriptList[0].SelectIcon();
        _iconScriptList.RemoveAt(0);
    }

    public Sprite GetIconSprite(EffectTypeImage typeImage) 
    {
        return _effectTypeData.Find(x => x.ImageType == typeImage).AttackIcon;
    }
}

[Serializable]
public class EffectTypeData 
{
    public EffectTypeImage ImageType => _imegeType;
   [SerializeField] private EffectTypeImage _imegeType;
   public Sprite AttackIcon => _attackIcon;
   [SerializeField] private Sprite _attackIcon;
}

public enum EffectTypeImage
{
    Attack,
    Defence,
    Special,
}