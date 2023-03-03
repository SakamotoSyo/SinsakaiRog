using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>Œ»İ‚ÌŠK‘w</summary>
    public static int CurremtLevel => currentLevel;
    public static PlayerStatusSaveData SaveData => saveData;

    [Tooltip("Œ»İ‚ÌŠK‘w")]
    private static int currentLevel = 1;
    private static float score;
    private static PlayerStatusSaveData saveData = new();

    void Start()
    {

    }

    void Update()
    {
        
    }

    /// <summary>
    /// ŠK‘wˆê‚Âã‚ª‚é
    /// </summary>
    public static void NextCurrentLevel() 
    {
        //Ÿ‚ÌŠK‘w‚ğƒ[ƒh‚·‚éˆ—
        currentLevel++;
    }

    public static void GameOver() 
    {
        //ƒŠƒUƒ‹ƒg‚ÌƒV[ƒ“‚ğŒÄ‚Ño‚·
    }

    /// <summary>
    /// Player‚ÉŠÖ‚·‚éƒf[ƒ^‚ğ•Û‘¶‚·‚é
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