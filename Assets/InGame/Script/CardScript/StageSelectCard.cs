using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class StageSelectCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("�J�[�\�������킹���Ƃ��̃J�[�h�̑傫���{��")]
    [SerializeField] private float _cardPickSize;
    [SerializeField] private RectTransform _rectpos;
    [SerializeField] private CardController _controller;
    [SerializeField] private UIOutline _uiLine;
    [SerializeField] private GameObject _cardDirectionObj;
    [SerializeField] private Transform _cardDirectionInsPos;
    [SerializeField] private Image _cardImage;
    [SerializeField] private Text _cardText;

    [Tooltip("���̃J�[�h��Position")]
    private (int X, int Y) _cardMapPosition;
    [Tooltip("")]
    private Vector3 _localScale;
    private StageType _stageType;
    private Tween _clickTween;
    private Tween _pointEnterTween;
    private bool _isNext;

    private void Awake()
    {
        _localScale = _rectpos.localScale;
    }

    private void Start()
    {
        //�I���ł���J�[�h����������
        if (!_uiLine) return;
        transform.DOScale(new Vector3(0.91f, 0.91f, 0.91f), 1f)
                 .SetLoops(-1, LoopType.Yoyo);
    }

    public void SetView(Sprite cardSprite, string cardText)
    {
        _cardImage.sprite = cardSprite;
        _cardText.text = cardText;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_uiLine || _isNext) return;
        _isNext = true;
        //�N���b�N���ꂽ�Ƃ���Player���ǂ��ɐi�񂾂��ۑ�����
        GameManager.PlayerMapPosition = (_cardMapPosition);
        ViewpointShift().Forget();
        Debug.Log($"X{_cardMapPosition.X} Y{_cardMapPosition.Y}�Ɉړ�");
    }

    /// <summary>
    /// �J�[�h��I�񂾎��Ɏ��_���ړ�
    /// </summary>
    private async UniTask ViewpointShift()
    {
        DOTween.To(() => Camera.main.transform.position,
                   x => Camera.main.transform.position = x,
                   new Vector3(transform.position.x, transform.position.y + 700f, Camera.main.transform.position.z), 2f)
                   .SetEase(Ease.OutSine);

        var obj = Instantiate(_cardDirectionObj, _cardDirectionInsPos);
        var directionCs = obj.GetComponent<StageSelectCardDirection>();
        directionCs.SetView(_cardImage.sprite, _cardText.text);
        await directionCs.StartMove();
        await FadeScript.Instance.FadeOut();
        LoadSceneManager.NextStageLoad(_stageType);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_uiLine) return;
        _pointEnterTween = _uiLine.DOColor(new Color(_uiLine.color.r, _uiLine.color.g, _uiLine.color.b, 1), 1.5f)
               .SetLoops(-1, LoopType.Yoyo);
        _rectpos.localScale *= _cardPickSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_uiLine) return;
        _pointEnterTween.Kill();
        _uiLine.color = new Color(_uiLine.color.r, _uiLine.color.g, _uiLine.color.b, 0.2f);
        _rectpos.localScale = _localScale;
    }

    public void SetStageType(StageType stageType)
    {
        _stageType = stageType;
    }

    /// <summary>
    /// �J�[�h��I�������Ƃ���Animation
    /// </summary>
    private void CardSelectAnimation()
    {
        //Camera.main.transform.DOMove(new Vector3()
    }

    /// <summary>
    /// ���̃J�[�h��Map�̂ǂ��ɂ��邩�̏���Set����
    /// </summary>
    public void SetCardMapPos(int x, int y)
    {
        _cardMapPosition.X = x;
        _cardMapPosition.Y = y;
    }

    private void OnDestroy()
    {
        _clickTween.Kill();
    }
}
