using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AudioManager
{

    [Tooltip("Audio�̏����i�[���Ă���ϐ�")] private AudioDataBase _params = default;
    [Tooltip("Count����ϐ�")] private int _poolCount = 0;
    [Tooltip("��������Obj��ۑ�����Poll�N���X��List")] private List<Pool> _pool = new();
    private static AudioManager _instance = new();

    static public AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>Prefab�̐���������</summary>
    AudioManager()
    {
        //resources�t�H���_����ǂݍ��ޏ����������ɋL�q����B
        _params = Resources.Load<AudioDataBase>("AudioDataBase");

        if (_params == null)
        {
            Debug.LogError("AudioDataBase��������܂���");
        }
        CreatePool();
    }

    /// <summary>�e��Prefab�𐶐�����</summary>
    public void CreatePool()
    {
        //�S�Ă̐������I�������return
        if (_poolCount >= _params.paramsList.Count)
        {
            return;
        }

        //�ݒ肵�Ă���Prefab�̐���������
        for (int i = 0; i < _params.paramsList[_poolCount].MaxCount; i++)
        {
            var obj = Object.Instantiate(_params.paramsList[_poolCount].SoundPrefab);
            obj.SetActive(false);
            SavePool(obj, _params.paramsList[_poolCount].Type);
        }

        _poolCount++;
        CreatePool();
    }

    /// <summary>
    /// ���𗬂��Ƃ��ɌĂяo���֐�
    /// </summary>
    /// <param name="type">���������T�E���h�̎��</param>
    /// <returns>���𗬂�GameObject</returns>
    public GameObject PlaySound(SoundPlayType type)
    {
        //Debug.Log("�Đ�");
        foreach (var pool in _pool)
        {
            if (pool.Obj.activeSelf == false && pool.Type == type)
            {
                pool.Obj.SetActive(true);
                return pool.Obj;
            }
        }

        //�����������Ă��镪�ő���Ȃ��Ȃ�����A�V������������
        var newObj = Object.Instantiate(_params.paramsList.Find(x => x.Type == type).SoundPrefab);
        SavePool(newObj, type);
        return newObj;
    }

    /// <summary>
    /// ���������I�u�W�F�N�g��List�ɒǉ����ĕۑ�����֐�
    /// </summary>
    /// <param name="obj">���������I�u�W�F�N�g</param>
    /// <param name="type">���������I�u�W�F�N�g��Type</param>
    void SavePool(GameObject obj, SoundPlayType type)
    {
        _pool.Add(new Pool { Obj = obj, Type = type });
    }

    public void Reset()
    {
        _pool.Clear();
    }

    /// <summary>������������ۑ����Ă������߂̃N���X</summary>
    private struct Pool
    {
        public GameObject Obj { get; set; }
        public SoundPlayType Type { get; set; }
    }
}

/// <summary>
/// �炵�������̎��
/// </summary>
public enum SoundPlayType
{
    PlayerAttack,
    EnemyAttack,
    SpecialAttack,
    TurnSwitching,
    Guard,
    Dead,
}

