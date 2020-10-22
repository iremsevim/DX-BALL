using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SFX : MonoBehaviour
{
    public static SFX instance;

    public UnityEvent OnStartSFX;
    public AudioSource audioSource;
    public List<Audioprofil> audioprofils;
    private void Awake()
    {
        instance= this;
    }

    public void PlaySound(string audios)
    {

   Audioprofil audio= audioprofils.Find(x => x.audioID == audios);
        OnStartSFX?.Invoke();
       audioSource.PlayOneShot(audio.clips);

    }
    [System.Serializable]
    public class Audioprofil
    {
        public string audioID;
        public AudioClip clips;

    }
}
