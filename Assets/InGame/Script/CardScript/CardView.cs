using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] Text _cardCost;
    [SerializeField] Text _cardName;
    [Tooltip("カードの説明文")]
    [SerializeField] Text _cardDescription;
    [SerializeField] Image _cardImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// カードの情報をセットする
    /// Modelに情報が入ったらPresenterを通して呼ばれる関数
    /// </summary>
    /// <param name="card">カードの情報</param>
    public void SetInfo(CardBaseClass card) 
    {
        if (card == null) return;
        _cardCost.text = card.CardCost.ToString();
        _cardName.text = card.Name;
        _cardImage.sprite = card.CardSprite;
        _cardDescription.text = card.CardDescription;

    }
}
