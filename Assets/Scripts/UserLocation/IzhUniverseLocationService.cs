using UnityEngine;

public class IzhUniverseLocationService : MonoBehaviour
{
    private OnlineMapsMarker userMarker;

    private void Start()
    {
        userMarker = OnlineMapsMarkerManager.CreateItem(new Vector2(0, 0), null, "User");

        OnlineMapsLocationService locationService = OnlineMapsLocationService.instance;

        if (locationService == null)
        {
            print("Service is not found. Add Location Service Component");
            return;
        }

        locationService.OnLocationChanged += OnLocationChanged;
        locationService.OnCompassChanged += OnCompassChanged;
        
        #if (UNITY_EDITOR)
        userMarker.position = new Vector2(53.223785f, 56.849894f);
        #endif

        OnlineMaps.instance.SetPositionAndZoom(userMarker.position.x, userMarker.position.y, 12);
    }

    private void OnLocationChanged(Vector2 position)
    {
        userMarker.position = position;
        Redraw();
    }

    private void OnCompassChanged(float angle)
    {
        userMarker.rotation = angle;
        Redraw();
    }

    private void Redraw()
    {
        OnlineMaps.instance.Redraw();
    }

}
