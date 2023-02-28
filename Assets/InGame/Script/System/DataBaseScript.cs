using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataBaseScript : MonoBehaviour
{
    public static List<CardBaseClass> CardBaseClassList => _cardList;
    public static List<EnemyStatusData> EnemyData => _enemyStatusData;
    public static float BasicReWardGold => _basicReWardGold;
    [Tooltip("�K�w�ɂ��{��")]
    public const float EFFECT_MAGNIFICATION = 0.05f;

    //TODO:��ؐ搶��Script�����Č�X�ύX���΂ɓ����
    [Header("�J�[�h�̃e�L�X�g�f�[�^")]
    [SerializeField] private TextAsset _cardData;
    [Header("�G�̃X�e�[�^�X�f�[�^")]
    [SerializeField] private TextAsset _enemyData;
    [Header("�J�[�h��Sprite�f�[�^")]
    [SerializeField] private  List<CardSpriteData> _cardSprite = new();
    [Tooltip("�K�w�ɂ���ēG�ɂ�����{��")]
    private static List<EnemyStatusData> _enemyStatusData = new(); 
    private static List<CardBaseClass> _cardList = new();
    private readonly int _floatLoopNum = 2;
    [Tooltip("��V�̃x�[�X�ƂȂ�S�[���h")]
    private static int _basicReWardGold = 20;
    private static bool _isInit = false;

    private void Awake()
    {
        if (!_isInit)
        {
            CardDataInit();
            EnemyDataInit();
            PlayerDataInit();
            Debug.Log("Data�̏�����");
            _isInit = true;
        }
    }

    public void Init()
    {
        if (!_isInit) 
        {
            CardDataInit();
            EnemyDataInit();
            PlayerDataInit();
            Debug.Log("Data�̏�����");
            _isInit = true;
        }
    }

    private void CardDataInit() 
    {
        StringReader sr = new StringReader(_cardData.text);
        //�ŏ��̍s�̓X�L�b�v
        sr.ReadLine();

        //TODO:�f�[�^�̐�����for������悤�ɂ���
        while(true) 
        {
            string line = sr.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            string[] parts = line.Split(',');
            int id = int.Parse(parts[0]);
            float[] floats = new float[_floatLoopNum];
            floats[0] = float.Parse(parts[2]);
            floats[1] = float.Parse(parts[4]);
            var gold = int.Parse(parts[7]);
            List<EffectData> effectList = new List<EffectData>();
            var enhansmentData = parts[8].Split('_');
            //�����̃}�W�b�N�i���o�[��ŏC��
            for (int j = 9; j < parts.Length; j++)
            {
                var data = parts[j].Split('_');
                effectList.Add(new EffectData(MakeClass<IEffect>(data[0]), float.Parse(data[1])));
            }
            CardBaseClass cardBaseClass = new CardBaseClass(id, parts[1], floats[0], parts[3], floats[1], parts[5]
                , GetSprite(parts[6]), gold, new EnhancementData(enhansmentData[0], enhansmentData[1]), effectList);
            _cardList.Add(cardBaseClass);
        }

    }

    public void EnemyDataInit() 
    {
        StringReader sr = new StringReader(_enemyData.text);

        //�ŏ��̍s�̓X�L�b�v
        sr.ReadLine();

        while(true)
        {
            string line = sr.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            string[] parts = line.Split(',');
            float MaxHp = float.Parse(parts[1]);
            int BaseLevel = int.Parse(parts[2]);
            string ActionNum = parts[3];
            List<EnemyEffectData> enemyEffectDatas = new List<EnemyEffectData>();

            for (int j = 4; j < parts.Length; j++) 
            {
                var effect = parts[j].Split('_');
                Debug.Log(effect[0]);
                enemyEffectDatas.Add(new EnemyEffectData(MakeClass<IEffect>(effect[0]), Enum.Parse<TargetType>(effect[1]), Enum.Parse<EffectTypeImage>(effect[3]), float.Parse(effect[2])));
            }
            EnemyStatusData enemyStatusData = new EnemyStatusData(parts[0], MaxHp, BaseLevel, ActionNum, enemyEffectDatas);

            _enemyStatusData.Add(enemyStatusData);
        }
    }

    public void PlayerDataInit() 
    {
        var playerData = new PlayerStatusSaveData();
        playerData.MaxHp = 50;
        playerData.Currenthp = playerData.MaxHp;
        playerData.MaxCost = 3;
        playerData.Nowcost = playerData.MaxCost;
        var deckList = new List<CardBaseClass>();
        deckList.Add(CardBaseClassList[CardBaseClassList.Count - 1]);
        for (int i = 0; i < 4; i++)
        {
            deckList.Add(NewCard(CardBaseClassList[0]));
            deckList.Add(NewCard(CardBaseClassList[1]));
        }
        playerData.DeckCardList = new List<CardBaseClass>(deckList);

        GameManager.SavePlayerData(playerData);
    }

    /// <summary>
    /// �l�n���ŏ���n��
    /// </summary>
    /// <param name="cardBase"></param>
    /// <returns></returns>
    public CardBaseClass NewCard(CardBaseClass cardBase) 
    {
        var card = new CardBaseClass();
        card.Init(cardBase);
        return card;
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

    public static CardBaseClass GetRandomCard() 
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
        return (T)Activator.CreateInstance(type);
    }
}

[Serializable]
public class CardSpriteData 
{
    public Sprite CardSprite => _cardSprite;
    [SerializeField] private 
        Sprite _cardSprite;
    public ImageType ImageType => _imageType;
    [SerializeField] private ImageType _imageType;
    
}

public struct EnemyStatusData
{
    public string ActionNum;
    public string Name;
    public int BaseCurrentLevel;
    public float MaxHp;
    public List<EnemyEffectData> enemyEffectDataList;

    public EnemyStatusData(string name, float maxHp, int baseCurrentLevel,string actionNum, List<EnemyEffectData> effectList)
    {
        enemyEffectDataList = new List<EnemyEffectData>(effectList);
        Name = name;
        MaxHp = maxHp;
        BaseCurrentLevel = baseCurrentLevel;
        ActionNum = actionNum;
    }
}

public enum ImageType 
{
    Slash,
    Defence,
    Draw,
    DefenceBreak,
    Strategy,
    Strike,
    Punch,
    Avert,
    Charge,
}