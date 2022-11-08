using UnityEngine;
using UnityEngine.UI;

public class AuduoGuideUI : MonoBehaviour
{
    //public Button closeBtn;
    public Button playBtn;
    public GameObject audioPanel;
    public Sprite[] playBtnImages;
    public Text generalTimeTextView;
    public Text curentTimeTextView;


    private AudioGuideController controller;
    private Slider slider;
    void Start()
    {
        if (controller == null)
            controller = AudioGuideController.instance;
        slider = controller.slider;
        //closeBtn.onClick.AddListener(() => ClosePanel());

        playBtn.onClick.AddListener(() => controller.PlayButtonEvent());
        playBtn.onClick.AddListener(ChangeIcon);
    }

    private void ChangeIcon()
    {
        if(controller.IsAudioPlaying())
            playBtn.image.sprite = playBtnImages[1];
        else
            playBtn.image.sprite = playBtnImages[0];
    }

    private string ChangeText(float time)
    {        
        int mins = ((int)(time / 60));
        int secs = ((int)(time % 60));
        return $"{string.Format("{0:d2}", mins)}:{string.Format("{0:d2}", secs)}";
    }

    private void ClosePanel()
    {
        audioPanel.SetActive(false);
    }

    private void Update()
    {
        curentTimeTextView.text = ChangeText(slider.value);
        generalTimeTextView.text = ChangeText(slider.maxValue);
    }
}
