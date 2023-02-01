using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] Text _cardCost;
    [SerializeField] Text _cardName;
    [Tooltip("�J�[�h�̐�����")]
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
    /// �J�[�h�̏����Z�b�g����
    /// Model�ɏ�񂪓�������Presenter��ʂ��ČĂ΂��֐�
    /// </summary>
    /// <param name="card">�J�[�h�̏��</param>
    public void SetInfo(CardBaseClass card) 
    {
        if (card == null) return;
        _cardCost.text = card.CardCost.ToString();
        _cardName.text = card.Name;
        _cardImage.sprite = card.CardSprite;
        _cardDescription.text = card.CardDescription;

    }
}
