using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEventView : MonoBehaviour
{
    [SerializeField] private Text _maxHp;
    [SerializeField] private Text _currentHp;
    [SerializeField] private Text _currentLevelText;

    private void Start()
    {
        _currentLevelText.text = GameManager.CurremtLevel.ToString();
    }

    public void SetMaxHpView(float MaxHp) 
    {
        _maxHp.text = MaxHp.ToString();
    }

    public void SetCurrentHp(float currentHp) 
    {
        _currentHp.text = currentHp.ToString();
    }
}
