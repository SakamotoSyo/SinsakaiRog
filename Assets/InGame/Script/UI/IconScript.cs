using System;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class IconScript : MonoBehaviour
{
    [SerializeField] private Image _iconImege;
    [SerializeField] private Image _iconEffectImage;
    [SerializeField] private Text _iconText;
    [SerializeField] private Animator _iconanim;
    [SerializeField] private Animator _effectAnim;
    private readonly float _animSpeed = 1.1f;

    public void SetImage(Sprite image) 
    {
        _iconImege.sprite = image;
        _iconEffectImage.sprite = image;
    }

    public void SetEffectPower(float effectNum) 
    {
        _iconText.text = Math.Floor(effectNum).ToString();
    }

    public async void SelectIcon() 
    {
        var token = this.GetCancellationTokenOnDestroy();
        _effectAnim.enabled = true;
        _iconanim.SetTrigger("Delete");
        await UniTask.Delay(TimeSpan.FromSeconds(_animSpeed), cancellationToken: token);
        Destroy(this.gameObject);
    } 
}
