using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object содержащий в себе аудиодорожку и таймокды
/// </summary>
/// Чтобы создать в Editor'е:  Create -> Guide Audio Clip
[CreateAssetMenu(fileName = "Guide Audio Clip")]
public class GuideAudioClip : ScriptableObject
{
    public AudioClip audioClip;
    public List<GuideTimecode> timecodes;
}
