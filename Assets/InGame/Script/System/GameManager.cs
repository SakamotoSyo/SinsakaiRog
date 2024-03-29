using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>現在の階層</summary>
    public static int CurremtLevel => currentLevel;
    public static PlayerStatusSaveData SaveData => saveData;
    public static (int X, int Y) PlayerMapPosition { set; get;}

    [Tooltip("現在の階層")]
    private static int currentLevel = 1;
    private static float score;
    private static PlayerStatusSaveData saveData = new();

    /// <summary>
    /// 階層一つ上がる
    /// </summary>
    public static void NextCurrentLevel() 
    {
        //次の階層をロードする処理
        currentLevel++;
    }

    public static void ResetCurrentLevel() 
    {
        currentLevel = 0;
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
}