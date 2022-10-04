using System.Collections;
using System.Collections.Generic;

public class MarkerData
{
    public int id;
    public double latitude;
    public double longitude;
    public string label;
    public string description;
    public string image_uri;

    public MarkerData(double latitude, double longitude, int id = 0, string label = "", string description = "", string image_uri = "")
    {
        this.id = id;
        this.latitude = latitude;
        this.longitude = longitude;
        this.label = label;
        this.description = description;
        this.image_uri = image_uri;
    }
}
