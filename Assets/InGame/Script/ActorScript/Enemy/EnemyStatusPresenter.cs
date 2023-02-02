using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyStatusPresenter : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private EnemyStatusView _enemyView;
    private EnemyStatus _enemyStatusModel;

    void Start()
    {
        _enemyStatusModel = _enemyController.EnemyStatus;
        _enemyStatusModel.MaxHp.Subscribe(_enemyView.MaxHpSet).AddTo(this);
        _enemyStatusModel.CurrentHp.Subscribe(_enemyView.SetHpCurrent).AddTo(this);
    }

    void Update()
    {
        
 
    }


}
