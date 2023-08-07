using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

[CustomEditor(typeof(CsvImporter))]
public class CsvImpoterEditor : Editor
{
    readonly int _floatLoopNum = 2;


    public override void OnInspectorGUI()
    {
        var importer = (CsvImporter)target;
        DrawDefaultInspector();

        if (GUILayout.Button("アイテム合成データの作成"))
        {
            Debug.Log("aaaa");
            SetEventData(importer);
        }
    }

    /// <summary>csvファイルのデータからScriptableObjectを作成する</summary>
    private void SetCardData(CsvImporter csvImpoter)
    {
        //if (csvImpoter.csvFile == null)
        //{
        //    Debug.LogError("CSVファイルがセットされていません");
        //    return;
        //}

        //StringReader sr = new StringReader(csvImpoter.csvFile.text);
        ////最初の行はスキップ
        //sr.ReadLine();

        ////ToDo: カードの情報が今は一つしかないためここのループは１
        //for (int i = 0; i < 2; i++)
        //{
        //    string line = sr.ReadLine();

        //    if (string.IsNullOrEmpty(line))
        //    {
        //        break;
        //    }

        //    string[] parts = line.Split(',');
        //    string path = "Assets/InGame/Data/CardData/CardData" + parts[1] + ".asset";
        //    //アイテムのインスタンスをメモリ上に作成
        //    var cardBaseClass = CreateInstance<CardBaseClass>();

        //    Debug.Log(line);
        //    int id = int.Parse(parts[0]);
        //    float[] floats = new float[_floatLoopNum];
        //    floats[0] = float.Parse(parts[2]);
        //    floats[1] = float.Parse(parts[4]);
        //    var gold = int.Parse(parts[7]);

        //    List<EffectData> effectList = new List<EffectData>();
        //    var enhansmentData = parts[8].Split('_');
        //    //ここのマジックナンバー後で修正
        //    for (int j = 9; j < parts.Length; j++)
        //    {
        //        var data = parts[j].Split('_');
        //        effectList.Add(new EffectData(MakeClass<IEffect>(data[0] + ", Assembly-CSharp"), float.Parse(data[1])));
        //    }
        //    Debug.Log(id);
        //    cardBaseClass = new CardBaseClass(id, parts[1], floats[0], parts[3], floats[1],parts[5], null
        //        , gold, new EnhancementData(enhansmentData[0], enhansmentData[1]), effectList);
        //    var asset = (CardBaseClass)AssetDatabase.LoadAssetAtPath(path, typeof(CardBaseClass));


        //    AssetDatabase.CreateAsset(cardBaseClass, path);
            //if (asset == null)
            //{
            //    //指定したパスが存在しなかった場合新規作成
            //   
            //}
            //else
            //{
            //    //指定したパスと同名のファイルが存在した場合はデータの更新
            //    EditorUtility.CopySerialized(cardBaseClass, asset);
            //    AssetDatabase.SaveAssets();
            //}

           // AssetDatabase.Refresh();
        //}
    }

    /// <summary>
    /// イベントのデータを作成する
    /// </summary>
    /// <param name="csvImpoter"></param>
    private void SetEventData(CsvImporter csvImpoter)
    {
        if (csvImpoter.csvFile == null)
        {
            Debug.LogError("CSVファイルがセットされていません");
            return;
        }

        StringReader sr = new StringReader(csvImpoter.csvFile.text);
        //最初の行はスキップ
        sr.ReadLine();
        while (true)
        {
            string line = sr.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            string[] parts = line.Split(',');
            string path = "Assets/InGame/Data/EventData/" + parts[0] + ".asset";
            var eventSelectCs = CreateInstance<EventSelectDataScript>();
            eventSelectCs.SetEventName(parts[0]);
            eventSelectCs.SetEventDescription(parts[1]);
            eventSelectCs.SetProbabilitySuccess(int.Parse(parts[2]));
            string[] eventResultsArray = parts[3].Split('_');
            Debug.Log(eventResultsArray[0]);
            eventSelectCs.SetEventResultsTextArray(eventResultsArray);

            var asset = AssetDatabase.LoadAssetAtPath(path, typeof(EventSelectDataScript)) as EventSelectDataScript;
            if (asset == null)
            {
                //指定したパスが存在しなかった場合新規作成
                AssetDatabase.CreateAsset(eventSelectCs, path);

            }
            else
            {
                //指定したパスと同名のファイルが存在した場合はデータの更新
                EditorUtility.CopySerialized(eventSelectCs, asset);
                AssetDatabase.SaveAssets();
            }
        }
    }

    /// <summary>
    /// 文字列からクラスのインスタンスを作成する
    /// </summary>
    /// <param name="className"></param>
    private T MakeClass<T>(string className)
    {
        Type type = Type.GetType(className);
        Debug.Log(type);
        return (T)Activator.CreateInstance(type);
    }

}


