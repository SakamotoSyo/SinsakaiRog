using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyStatusPresenter : MonoBehaviour
{
    [SerializeField] EnemyStatus _enemyStatusModel;
    [SerializeField] EnemyStatusView _enemyView;

    // Start is called before the first frame update
    void Start()
    {
        _enemyStatusModel.CurrentHp.Subscribe(value => _enemyView.SetHpCurrent(value)).AddTo(this);
        _enemyStatusModel.MaxHp.Subscribe(value => _enemyView.MaxHpSet(value)).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
 
    }


}
