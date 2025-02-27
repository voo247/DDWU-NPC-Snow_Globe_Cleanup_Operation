using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] public AudioSource generalAudioSource;
    [SerializeField] public AudioSource typingAudioSource;
    public string resourcesPath = "Sounds/SoundEffect/";

    // // AudioSource가 할당되지 않았다면 동적으로 추가
    // // -> 직접 할당
    // private void Start()
    // {
    //
    //     if (generalAudioSource == null)
    //         generalAudioSource = gameObject.AddComponent<AudioSource>();

    //     if (typingAudioSource == null)
    //         typingAudioSource = gameObject.AddComponent<AudioSource>();
    // }

    private IEnumerator Delay(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    public void PlayGeneralSound(string soundEffectName)
    {
        StartCoroutine(Delay(0.5f));
        AudioClip clip = Resources.Load<AudioClip>(resourcesPath + soundEffectName);
        if (clip != null)
        {
            generalAudioSource.clip = clip;
            generalAudioSource.loop = false;
            generalAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("DialogueManager: 사운드 클립을 찾을 수 없음 (" + soundEffectName + ")");
        }
    }

    public void StopGeneralSound()
    {
        if (generalAudioSource.isPlaying)
            generalAudioSource.Stop();
    }

    public void PlayTypingSound(string typingSoundName)
    {
        StartCoroutine(Delay(0.5f));
        AudioClip clip = Resources.Load<AudioClip>(resourcesPath + typingSoundName);
        if (clip != null)
        {
            typingAudioSource.clip = clip;
            typingAudioSource.loop = true;
            typingAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("DialogueManager: 사운드 클립을 찾을 수 없음 (" + typingSoundName + ")");
        }
    }

    public void StopTypingSound()
    {
        if (typingAudioSource.isPlaying)
            typingAudioSource.Stop();
    }
}