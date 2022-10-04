using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomUIBubblePopup : MonoBehaviour
{
    public Canvas canvas;
    public GameObject bubble;
    public Text title;
    public RawImage photo;
    public Button button;
    [SerializeField] public List<MarkerData> datas;
    public GameObject detailsPanel;

    private OnlineMapsMarker targetMarker;

    private void OnDownloadPhotoComplete(OnlineMapsWWW www)
    {
        Texture2D texture = new Texture2D(1, 1);
        www.LoadImageIntoTexture(texture);

        photo.texture = texture;
    }

    private void OnMapClick()
    {
        targetMarker = null;
        button.onClick.RemoveAllListeners();
        bubble.SetActive(false);
    }

    private void OnDetailsClick(MarkerData data)
    {
        detailsPanel.SetActive(true);
        PopupDetails popupDetails = GetComponent<PopupDetails>();
        popupDetails.FillFields(data);
        Debug.Log($"{data.description}");
    }

    private void OnMarkerClick(OnlineMapsMarkerBase marker)
    {
        button.onClick.RemoveAllListeners();
        targetMarker = marker as OnlineMapsMarker;

        MarkerData data = marker["data"] as MarkerData;
        if (data == null) return;
        bubble.SetActive(true);
        title.text = data.label;

        button.onClick.AddListener(() => OnDetailsClick(data));

        if (photo.texture != null)
        {
            OnlineMapsUtils.Destroy(photo.texture);
            photo.texture = null;
        }

        OnlineMapsWWW www = new OnlineMapsWWW(data.image_uri);
        www.OnComplete += OnDownloadPhotoComplete;

        UpdateBubblePosition();
    }

    private void UpdateBubblePosition()
    {
        // If no marker is selected then exit.
        if (targetMarker == null) return;

        // Hide the popup if the marker is outside the map view
        if (!targetMarker.inMapView)
        {
            if (bubble.activeSelf) bubble.SetActive(false);
        }
        else if (!bubble.activeSelf) bubble.SetActive(true);

        // Convert the coordinates of the marker to the screen position.
        Vector2 screenPosition = OnlineMapsControlBase.instance.GetScreenPosition(targetMarker.position);

        // Add marker height
        screenPosition.y += targetMarker.height;

        // Get a local position inside the canvas.
        Vector2 point;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, null, out point);

        // Set local position of the popup
        (bubble.transform as RectTransform).localPosition = point;
    }

    public void AddRangeData(List<MarkerData> data)
    {
        datas.AddRange(data);
    }


    private void Start()
    {
        // Subscribe to events of the map 
        OnlineMaps.instance.OnChangePosition += UpdateBubblePosition;
        OnlineMaps.instance.OnChangeZoom += UpdateBubblePosition;
        OnlineMapsControlBase.instance.OnMapClick += OnMapClick;

        if (OnlineMapsControlBaseDynamicMesh.instance != null)
        {
            OnlineMapsControlBaseDynamicMesh.instance.OnMeshUpdated += UpdateBubblePosition;
        }

        if (OnlineMapsCameraOrbit.instance != null)
        {
            OnlineMapsCameraOrbit.instance.OnCameraControl += UpdateBubblePosition;
        }

        if (datas != null)
        {
            foreach (MarkerData data in datas)
            {
                OnlineMapsMarker marker = OnlineMapsMarkerManager.CreateItem(data.longitude, data.latitude);
                marker["data"] = data;
                marker.OnClick += OnMarkerClick;
            }
        }


        // Initial hide popup
        bubble.SetActive(false);
    }
}
