using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] private OnlineMapsMarkerManager markerManager;
    [SerializeField] private MarkerShirtDetails popup;
    [SerializeField, Tooltip("Markers")] private List<MarkerData> data;
    private void Awake()
    {
        MarkerDataSharedDescription description = new MarkerDataSharedDescription();

        data = new List<MarkerData>{
            new MarkerData(53.191212, 56.843837, 2, 1, 1,
                "Главный корпус оружейного завода", description.main_corp,
                "https://i.ytimg.com/vi/WUqtMRXR3Bk/maxresdefault.jpg"),
            new MarkerData(53.197900, 56.846705, 1, 2, 2,
                "Генеральский дом", description.general_house,
                "https://upload.wikimedia.org/wikipedia/commons/0/06/KFC_Home_Base.jpg"),
            new MarkerData(53.198576, 56.844027, 0, 3, 3,
                "Михайловская колонна", description.column, 
                "https://pro-dachnikov.com/uploads/posts/2021-11/1637834930_45-pro-dachnikov-com-p-sakura-foto-46.jpg"),
            new MarkerData(53.198057, 56.843916, 3, 3, 3,
                "Музей Ижмаш", description.izhmash,
                "https://pro-dachnikov.com/uploads/posts/2021-11/1637834930_45-pro-dachnikov-com-p-sakura-foto-46.jpg")
        };
       
        if (popup == null)
            popup = GetComponent<MarkerShirtDetails>();

        popup.MarkerDatas = data;
    }
    void Start()
    {
        if (markerManager == null)
            markerManager = GetComponent<OnlineMapsMarkerManager>();

    }
}
