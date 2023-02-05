using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private ActorGenerator _actorGenerator;
    [SerializeField] private DataBaseScript _dataBaseScript;

    private void Awake()
    {
        _dataBaseScript.Init();
        _actorGenerator.Init();
    }
}
