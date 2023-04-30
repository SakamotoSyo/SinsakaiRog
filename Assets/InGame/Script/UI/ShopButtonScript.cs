using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;

public class ShopButtonScript : MonoBehaviour
{
    [SerializeField] private CardController _cardController;
    [SerializeField] private Image _soldOutImage;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Text _moneyText;
    [SerializeField] private AudioSource _audioSource;
    [Tooltip("GoldÇ™ë´ÇËÇ»Ç©Ç¡ÇΩÇ∆Ç´Ç…égÇ§")]
    [SerializeField] private Animator _warnimgAnim;
    private IPlayerStatus _playerStatus;
    private Action _closeAction;

    private void Start()
    {
        if (_moneyText != null) 
        {
            _moneyText.text = _cardController.CardBaseClass.Gold.ToString();
        }
        _playerStatus = PlayerEventPresenter.PlayerStatus;
    }

    public void Buy() 
    {
        if (_playerStatus.UseGold(_cardController.CardBaseClass.Gold))
        {
            _audioSource.Play();
            var card = new CardBaseClass();
            card.Init(_cardController.CardBaseClass);
            _playerStatus.AddDeckCard(card);
            _soldOutImage.enabled = true;
            _shopButton.enabled = false;
        }
        else 
        {
            _warnimgAnim.SetTrigger("WarningAnim");
        }  
    }

    /// <summary>
    /// èúãéå¯â Ççwì¸ÇµÇΩèÍçá
    /// </summary>
    public void BuyRemovalEffect() 
    {
        //àÍívÇ∑ÇÈÉJÅ[ÉhñºÇåüçıÇµÇƒListÇ©ÇÁäYìñÇµÇΩÇ‡ÇÃÇàÍÇ¬çÌèúÇ∑ÇÈ
        var searchResult = _playerStatus.GetDeckCardList()
            .Where(e => e.Name == _cardController.CardBaseClass.Name).Distinct().ToArray();

        _playerStatus.GetDeckCardList().Remove(searchResult[0]);
        _closeAction?.Invoke();
    }

    /// <summary>
    /// ã≠âªå¯â Ççwì¸ÇµÇΩèÍçá
    /// </summary>
    public void BuyElementEffect()
    {
        var searchResult = _playerStatus.GetDeckCardList()
              .Where(e => e.Name == _cardController.CardBaseClass.Name)
              .Where(e => e.NumberReinforcement == 0)
              .Distinct().ToArray();
        Debug.Log(searchResult[0].Name);
        if (searchResult[0].EnhancementData.CardEnhancement == "Cost")
        {
            searchResult[0].SetEnhancementNum();
            searchResult[0].DecreasedCost();

        }
        else if (searchResult[0].EnhancementData.CardEnhancement == "Power") 
        {
            searchResult[0].SetEnhancementNum();
            searchResult[0].IncreaseEffectPower();
        }
        _closeAction?.Invoke();
    }


    public void SetCloseAction(Action ac)
    {
        _closeAction = ac;
    }
}
