using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayScript : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSouse;

    private void OnEnable()
    {
        _audioSouse.Play();
        StartCoroutine(ActiveTimer());
    }

    IEnumerator ActiveTimer()
    {
        yield return new WaitForSeconds(_audioSouse.clip.length);
        gameObject.SetActive(false);
    }

}
