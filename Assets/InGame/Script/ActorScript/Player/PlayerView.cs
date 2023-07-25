using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : ActorViewBase
{
    [SerializeField] private Text _costText;
    [SerializeField] private Text _discardedText;
    [SerializeField] private Text _deckText;
    [SerializeField] private GameObject _cardParentObj;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Animator _costEffectAnim;
    [SerializeField] private Text _costEffectText;

    public void DrawView(CardBaseClass card)
    {
        var obj = Instantiate(_cardPrefab, _cardParentObj.transform.position, Quaternion.identity);
        obj.transform.SetParent(_cardParentObj.transform);
        var cardController = obj.GetComponentInChildren<CardController>();
        cardController.CardAnimation.SetParentTransform(_cardParentObj.transform);
        cardController.SetCardBaseClass(card);
    }
    public void GraveyardCardsView(int count)
    {
        _discardedText.text = count.ToString();
    }

    public void DeckCardView(int count)
    {
        _deckText.text = count.ToString();
    }

    public async void SetCostText(float cost)
    {
        if (int.Parse(_costText.text) < cost)
        {
            var token = this.GetCancellationTokenOnDestroy();
            _costEffectText.enabled = true;
            _costEffectText.text = (cost - int.Parse(_costText.text)).ToString("0");
            _costText.text = cost.ToString();
            _costEffectAnim.SetTrigger("CostEffect");
            await UniTask.WaitUntil(() => _costEffectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f, cancellationToken: token);
            _costEffectText.enabled = false;
        }
        else 
        {
            _costText.text = cost.ToString();
        }
    }
}
