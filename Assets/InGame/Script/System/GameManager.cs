using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>���݂̊K�w</summary>
    public static int CurremtLevel => currentLevel;
    public static PlayerStatusSaveData SaveData => saveData;

    [Tooltip("���݂̊K�w")]
    private static int currentLevel = 0;
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

    public static void ResetCurrentLevel() 
    {
        currentLevel = 0;
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
}