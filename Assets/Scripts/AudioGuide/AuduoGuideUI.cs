using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuduoGuideUI : MonoBehaviour
{
    public Button closeBtn;
    public Button playBtn;
    public Button stopBtn;
    public GameObject audioPanel;
    void Start()
    {
        closeBtn.onClick.AddListener(() => ClosePanel());
        playBtn.onClick.AddListener(() => AudioGuideController.instance.PlayButtonEvent());
        stopBtn.onClick.AddListener(() => AudioGuideController.instance.StopAudio());
    }

    private void ClosePanel()
    {
        audioPanel.SetActive(false);
    }
}
