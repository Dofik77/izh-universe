using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] private OnlineMapsMarkerManager markerManager;
    [SerializeField] private MarkerShirtDetails popup;
    [SerializeField, Tooltip("Markers")] private List<MarkerData> data;
    [SerializeField] private List<PhotoSO> photoData;


    private void Awake()
    {
        MarkerDataSharedDescription description = new MarkerDataSharedDescription();

        data = new List<MarkerData>{
            new MarkerData(53.191212, 56.843837, 2, 0, 1, "Главный корпус оружейного завода", description.main_corp, photoData[0]),
            new MarkerData(53.197900, 56.846705, 1, 1, 2, "Генеральский дом", description.general_house, photoData[1]),
            new MarkerData(53.198576, 56.844027, 0, 2, 3, "Михайловская колонна", description.column, photoData[2]),
            new MarkerData(53.198057, 56.843916, 3, 3, 3, "Музей ИжМаш", description.izhmash, photoData[3]),
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
