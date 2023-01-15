using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseScript : MonoBehaviour
{
    [Header("カードのテキストデータ")]
    [SerializeField] private TextAsset _cardData;
    [Header("カードのデータ")]
    [SerializeField] private List<CardBaseClass> _cardList = new List<CardBaseClass>();

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
