using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataBaseScript : MonoBehaviour
{
    //TODO:鈴木先生のScriptを見て後々変更を絶対に入れる
    [Header("カードのテキストデータ")]
    [SerializeField] private TextAsset _cardData;
    [Header("敵のステータスデータ")]
    [SerializeField] private TextAsset _enemyData;
    [Header("カードのSpriteデータ")]
    [SerializeField] private List<SpriteData> _cardSprite = new();
    [Tooltip("階層によって敵にかかる倍率")]
    public const float EFFECT_MAGNIFICATION = 1.1f;
    public static List<EnemyStatusData> EnemyData => _enemyStatusData;
    private static List<EnemyStatusData> _enemyStatusData = new(); 
    public static List<CardBaseClass> CardBaseClassList => _cardList;
    [Header("カードのデータ")]
    private static List<CardBaseClass> _cardList = new();

    readonly int _floatLoopNum = 2;

    public void Init()
    {
        CardDataInit();
        EnemyDataInit();
    }

    private void Start()
    {

    }

    private void CardDataInit() 
    {
        StringReader sr = new StringReader(_cardData.text);
        //最初の行はスキップ
        sr.ReadLine();

        //TODO:データの数だけforが周りようにする
        for (int i = 0; i < 3; i++) 
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

            List<EffectData> effectList = new List<EffectData>();
            //ここのマジックナンバー後で修正
            for (int j = 7; j < parts.Length; j++)
            {
                var data = parts[j].Split('_');
                effectList.Add(new EffectData(MakeClass<IEffect>(data[0]), float.Parse(data[1])));
            }
            
            CardBaseClass cardBaseClass = new CardBaseClass(id, parts[1], floats[0], parts[3], floats[1], parts[5], GetSprite(parts[6]), effectList);
            _cardList.Add(cardBaseClass);
        }

    }

    public void EnemyDataInit() 
    {
        StringReader sr = new StringReader(_enemyData.text);

        //最初の行はスキップ
        sr.ReadLine();

        //TODO:データの数だけforが周りようにする
        for (int i = 0; i < 1; i++) 
        {
            string line = sr.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            string[] parts = line.Split(',');
            float MaxHp = float.Parse(parts[1]);
            int BaseLevel = int.Parse(parts[2]);
            List<EnemyEffectData> enemyEffectDatas = new List<EnemyEffectData>();

            for (int j = 3; j < parts.Length; j++) 
            {
                var effect = parts[j].Split('_');
                Debug.Log(Enum.Parse<EffectTypeImage>(effect[3]));
                enemyEffectDatas.Add(new EnemyEffectData(MakeClass<IEffect>(effect[0]), Enum.Parse<TargetType>(effect[1]), Enum.Parse<EffectTypeImage>(effect[3]), float.Parse(effect[2])));
            }
            EnemyStatusData enemyStatusData = new EnemyStatusData(parts[0], MaxHp, BaseLevel, enemyEffectDatas);

            _enemyStatusData.Add(enemyStatusData);
        }
    }

    /// <summary>
    /// Enumの要素と文字列が一致したらSpriteを返す
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
    /// 文字列からクラスのインスタンスを作成する
    /// </summary>
    /// <param name="className"></param>
    private T MakeClass<T>(string className)
    {
        Type type = Type.GetType(className);
        return (T)Activator.CreateInstance(type);
    }
}

[Serializable]
public class SpriteData 
{
    public Sprite CardSprite => _cardSprite;
    [SerializeField] private 
        Sprite _cardSprite;
    public ImageType ImageType => _imageType;
    [SerializeField] private ImageType _imageType;
    
}

public enum ImageType 
{
    Slash,
    Defence,
    Draw,
}