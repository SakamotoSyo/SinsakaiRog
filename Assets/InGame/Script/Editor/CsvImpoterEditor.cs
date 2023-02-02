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
            SetData(importer);
        }
    }

    /// <summary>csvファイルのデータからScriptableObjectを作成する</summary>
    void SetData(CsvImporter csvImpoter)
    {
        if (csvImpoter.csvFile == null)
        {
            Debug.LogError("CSVファイルがセットされていません");
            return;
        }

        StringReader sr = new StringReader(csvImpoter.csvFile.text);
        //最初の行はスキップ
        sr.ReadLine();

        //ToDo: カードの情報が今は一つしかないためここのループは１
        for (int i = 0; i < 1; i++)
        {
            string line = sr.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            string[] parts = line.Split(',');
            string path = "Assets/InGame/Data/CardData/CardData" + parts[1] + ".asset";
            //アイテムのインスタンスをメモリ上に作成
            var cardBaseClass = CreateInstance<CardBaseClass>();

            Debug.Log(line);
            int id = int.Parse(parts[0]);
            float[] floats = new float[_floatLoopNum];
            for (int j = 0; j < _floatLoopNum; j++)
            {
                floats[j] = float.Parse(parts[j + 2]);
            }

            List<EffectData> effectList = new List<EffectData>();
            //ここのマジックナンバー後で修正
            for (int j = 6; j < parts.Length; j++)
            {
                var data = parts[j].Split('_');
                effectList.Add(new EffectData(MakeClass<IEffect>(data[0] + ", Assembly-CSharp"), float.Parse(data[1])));
            }
            Debug.Log(id);
            cardBaseClass = new CardBaseClass(id, parts[1], floats[0], parts[4], floats[1],  parts[5],csvImpoter.Sprite, effectList);
            var asset = (CardBaseClass)AssetDatabase.LoadAssetAtPath(path, typeof(CardBaseClass));

            if (asset == null)
            {
                //指定したパスが存在しなかった場合新規作成
                AssetDatabase.CreateAsset(cardBaseClass, path);
            }
            else
            {
                //指定したパスと同名のファイルが存在した場合はデータの更新
                EditorUtility.CopySerialized(cardBaseClass, asset);
                AssetDatabase.SaveAssets();
            }
            AssetDatabase.Refresh();
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


