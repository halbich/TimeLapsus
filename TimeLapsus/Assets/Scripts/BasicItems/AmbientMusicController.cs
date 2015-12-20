using UnityEngine;
using System.Collections;

public class AmbientMusicController : MonoBehaviour
{

    private AudioSource audioClip;

    // Use this for initialization
    void Start()
    {
        audioClip = GetComponent<AudioSource>();

        if (audioClip == null)
        {
            Debug.LogError("AudioClipController without AudioSource " + gameObject.name);
            enabled = false;
        }
    }

    IEnumerator quietDown(float time)
    {
        var remainingTime = time;
        var remainingLevel = audioClip.volume;

        while(remainingTime > 0 && audioClip.volume > 0)
        {
            var currentRemainingTime = remainingTime - Time.deltaTime;
            var currentVolume = remainingLevel * currentRemainingTime / remainingTime;

            if (currentVolume < 0)
                currentVolume = 0;

            remainingTime = currentRemainingTime;
            remainingLevel = audioClip.volume = currentVolume;

            yield return null;
        }

        audioClip.volume = 0;
    }

    public void QuietDown(float time)
    {
        StartCoroutine(quietDown(time));
    }
}
