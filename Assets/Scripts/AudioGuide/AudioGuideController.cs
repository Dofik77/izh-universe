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
    /// Событие, для отслеживания таймлайна аудиоресурса
    /// </summary>
    public Action<float> OnAudioPlayed;
    
    /// <summary>
    /// Singleton
    /// </summary>
    public static AudioGuideController instance;

    //Main Audio Source
    private AudioSource audioGuideSource;

    //Флаг, сообщающий, изменяет ли пользователь значение слайдера 
    private bool changingTime = false;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        audioGuideSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Метод задания AudioSource в состояния Play/Pause. 
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
    /// Метод задания AudioSource в состояниe Stop, с полседующим сбросом значения слайдера. 
    /// </summary>
    public void StopAudio()
    {
        if(IsAudioPlaying())
            audioGuideSource.Stop();
        ResetTimeline();
    }

    /// <summary>
    /// Метод задания аудиодорожки
    /// </summary>
    /// <param name="clipId">Id аудиодорожки</param>
    public void SetAudioClip(int clipId)
    {
        audioGuideSource.clip = audioClips[clipId].audioClip;
        SetTrackNameText(audioClips[clipId].audioClip);
        SetSliderLenght();
        ResetTimeline();
    }

    /// <summary>
    /// Пользовательское задание точки проигрывания аудиодорожки
    /// </summary>
    /// <param name="time">Время</param>
    public void SetTimeline(float time)
    {        
        if (changingTime)
            audioGuideSource.time = time;
    }

    /// <summary>
    /// Изменение флага, сообщающего, изменяет ли пользователь значение слайдера 
    /// </summary>
    /// <param name="flag">Флаг</param>
    public void SetChangingTime(bool flag)
    {
        changingTime = flag;
    }

    /// <summary>
    /// Играет ли трек сейчас?
    /// </summary>
    /// <returns></returns>
    public bool IsAudioPlaying()
    {
        return audioGuideSource.isPlaying;
    }

    /// <summary>
    /// Задание максимального значения слайдера
    /// </summary>
    private void SetSliderLenght()
    {
        slider.maxValue = audioGuideSource.clip.length;
    }

    /// <summary>
    /// Обнуление таймлайна трека
    /// </summary>
    private void ResetTimeline()
    {
        audioGuideSource.time = 0;
        if (OnAudioPlayed != null)
            OnAudioPlayed(0);
    }

    /// <summary>
    /// Задаём текстовому полю значение названия трека
    /// </summary>
    /// <param name="clip">Трек</param>
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
