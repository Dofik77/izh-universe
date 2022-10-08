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

        OnlineMaps.instance.SetPositionAndZoom(userMarker.position.x, userMarker.position.y, 17);
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
