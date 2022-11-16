using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Assets.Scripts;

public class MarkerShirtDetails : MonoBehaviour
{
    [SerializeField] public Canvas MainCanvas;
    [SerializeField] public GameObject MarkerShirt;
    [SerializeField] public Text ShirtNameText;
    [SerializeField] public RawImage ShirtPhoto;
    [SerializeField] public Button ShirtButton;
    [SerializeField] public List<MarkerData> MarkerDatas;
    [SerializeField] public GameObject MarkerInfoDetails;
    [SerializeField] private int yOffset = 10;

    private OnlineMapsMarker targetMarker;
    private ArgumentsHandler<MarkerData> argumentsHandler;

    private void OnDownloadPhotoComplete(OnlineMapsWWW www)
    {
        Texture2D texture = new Texture2D(1, 1);
        www.LoadImageIntoTexture(texture);

        ShirtPhoto.texture = texture;
    }

    private void OnMapClick()
    {
        targetMarker = null;
        //ShirtButton.onClick.RemoveAllListeners();
        MarkerShirt.SetActive(false);
    }

    private void OnDetailsClick(MarkerData data)
    {
        argumentsHandler.SetArgs(data);
        MarkerShirt.SetActive(false);
        
        //re-write to GameApp logic fill data 
    }

    private void OnMarkerClick(OnlineMapsMarkerBase marker)
    {
        //ShirtButton.onClick.RemoveAllListeners();
        targetMarker = marker as OnlineMapsMarker;

        MarkerData data = marker["data"] as MarkerData;
        if (data == null) return;
        MarkerShirt.SetActive(true);
        ShirtNameText.text = data.label;

        ShirtButton.onClick.AddListener(() => OnDetailsClick(data));
        
        if (ShirtPhoto.texture != null)
        {
            OnlineMapsUtils.Destroy(ShirtPhoto.texture);
            ShirtPhoto.texture = null;
        }

        ShirtPhoto.texture = data.SO.ShirtPhoto;
        
        // OnlineMapsWWW www = new OnlineMapsWWW(data.);
        // www.OnComplete += OnDownloadPhotoComplete;

        UpdateBubblePosition();
    }

    private void UpdateBubblePosition()
    {
        // If no marker is selected then exit.
        if (targetMarker == null) return;

        // Hide the popup if the marker is outside the map view
        if (!targetMarker.inMapView)
        {
            if (MarkerShirt.activeSelf) MarkerShirt.SetActive(false);
        }
        else if (!MarkerShirt.activeSelf) MarkerShirt.SetActive(true);

        // Convert the coordinates of the marker to the screen position.
        Vector2 screenPosition = OnlineMapsControlBase.instance.GetScreenPosition(targetMarker.position);

        // Add marker height
        screenPosition.y += targetMarker.height;

        // Get a local position inside the canvas.
        Vector2 point;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas.transform as RectTransform, screenPosition, null, out point);
        point.y += yOffset;
        
        // Set local position of the popup
        (MarkerShirt.transform as RectTransform).localPosition = point;
    }

    public void AddRangeData(List<MarkerData> data)
    {
        MarkerDatas.AddRange(data);
    }


    private void Start()
    {
        OnlineMaps.instance.OnChangePosition += UpdateBubblePosition;
        OnlineMaps.instance.OnChangeZoom += UpdateBubblePosition;
        OnlineMapsControlBase.instance.OnMapClick += OnMapClick;

        argumentsHandler = ArgumentsHandler<MarkerData>.GetInstance();

        if (OnlineMapsControlBaseDynamicMesh.instance != null)
        {
            OnlineMapsControlBaseDynamicMesh.instance.OnMeshUpdated += UpdateBubblePosition;
        }

        if (OnlineMapsCameraOrbit.instance != null)
        {
            OnlineMapsCameraOrbit.instance.OnCameraControl += UpdateBubblePosition;
        }

        if (MarkerDatas != null)
        {
            foreach (MarkerData data in MarkerDatas)
            {
                OnlineMapsMarker marker = OnlineMapsMarkerManager.CreateItem(data.longitude, data.latitude);
                marker["data"] = data;
                marker.OnClick += OnMarkerClick;
            }
        }
        
        // Initial hide popup
        MarkerShirt.SetActive(false);
    }
}
