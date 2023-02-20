using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SystemUI : MonoBehaviour
{
    [SerializeField] private Text _currentLevelText;
    [SerializeField] private Text _maxHpText;
    [SerializeField] private Text _currentHpText;
    [SerializeField] private Text _goldText;
    [SerializeField] private ActorGenerator _generator;
    private IPlayerStatus _playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        _playerStatus = _generator.PlayerController.PlayerStatus;
        _currentLevelText.text = GameManager.CurremtLevel.ToString();
        _playerStatus.GetStatusBase().GetMaxHpOb().Subscribe(MaxHpSet).AddTo(this);
        _playerStatus.GetStatusBase().GetCurrentHpOb().Subscribe(SetHpCurrent).AddTo(this);
        _playerStatus.GetGoldOb().Subscribe(SetGoldView).AddTo(this);
    }

    public void MaxHpSet(float MaxHp)
    {
        _maxHpText.text = MaxHp.ToString("0");
    }

    private void SetHpCurrent(float currentHp)
    {
        _currentHpText.text = currentHp.ToString("0");
    }

    private void SetGoldView(float gold) 
    {
        _goldText.text = gold.ToString("0");
    }
}
