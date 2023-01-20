using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataBaseScript : MonoBehaviour
{
    [Header("�J�[�h�̃e�L�X�g�f�[�^")]
    [SerializeField] private TextAsset _cardData;

    public List<CardBaseClass> CardBaseClassList => _cardList;
    [Header("�J�[�h�̃f�[�^")]
    [SerializeField] private List<CardBaseClass> _cardList = new List<CardBaseClass>();

    readonly int _floatLoopNum = 4;

    private void Awake()
    {
        //DataInit();
    }

    private void Start()
    {
       
    }

    private void DataInit() 
    {
        StringReader sr = new StringReader(_cardData.text);
        //�ŏ��̍s�̓X�L�b�v
        sr.ReadLine();

        while (true) 
        {
            string line = sr.ReadLine();

            if (string.IsNullOrEmpty(line)) 
            {
                break;
            }

            string[] parts = line.Split(' ');

            int id = int.Parse(parts[0]);
            float[] floats = new float[_floatLoopNum];
            for (int i = 0; i < _floatLoopNum; i++) 
            {

                floats[i] = float.Parse(parts[i + 1]);
            }

            CardBaseClass cardBaseClass = new CardBaseClass(id, floats[0], floats[1], floats[2], floats[3], parts[5], parts[6]);
        }

    }

    public CardBaseClass GetRandomCard() 
    {
        return _cardList[Random.Range(0, _cardList.Count)];
    }
}
