using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージを選択する際にカードにImageなどを当てはめるクラス
/// </summary>
public class StageMapView : MonoBehaviour
{
    [Tooltip("マップのListの何番目を適応させるか")]
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
            //カードに自分がどのステージのカードなのかの情報を渡す
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
