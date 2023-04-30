using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

public class FieldTest : MonoBehaviour
{
    [TagName]
    [SerializeField] private string _cardTag;
    [SerializeField] private ActorGenerator _actorGenerator;
    [SerializeField] private UIOutline _uiOutCs;
    [SerializeField] private DataBaseScript _dataBase;
    [SerializeField] private GameObject _particleObj;

    private GameObject _controller;
    private PlayerController _playCon;
    private EnemyController _enemyCon;
    private CancellationToken _cancellationToken;

    private void Start()
    {
        _playCon = _actorGenerator.PlayerController;
        _enemyCon = _actorGenerator.EnemyController;
        _cancellationToken = this.GetCancellationTokenOnDestroy();
    }

    private void Update()
    {
        MouseUpEvent();
    }

    /// <summary>
    /// Mouseの左クリックが終った時のイベント
    /// </summary>
    private void MouseUpEvent() 
    {
        if (Input.GetMouseButtonUp(0) && _controller != null)
        {
            var card = _controller.GetComponent<CardController>().CardBaseClass;
            _uiOutCs.enabled = false;
            //カードを使用するかどうか
            if (_playCon.UseCost(card.CardCost)) 
            {
                card.UseEffect(_playCon, _enemyCon, card.Tartget);
                _playCon.PlayerAnim.AttackAnim(_cancellationToken).Forget();
                _playCon.PlayerStatus.GraveyardCardsAdd(card);
                //OutLineも生成
                Instantiate(_particleObj, Camera.main.ScreenToWorldPoint(Input.mousePosition
                             + new Vector3(0, 0, 10)), Quaternion.identity);
                Destroy(_controller.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_cardTag))
        {
            _uiOutCs.enabled = true;
            _controller = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_cardTag))
        {
            _uiOutCs.enabled = false;
            _controller = null;
        }
    }
}
