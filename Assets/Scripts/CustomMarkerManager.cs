using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMarkerManager : MonoBehaviour
{
    [SerializeField] private OnlineMapsMarkerManager markerManager;
    [SerializeField] private CustomUIBubblePopup popup;
    [SerializeField, Tooltip("Markers")] private List<MarkerData> data;
    private void Awake()
    {
        data = new List<MarkerData>{
            new MarkerData(56.8441905225239, 53.2011340885793, 1,
                "Marker 1", "Marker 1 - Description",
                "https://i.ytimg.com/vi/WUqtMRXR3Bk/maxresdefault.jpg"),
            new MarkerData(56.8496014366944, 53.2052574474073, 2,
                "Marker 2", "Marker 2 - Description",
                "https://upload.wikimedia.org/wikipedia/commons/0/06/KFC_Home_Base.jpg"),
            new MarkerData(56.8380977056658, 53.196849051651, 3, 
                "Marker 3", "Marker 3 - Description", 
                "https://pro-dachnikov.com/uploads/posts/2021-11/1637834930_45-pro-dachnikov-com-p-sakura-foto-46.jpg")
        };

        if (popup == null)
            popup = GetComponent<CustomUIBubblePopup>();

        popup.datas = data;
    }
    void Start()
    {
        if (markerManager == null)
            markerManager = GetComponent<OnlineMapsMarkerManager>();

    }
}
