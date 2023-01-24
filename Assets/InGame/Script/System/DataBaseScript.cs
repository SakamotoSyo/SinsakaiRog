using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataBaseScript : MonoBehaviour
{
    [Header("�J�[�h�̃e�L�X�g�f�[�^")]
    [SerializeField] private TextAsset _cardData;

    public List<CardBaseClass> CardBaseClassList => _cardList;
    [Header("�J�[�h�̃f�[�^")]
    [SerializeField] private List<CardBaseClass> _cardList = new List<CardBaseClass>();

    PlayerStatus _status;
    EnemyStatus _enemyStaus;

    readonly int _floatLoopNum = 2;

    private void Awake()
    {
        CardDataInit();
    }

    private void Start()
    {
        _cardList[0].UseEffect(_status, _enemyStaus);
    }

    private void CardDataInit() 
    {
        StringReader sr = new StringReader(_cardData.text);
        //�ŏ��̍s�̓X�L�b�v
        sr.ReadLine();

        //while (true) 
        //{
        //    string line = sr.ReadLine();

        //    if (string.IsNullOrEmpty(line)) 
        //    {
        //        break;
        //    }

        //    string[] parts = line.Split(' ');

        //    int id = int.Parse(parts[0]);
        //    float[] floats = new float[_floatLoopNum];
        //    for (int i = 0; i < _floatLoopNum; i++) 
        //    {

        //        floats[i] = float.Parse(parts[i + 2]);
        //    }

        //    List<EffectData> effectList = new List<EffectData>();
        //    �����̃}�W�b�N�i���o�[��ŏC��
        //    for (int i = 7; i < parts.Length; i++) 
        //    {
        //        var data = parts[i].Split("_");
        //        effectList.Add( new EffectData(MakeClass<ICardEffect>(data[0]), float.Parse(data[1])));
        //    }
        //    CardBaseClass cardBaseClass = new CardBaseClass(id, parts[1], floats[0], floats[1], parts[6], parts[7], effectList);
        //}

        for (int i = 0; i < 1; i++) 
        {
            string line = sr.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            string[] parts = line.Split(',');

            Debug.Log(line);
            int id = int.Parse(parts[0]);
            float[] floats = new float[_floatLoopNum];
            for (int j = 0; j < _floatLoopNum; j++)
            {
                floats[j] = float.Parse(parts[j + 2]);
            }

            List<EffectData> effectList = new List<EffectData>();
            //�����̃}�W�b�N�i���o�[��ŏC��
            for (int j = 6; j < parts.Length; j++)
            {
                var data = parts[j].Split('_');
                effectList.Add(new EffectData(MakeClass<IEffect>(data[0]), float.Parse(data[1])));
            }
            CardBaseClass cardBaseClass = new CardBaseClass(id, parts[1], floats[0], floats[1], parts[4], parts[5], effectList);
            _cardList.Add(cardBaseClass);
        }

    }

    public CardBaseClass GetRandomCard() 
    {
        return _cardList[UnityEngine.Random.Range(0, _cardList.Count)];
    }

    /// <summary>
    /// �����񂩂�N���X�̃C���X�^���X���쐬����
    /// </summary>
    /// <param name="className"></param>
    private T MakeClass<T>(string className)
    {
        Debug.Log(className);
        Type type = Type.GetType(className);
        return (T)Activator.CreateInstance(type);
    }
}
