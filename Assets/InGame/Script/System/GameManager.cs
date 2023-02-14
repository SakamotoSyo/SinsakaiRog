using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>現在の階層</summary>
    public static int CurremtLevel => currentLevel;
    public static PlayerStatusSaveData SaveData => saveData;

    [Tooltip("現在の階層")]
    private static int currentLevel = 1;
    private static float score;
    private static PlayerStatusSaveData saveData;

    void Start()
    {

    }

    void Update()
    {
        
    }

    /// <summary>
    /// 階層一つ上がる
    /// </summary>
    public static void NextCurrentLevel() 
    {
        //次の階層をロードする処理
        currentLevel++;
    }

    public static void GameOver() 
    {
        //リザルトのシーンを呼び出す
    }

    /// <summary>
    /// Playerに関するデータを保存する
    /// </summary>
    /// <param name="save"></param>
    public static void SavePlayerData(PlayerStatusSaveData save) 
    {
        saveData = save;
    }

    public static void SavePlayerData<T>(T saveData, PlayerStatusSaveType saveType) 
    {
        switch (saveType)
        {
            case PlayerStatusSaveType.MaxHp:
                break;
            case PlayerStatusSaveType.CurrentHp:
                break;
            case PlayerStatusSaveType.Defence:
                break;
            case PlayerStatusSaveType.MaxCost:
                break;
            case PlayerStatusSaveType.Nowcost:
                break;
            case PlayerStatusSaveType.Gold:
                break;
            case PlayerStatusSaveType.HandCardList:
                break;
            case PlayerStatusSaveType.DeckCardList:
                break;
            case PlayerStatusSaveType.GraveyardCards:
                break;
            default:
                break;
        }
    }
}