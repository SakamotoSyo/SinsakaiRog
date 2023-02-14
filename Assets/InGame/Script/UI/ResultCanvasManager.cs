using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultCanvasManager : MonoBehaviour
{
    [Tooltip("��V�̐����ꏊ")]
    [SerializeField] private GameObject _buttonInsPos;
    [Tooltip("�J�[�h�̐����ꏊ")]
    [SerializeField] private GameObject _cardInsPos;
    [Tooltip("��V���܂Ƃ߂Ă���e�I�u�W�F�N�g")]
    [SerializeField] private GameObject _reWardParentObj;
    [Tooltip("�J�[�h�̕�V���܂Ƃ߂Ă���e�I�u�W�F�N�g")]
    [SerializeField] private GameObject _cardParentObj;
    [SerializeField] private GameObject _itemPanel;
    [SerializeField] private GameObject _itemButtonPrefab;
    [SerializeField] private GameObject _cardButtonPrefab;
    [Tooltip("�J�[�h���W�܂�Transform")]
    [SerializeField] private Transform _cardTransform;
    [SerializeField] private ActorGenerator _actorGenerator;
    private Button[] _itemButtonArray = new Button[4];
    private GameObject[] _itemObjArray = new GameObject[4];
    private Button[] _cardButtonArray = new Button[3];
    private GameObject[] _cardObjArray = new GameObject[3];

    public void ActiveResultPanel() 
    {
        _itemPanel.SetActive(true);
        ItemButtonReWardIns();
    }

    public void ActiveReWardPanel() 
    {
        _itemPanel.SetActive(true);
        _cardParentObj.SetActive(false);
    }

    /// <summary>
    /// ��V�ƂȂ�A�C�e���𐶐��C�x���g�̓o�^������
    /// </summary>
    public void ItemButtonReWardIns()
    {
        //TODO:�����̂܂��񐔂������_���ɕς��邱�Ƃŕ�V�̑�����ݒ肵����
        for (int i = 0; i < 2; i++) 
        {
           var obj = Instantiate(_itemButtonPrefab, transform.position, transform.rotation);
            _itemObjArray[i] = obj;
            //�{�^����Image��Text�̐ݒ�
            _itemButtonArray[i] = _itemObjArray[i].GetComponent<RewardButtonSetting>().ButtonSetting((ReWardType)i);
            obj.transform.SetParent(_buttonInsPos.transform);
        }

        _itemButtonArray[0].NullCast().onClick.AddListener(() => GetPlayerReWardGold());
        _itemButtonArray[1].NullCast().onClick.AddListener(() => BeginChooseCardScreen());
    }

    /// <summary>
    /// �v���C���[���S�[���h�̕�V���Q�b�g����
    /// </summary>
    public void GetPlayerReWardGold() 
    {
        Destroy(_itemObjArray[0]);
        _actorGenerator.PlayerController.AddReWardGold(DataBaseScript.BasicReWardGold * GameManager.CurremtLevel
            * DataBaseScript.EFFECT_MAGNIFICATION);
        Destroy(_itemButtonArray[0].gameObject);
    }

    /// <summary>
    /// �J�[�h��I������Animation���n�܂�
    /// </summary>
    public void BeginChooseCardScreen() 
    {
        Destroy(_itemObjArray[1]);
        _reWardParentObj.SetActive(false);
        _cardParentObj.SetActive(true);
        //TODO:�}�W�b�N�i���o�[
        //��V�̉񐔕��J�[�h�𐶐�
        for (int i = 0; i < 3; i++) 
        {
            _cardObjArray[i] = Instantiate(_cardButtonPrefab);
            var cardController = _cardObjArray[i].GetComponent<CardController>();
            cardController.SetCardBaseClass
                (DataBaseScript.CardBaseClassList[Random.Range(0, DataBaseScript.CardBaseClassList.Count)]);
            //cardController.CardAnimation.SetParentTransform(_cardInsPos.transform);
            _cardObjArray[i].transform.SetParent(_cardInsPos.transform);
            _cardButtonArray[i] = _cardObjArray[i].GetComponent<Button>();
        }

        for (int i = 0; i < _cardObjArray.Length; i++) 
        {
            var cardObj = _cardObjArray[i];
            _cardButtonArray[i].onClick.AddListener(() => GetReWardCard(cardObj));
        }
    }

    public void GetReWardCard(GameObject obj) 
    {
        //�I�����ꂽ�J�[�h���f�b�L�ɒǉ�����
        _actorGenerator.PlayerController.PlayerStatus.AddDeckCard(obj.GetComponent<CardController>().CardBaseClass);
        for (int i = 0; i < _cardButtonArray.Length; i++) 
        {
            _cardButtonArray[i].enabled = false;
        }
        //�J�[�h��Animation���������Destroy����
        for(int i = 0; i < _cardObjArray.Length; i++) 
        {
            var moveObj = _cardObjArray[i];
            DOTween.To(() => moveObj.transform.localPosition,
                x => moveObj.transform.localPosition = x,
                _cardTransform.position, 0.5f)
                .OnComplete(() => 
                {
                    Destroy(moveObj);
                    ActiveReWardPanel();
                });
        }

        Debug.Log($"{obj.GetComponent<CardController>().CardBaseClass.Name}Get����");
    }

    public void Arrow() 
    {
        Debug.Log("Arrow");
    }
}

public enum ReWardType 
{
    Gold,
    Card,
}

public static class ExtensionMethods
{
    public static T NullCast<T>(this T obj) where T : UnityEngine.Object
        => (obj != null) ? obj : null;
}