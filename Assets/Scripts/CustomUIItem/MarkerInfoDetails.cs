using UnityEngine;
using UnityEngine.UI;

public class MarkerInfoDetails : MonoBehaviour
{
    [SerializeField] public Text MarkerDetailsName;
    [SerializeField] public Text Description;
    [SerializeField] public RawImage Photo;
    [SerializeField] public Button AudioGuideBtn;
    [SerializeField] public GameObject AudioPanel;

    private int audioId;

    private void OnDownloadPhotoComplete(OnlineMapsWWW www)
    {
        Texture2D texture = new Texture2D(1, 1);
        www.LoadImageIntoTexture(texture);

        Photo.texture = texture;
    }

    public void FillFields(MarkerData data)
    {
        MarkerDetailsName.text = data.label;
        Description.text = data.description;

        if (Photo.texture != null)
        {
            OnlineMapsUtils.Destroy(Photo.texture);
            Photo.texture = null;
        }

        audioId = data.audioClipId;

        OnlineMapsWWW www = new OnlineMapsWWW(data.image_uri);
        www.OnComplete += OnDownloadPhotoComplete;
    }
    
    private void SetupGuide()
    {
        //AudioPanel.SetActive(true);
        AudioGuideController.instance.SetAudioClip(audioId);
    }

    void Start()
    {
        //closeBtn.onClick.AddListener(() => CloseOnClick());
        AudioGuideBtn.onClick.AddListener(() => SetupGuide());
    }
}
