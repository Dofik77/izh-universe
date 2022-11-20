using UnityEngine;

public class IzhUniverseLocationService : MonoBehaviour
{
    private OnlineMapsMarker userMarker;
    private bool findOnce;
    private bool isObserving = false;
    private Vector2 position;
    OnlineMapsLocationService locationService;

    private void Start()
    {
        position = new Vector2(53.198057f, 56.843916f);
        locationService = OnlineMapsLocationService.instance;

        if (locationService == null)
        {
            print("Service is not found. Add Location Service Component");
            return;
        }

        ZoomAt(position, 15);
    }

    private void Update()
    {
        if (locationService.IsLocationServiceRunning() && !isObserving)
        {
            ObserveUser();
            isObserving = true;
        }
    }

    private void OnLocationChanged(Vector2 position)
    {
        userMarker.position = position;
        if(findOnce)
            ShowUser(userMarker);
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

    private void ShowUser(OnlineMapsMarker userMarker)
    {
        OnlineMaps.instance.SetPositionAndZoom(userMarker.position.x, userMarker.position.y, 12);
        findOnce = false;
    }

    private void ZoomAt(Vector2 position, int zoom)
    {
        OnlineMaps.instance.SetPositionAndZoom(position.x, position.y, zoom);
    }

    private void ObserveUser()
    {
        userMarker = OnlineMapsMarkerManager.CreateItem(new Vector2(0, 0), null, "User");
        findOnce = true;
        locationService.OnLocationChanged += OnLocationChanged;
        locationService.OnCompassChanged += OnCompassChanged;

#if (UNITY_EDITOR)
        userMarker.position = new Vector2(53.223785f, 56.849894f);
#endif
    }
}
