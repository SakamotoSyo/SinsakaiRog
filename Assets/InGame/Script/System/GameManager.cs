using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>���݂̊K�w</summary>
    public static int CurremtLevel => currentLevel;
    public static PlayerStatusSaveData SaveData => saveData;

    [Tooltip("���݂̊K�w")]
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
    /// �K�w��オ��
    /// </summary>
    public static void NextCurrentLevel() 
    {
        //���̊K�w�����[�h���鏈��
        currentLevel++;
    }

    public static void GameOver() 
    {
        //���U���g�̃V�[�����Ăяo��
    }

    /// <summary>
    /// Player�Ɋւ���f�[�^��ۑ�����
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