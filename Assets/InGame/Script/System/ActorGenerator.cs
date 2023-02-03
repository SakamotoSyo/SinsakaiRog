using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _playerInsPos;
    [SerializeField] private Transform _enemyInsPos;

    public PlayerController PlayerController => _playerController;
    private PlayerController _playerController;
    public EnemyController EnemyController => _enemyController;
    private EnemyController _enemyController;

    private void Awake()
    {
        PlayerGeneration();
        EnemyGenaration();
    }

    /// <summary>
    /// PlayerÇÃê∂ê¨Ç∆èÓïÒÇÃï€ë∂
    /// </summary>
    public void PlayerGeneration()
    {
        _playerPrefab = Instantiate(_playerPrefab, _playerInsPos.transform.position, _playerInsPos.transform.rotation);
        _playerPrefab.transform.SetParent(_playerInsPos);
        _playerController = _playerPrefab.GetComponent<PlayerController>();
    }

    public void EnemyGenaration() 
    {
        _enemyPrefab = Instantiate(_enemyPrefab, _enemyInsPos.transform.position, _enemyInsPos.rotation);
        _enemyPrefab.transform.SetParent(_enemyInsPos);
        _enemyController = _enemyPrefab.GetComponent<EnemyController>();
    }
}
