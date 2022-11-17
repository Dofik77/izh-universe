using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioGuideController : MonoBehaviour
{
    public Slider slider;
    public List<GuideAudioClip> audioClips;
    public Text trackName;
    public Text chapterName;
    public GameObject edgePrefab;
    public float sliderWidth;
    public float sliderHeigt;

    private List<GuideTimecode> guideTimecodes;

    private List<GameObject> edgeInstances = new List<GameObject>();

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

    private void Start()
    {
        slider.GetComponent<RectTransform>().sizeDelta = new Vector2(sliderWidth, sliderHeigt);
    }

    /// <summary>
    /// ����� ������� AudioSource � ��������� Play/Pause. 
    /// </summary>
    public void PlayButtonEvent()
    {
        if (!IsAudioPlaying())
            audioGuideSource.Play();    
        else
            audioGuideSource.Pause();        
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

    public void RewindAudioGuide(int timeInSeconds)
    {
        if (audioGuideSource.time < Mathf.Abs(timeInSeconds))
            audioGuideSource.time = 0;

        if (audioGuideSource.time + timeInSeconds > audioGuideSource.clip.length)
            audioGuideSource.time = audioGuideSource.clip.length;
        else
            audioGuideSource.time += timeInSeconds;
    }

    /// <summary>
    /// ����� ������� ������������
    /// </summary>
    /// <param name="clipId">Id ������������</param>
    public void SetAudioClip(int clipId)
    {
        var currentClip = audioClips[clipId];

        guideTimecodes = currentClip.timecodes;

        audioGuideSource.clip = currentClip.audioClip;
        SetTrackNameText(currentClip.audioClipName);
        SetSliderLenght();
        ResetTimeline();
        DestroyEdges();
        DrawTimecodes(guideTimecodes);
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
    private void SetTrackNameText(string clipName)
    {
        if (trackName == null) return;
        trackName.text = clipName;
    }

    private void SetChapterNameText(List<GuideTimecode> guideTimecodes)
    {
        if (chapterName == null) return;

        chapterName.text = guideTimecodes[0].title;

        for (int i = 0; i < guideTimecodes.Count; i++)
        {
            if (slider.value >= guideTimecodes[i].start && slider.value < guideTimecodes[i].end)
            {
                chapterName.text = guideTimecodes[i].title;
                break;
            }
        }        
    }

    private void DrawTimecodes(List<GuideTimecode> timecodes)
    {
        //���� ��� �������
        float takenSliderWidth = 0;
        float segmentWidth;
        int totalAudioDuration = timecodes[timecodes.Count - 1].end - timecodes[0].start;
        for(int i = 0; i < timecodes.Count - 1; i++)
        {
            float xPosition = CalculateXPositionBySliderWidth(takenSliderWidth, timecodes[i].end - timecodes[i].start, totalAudioDuration, out segmentWidth);
            var edgeInstance = Instantiate(edgePrefab, new Vector3(xPosition, 0, 0), Quaternion.identity);
            edgeInstance.transform.SetParent(transform.GetChild(1), false);
            takenSliderWidth += segmentWidth;
            edgeInstances.Add(edgeInstance);
        }
    }

    private void DestroyEdges()
    {
        if (edgeInstances == null)
            return;

        foreach (var edgeInstance in edgeInstances)
            Destroy(edgeInstance);
        
        edgeInstances.Clear();
    }

    private float CalculateXPositionBySliderWidth(float takenSliderWidth, int segmentDuration, int totalAudioDuration, out float segmentWidth)
    {
        segmentWidth = sliderWidth * segmentDuration / totalAudioDuration;
        return (-sliderWidth / 2) + segmentWidth + takenSliderWidth;
    }

    private void FixedUpdate()
    {
        if (OnAudioPlayed != null && !changingTime && IsAudioPlaying())
        {
            OnAudioPlayed(audioGuideSource.time);
        }
        SetChapterNameText(guideTimecodes);
    }

}
