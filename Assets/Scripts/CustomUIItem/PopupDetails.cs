using UnityEngine;
using UnityEngine.UI;

public class PopupDetails : MonoBehaviour
{
    public Text title;
    public Text description;
    public RawImage photo;
    public Button btn;
    public GameObject detailsPanel;

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

        OnlineMapsWWW www = new OnlineMapsWWW(data.image_uri);
        www.OnComplete += OnDownloadPhotoComplete;
    }

    private void CloseOnClick()
    {
        detailsPanel.SetActive(false);
    }

    void Start()
    {
        btn.onClick.AddListener(() => CloseOnClick());
    }
}
