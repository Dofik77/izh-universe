using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTimeline : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Slider slider;
    
    private AudioGuideController audioController;

    void Start()
    {
        if (audioController == null)
            audioController = AudioGuideController.instance;
        audioController.OnAudioPlayed += OnAudioPlayed;
    }

    private void OnAudioPlayed(float time)
    {
        slider.value = time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        audioController.SetTimeline(slider.value);
        audioController.OnAudioPlayed += OnAudioPlayed;
        audioController.SetChangingTime(false);
        ((IPointerUpHandler)slider).OnPointerUp(eventData);        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        audioController.SetChangingTime(true);
        audioController.OnAudioPlayed -= OnAudioPlayed;
        ((IPointerDownHandler)slider).OnPointerDown(eventData);        
    }
}
