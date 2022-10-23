using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioGuideController : MonoBehaviour
{
    public Slider slider;
    public Text playBtnText;
    public List<GuideAudioClip> audioClips;
    public Text trackName;

    /// <summary>
    /// �������, ��� ������������ ��������� ������������
    /// </summary>
    public Action<float> OnAudioPlayed;
    
    /// <summary>
    /// Singleton
    /// </summary>
    public static AudioGuideController instance;

    //Main Audio Source
    private AudioSource audioGuideSource;

    //����, ����������, �������� �� ������������ �������� �������� 
    private bool changingTime = false;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        audioGuideSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ����� ������� AudioSource � ��������� Play/Pause. 
    /// </summary>
    public void PlayButtonEvent()
    {
        if (!IsAudioPlaying())
        {
            audioGuideSource.Play();
            playBtnText.text = "Pause";
        }            
        else
        {
            audioGuideSource.Pause();
            playBtnText.text = "Play";
        }            
    }

    /// <summary>
    /// ����� ������� AudioSource � ��������e Stop, � ����������� ������� �������� ��������. 
    /// </summary>
    public void StopAudio()
    {
        if(IsAudioPlaying())
            audioGuideSource.Stop();
        ResetTimeline();
    }

    /// <summary>
    /// ����� ������� ������������
    /// </summary>
    /// <param name="clipId">Id ������������</param>
    public void SetAudioClip(int clipId)
    {
        audioGuideSource.clip = audioClips[clipId].audioClip;
        SetTrackNameText(audioClips[clipId].audioClip);
        SetSliderLenght();
        ResetTimeline();
    }

    /// <summary>
    /// ���������������� ������� ����� ������������ ������������
    /// </summary>
    /// <param name="time">�����</param>
    public void SetTimeline(float time)
    {        
        if (changingTime)
            audioGuideSource.time = time;
    }

    /// <summary>
    /// ��������� �����, �����������, �������� �� ������������ �������� �������� 
    /// </summary>
    /// <param name="flag">����</param>
    public void SetChangingTime(bool flag)
    {
        changingTime = flag;
    }

    /// <summary>
    /// ������ �� ���� ������?
    /// </summary>
    /// <returns></returns>
    public bool IsAudioPlaying()
    {
        return audioGuideSource.isPlaying;
    }

    /// <summary>
    /// ������� ������������� �������� ��������
    /// </summary>
    private void SetSliderLenght()
    {
        slider.maxValue = audioGuideSource.clip.length;
    }

    /// <summary>
    /// ��������� ��������� �����
    /// </summary>
    private void ResetTimeline()
    {
        audioGuideSource.time = 0;
        if (OnAudioPlayed != null)
            OnAudioPlayed(0);
    }

    /// <summary>
    /// ����� ���������� ���� �������� �������� �����
    /// </summary>
    /// <param name="clip">����</param>
    private void SetTrackNameText(AudioClip clip)
    {
        if (trackName == null) return;
        trackName.text = clip.name;
    }

    private void FixedUpdate()
    {
        if (OnAudioPlayed != null && !changingTime && IsAudioPlaying())
        {
            OnAudioPlayed(audioGuideSource.time);
        }            
    }

}
