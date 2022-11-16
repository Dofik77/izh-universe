using Assets.Scripts;
using DefaultNamespace;
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
    private ArgumentsHandler<MarkerData> argumentsHandler;
    private ArgumentsHandler<int> argumentsHandlerModelData;
    private MarkerData lastMarkerData;

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

        Photo.texture = data.SO.Photo;

        audioId = data.audioClipId;
        
        argumentsHandlerModelData.SetArgs(data.modelId);

        // OnlineMapsWWW www = new OnlineMapsWWW(data.image_uri);
        // www.OnComplete += OnDownloadPhotoComplete;
    }
    
    private void SetupGuide()
    {
        AudioGuideController.instance.SetAudioClip(audioId);
    }

    void Start()
    {
        argumentsHandlerModelData = ArgumentsHandler<int>.GetInstance();
        argumentsHandler = ArgumentsHandler<MarkerData>.GetInstance();
        //closeBtn.onClick.AddListener(() => CloseOnClick());
        AudioGuideBtn.onClick.AddListener(() => SetupGuide());
    }

    private void Update()
    {
        if(argumentsHandler.GetArgs() != lastMarkerData)
        {
            FillFields(argumentsHandler.GetArgs());
            lastMarkerData = argumentsHandler.GetArgs();
        }
    }
}
