using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioController
{
    public static void PlayRandomSound(AudioSource source, AudioClip[] clips)
    {
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}