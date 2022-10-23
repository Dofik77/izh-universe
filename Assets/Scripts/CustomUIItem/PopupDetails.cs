using UnityEngine;
using UnityEngine.UI;

public class PopupDetails : MonoBehaviour
{
    public Text title;
    public Text description;
    public RawImage photo;
    public Button closeBtn;
    public Button audioGuideBtn;
    public GameObject detailsPanel;
    public GameObject audioPanel;

    private int audioId;

    private void OnDownloadPhotoComplete(OnlineMapsWWW www)
    {
        Texture2D texture = new Texture2D(1, 1);
        www.LoadImageIntoTexture(texture);

        photo.texture = texture;
    }

    public void FillFields(MarkerData data)
    {
        title.text = data.label;
        description.text = data.description;

        if (photo.texture != null)
        {
            OnlineMapsUtils.Destroy(photo.texture);
            photo.texture = null;
        }

        audioId = data.audioClipId;

        OnlineMapsWWW www = new OnlineMapsWWW(data.image_uri);
        www.OnComplete += OnDownloadPhotoComplete;
    }

    private void CloseOnClick()
    {
        detailsPanel.SetActive(false);
    }

    private void SetupGuide()
    {
        audioPanel.SetActive(true);
        AudioGuideController.instance.SetAudioClip(audioId);
    }

    void Start()
    {
        closeBtn.onClick.AddListener(() => CloseOnClick());
        audioGuideBtn.onClick.AddListener(() => SetupGuide());
    }
}
