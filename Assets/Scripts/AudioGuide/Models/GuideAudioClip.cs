using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object ���������� � ���� ������������ � ��������
/// </summary>
/// ����� ������� � Editor'�:  Create -> Guide Audio Clip
[CreateAssetMenu(fileName = "Guide Audio Clip")]
public class GuideAudioClip : ScriptableObject
{
    public AudioClip audioClip;
    public List<GuideTimecode> timecodes;
}
