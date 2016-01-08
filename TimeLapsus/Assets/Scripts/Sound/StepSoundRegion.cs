using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StepSoundRegion : ScriptWithController
{

    public List<AudioClip> audioClips;

    private AudioSource source;

    private AudioClip[] internalClipSource;

    protected override void Start()
    {
        base.Start();
        source = Controller.PlayerCharacter.GetComponent<AudioSource>();

        if (audioClips == null || audioClips.Count <= 1)
        {
            Debug.LogError("Define at least 2 audio clips! " + gameObject.name);
            gameObject.SetActive(false);
            return;
        }

        internalClipSource = audioClips.ToArray();
        pickRandomToZero();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        Controller.PlayerController.RegisterStepSoundRegion(this);
    }

    private void pickRandomToZero()
    {
        var elem = Random.Range(1, internalClipSource.Length);
        var tmp = internalClipSource[0];
        internalClipSource[0] = internalClipSource[elem];
        internalClipSource[elem] = tmp;
    }

    internal void PlaySound()
    {
        pickRandomToZero();
        source.clip = internalClipSource[0];
        source.Play();
    }
}
