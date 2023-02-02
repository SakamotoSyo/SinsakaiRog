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

        if (GUILayout.Button("�A�C�e�������f�[�^�̍쐬"))
        {
            Debug.Log("aaaa");
            SetData(importer);
        }
    }

    /// <summary>csv�t�@�C���̃f�[�^����ScriptableObject���쐬����</summary>
    void SetData(CsvImporter csvImpoter)
    {
        if (csvImpoter.csvFile == null)
        {
            Debug.LogError("CSV�t�@�C�����Z�b�g����Ă��܂���");
            return;
        }

        StringReader sr = new StringReader(csvImpoter.csvFile.text);
        //�ŏ��̍s�̓X�L�b�v
        sr.ReadLine();

        //ToDo: �J�[�h�̏�񂪍��͈�����Ȃ����߂����̃��[�v�͂P
        for (int i = 0; i < 1; i++)
        {
            string line = sr.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            string[] parts = line.Split(',');
            string path = "Assets/InGame/Data/CardData/CardData" + parts[1] + ".asset";
            //�A�C�e���̃C���X�^���X����������ɍ쐬
            var cardBaseClass = CreateInstance<CardBaseClass>();

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
                effectList.Add(new EffectData(MakeClass<IEffect>(data[0] + ", Assembly-CSharp"), float.Parse(data[1])));
            }
            Debug.Log(id);
            cardBaseClass = new CardBaseClass(id, parts[1], floats[0], parts[4], floats[1],  parts[5],csvImpoter.Sprite, effectList);
            var asset = (CardBaseClass)AssetDatabase.LoadAssetAtPath(path, typeof(CardBaseClass));

            if (asset == null)
            {
                //�w�肵���p�X�����݂��Ȃ������ꍇ�V�K�쐬
                AssetDatabase.CreateAsset(cardBaseClass, path);
            }
            else
            {
                //�w�肵���p�X�Ɠ����̃t�@�C�������݂����ꍇ�̓f�[�^�̍X�V
                EditorUtility.CopySerialized(cardBaseClass, asset);
                AssetDatabase.SaveAssets();
            }
            AssetDatabase.Refresh();
        }
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


