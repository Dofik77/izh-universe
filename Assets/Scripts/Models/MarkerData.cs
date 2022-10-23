using System;

[Serializable]
public class MarkerData
{
    public int id;
    public double latitude;
    public double longitude;
    public string label;
    public string description;
    public string image_uri;
    public int sprite;
    public int audioClipId;
    
    public MarkerData(double latitude, double longitude, int audioClipId, int id = 0, string label = "", string description = "", string image_uri = "")
    {
        this.id = id;
        this.latitude = latitude;
        this.longitude = longitude;
        this.label = label;
        this.description = description;
        this.image_uri = image_uri;
        this.audioClipId = audioClipId;
    }
}
