using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkMakerTest : MonoBehaviour
{
    [SerializeField] private OnlineMapsMarkerManager markerManager;
    [SerializeField, Tooltip("Marker")] private List<MarkerData> data;
    [SerializeField] private CustomUIBubblePopup popup;

    private double lat = 56.8701934814453;
    private double lon = 53.2202377319336;
    private void Awake()
    {
        data = new List<MarkerData>{
            new MarkerData(56.8441905225239, 53.2011340885793, 1, "Рыжий няша", "Хочу лису, купите лису D:", "https://i.ytimg.com/vi/WUqtMRXR3Bk/maxresdefault.jpg"),
            new MarkerData(56.8496014366944, 53.2052574474073, 2, "KFC", "Накормите студента, хотя бы котлетой, хотя бы котлетой, уооооуууууу", "https://upload.wikimedia.org/wikipedia/commons/0/06/KFC_Home_Base.jpg"),
            new MarkerData(56.8380977056658, 53.196849051651, 3, "Фудзияма", "Серьёзно? Нет! Но фотка классная", "https://pro-dachnikov.com/uploads/posts/2021-11/1637834930_45-pro-dachnikov-com-p-sakura-foto-46.jpg")
        };

        GetLocation();

        gameObject.GetComponent<OnlineMaps>().SetPositionAndZoom(lon, lat, 17);

        if (popup == null)
            popup = GetComponent<CustomUIBubblePopup>();

        popup.datas = data;
    }
    void Start()
    {
        if(markerManager == null)
            markerManager = GetComponent<OnlineMapsMarkerManager>();
        
    }

    private void GetLocation()
    {
        Input.location.Start();
        if (Input.location.status == LocationServiceStatus.Stopped)
        {
            int num = Random.Range(0, this.data.Count);
            lon = this.data[num].longitude;
            lat = this.data[num].latitude;
            return;
        }
        LocationInfo data = Input.location.lastData;
        lon = data.longitude;
        lat = data.latitude;
        Debug.Log(lon);
        Debug.Log(lat);
    }

}
