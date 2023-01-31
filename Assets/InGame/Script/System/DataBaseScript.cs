using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataBaseScript : MonoBehaviour
{
    [Header("�J�[�h�̃e�L�X�g�f�[�^")]
    [SerializeField] private TextAsset _cardData;
    [Header("�J�[�h��Sprite�f�[�^")]
    [SerializeField] private List<SpriteData> _cardSprite = new List<SpriteData>();
    public static List<CardBaseClass> CardBaseClassList => _cardList;
    [Header("�J�[�h�̃f�[�^")]
    private static List<CardBaseClass> _cardList = new List<CardBaseClass>();



    PlayerStatus _status;
    //ToDo: SerializeField������
    [SerializeField] EnemyStatus _enemyStaus;

    readonly int _floatLoopNum = 2;

    private void Awake()
    {
        CardDataInit();
    }

    private void Start()
    {

    }

    private void CardDataInit() 
    {
        StringReader sr = new StringReader(_cardData.text);
        //�ŏ��̍s�̓X�L�b�v
        sr.ReadLine();

        //TODO:�f�[�^�̐�����for������悤�ɂ���
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

            floats[0] = float.Parse(parts[2]);
            floats[1] = float.Parse(parts[4]);

            List<EffectData> effectList = new List<EffectData>();
            //�����̃}�W�b�N�i���o�[��ŏC��
            for (int j = 7; j < parts.Length; j++)
            {
                var data = parts[j].Split('_');
                effectList.Add(new EffectData(MakeClass<IEffect>(data[0]), float.Parse(data[1])));
            }
            
            CardBaseClass cardBaseClass = new CardBaseClass(id, parts[1], floats[0], parts[3], floats[1], parts[5], GetSprite(parts[6]), effectList);
            _cardList.Add(cardBaseClass);
        }

    }

    /// <summary>
    /// Enum�̗v�f�ƕ����񂪈�v������Sprite��Ԃ�
    /// </summary>
    /// <param name="imageType"></param>
    public Sprite GetSprite(string imageType)
    {
        ImageType type = Enum.Parse<ImageType>(imageType);
        return _cardSprite.Find(x => x.ImageType == type).CardSprite;
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
        Type type = Type.GetType(className);
        Debug.Log(type);
        return (T)Activator.CreateInstance(type);
    }
}

[Serializable]
public class SpriteData 
{
    public Sprite CardSprite => _cardSprite;
    [SerializeField] private Sprite _cardSprite;
    public ImageType ImageType => _imageType;
    [SerializeField] private ImageType _imageType;
    
}

public enum ImageType 
{
    Slash,
}