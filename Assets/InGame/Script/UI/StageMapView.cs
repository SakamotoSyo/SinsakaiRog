using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�e�[�W��I������ۂɃJ�[�h��Image�Ȃǂ𓖂Ă͂߂�N���X
/// </summary>
public class StageMapView : MonoBehaviour
{
    [Tooltip("�}�b�v��List�̉��Ԗڂ�K�������邩")]
    [SerializeField] private int _mapListSelectNum;
    [SerializeField] private Sprite[] _spriteArray;
    [SerializeField] private string[] _stageInfo;
    [SerializeField] private Image[] _stageImage;
    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    private void SetUp() 
    {
        var cardArray = GetComponentsInChildren<StageSelectCard>();
        var cardInfoText = GetComponentsInChildren<Text>();
        if (GenerationDungeonMap.DungeonMapData[1] == null) 
        {
            Debug.Log("Null");
        }

        for (int i = 0; i < GenerationDungeonMap.DungeonMapData[_mapListSelectNum].Length; i++) 
        {
            //�J�[�h�Ɏ������ǂ̃X�e�[�W�̃J�[�h�Ȃ̂��̏���n��
            cardArray[i].SetStageType((StageType)GenerationDungeonMap.DungeonMapData[_mapListSelectNum][i]);
            _stageImage[i].sprite = _spriteArray[GenerationDungeonMap.DungeonMapData[_mapListSelectNum][i]];
            cardInfoText[i].text = _stageInfo[GenerationDungeonMap.DungeonMapData[_mapListSelectNum][i]];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
