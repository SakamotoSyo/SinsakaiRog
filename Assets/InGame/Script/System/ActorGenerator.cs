using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class ActorGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private List<EnemyInstanceData> _enemyPrefabList = new List<EnemyInstanceData>();
    [SerializeField] private Transform _playerInsPos;
    [SerializeField] private Transform _enemyInsPos;

    public PlayerController PlayerController => _playerController;
    private PlayerController _playerController;
    public EnemyController EnemyController => _enemyController;
    private EnemyController _enemyController;

    public void Init()
    {
        PlayerGeneration();
        EnemyGenaration();
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// Playerの生成と情報の保存
    /// </summary>
    public void PlayerGeneration()
    {
        _playerPrefab = Instantiate(_playerPrefab, _playerInsPos.transform.position, _playerInsPos.transform.rotation);
        _playerPrefab.transform.SetParent(_playerInsPos);
        _playerController = _playerPrefab.GetComponent<PlayerController>();
    }

    public void EnemyGenaration() 
    {
        EnemyInstanceData enemyData = EnemySelect();
        GameObject enemy = Instantiate(enemyData.EnemyObj, _enemyInsPos.transform.position, _enemyInsPos.rotation);
        enemy.transform.SetParent(_enemyInsPos);
        _enemyController = enemy.GetComponent<EnemyController>();
        //TODO:ここで今は試験的に実施後々にはEnemyの情報をどこかからもらいたい
        _enemyController.EnemyStatus.StatusSet(enemyData.EnemyStatus);
    }

    public EnemyInstanceData EnemySelect() 
    {
        EnemyStatusData[] enemyDataArray = DataBaseScript.EnemyData.Where(x => x.BaseCurrentLevel <= GameManager.CurremtLevel).ToArray();
        EnemyStatusData enemyStatusData = enemyDataArray[UnityEngine.Random.Range(0, enemyDataArray.Length)];
        EnemyInstanceData enemyData = _enemyPrefabList.Find(x => x.InstanceName == enemyStatusData.Name);
        enemyData.EnemyStatus = enemyStatusData;
        return enemyData;

    }
}

[System.Serializable]
public class EnemyInstanceData 
{
    public string InstanceName;
    public GameObject EnemyObj;
    public EnemyStatusData EnemyStatus;
}