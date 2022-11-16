using System;

[Serializable]
public class MarkerData
{
    public int id;
    public double latitude;
    public double longitude;
    public string label;
    public string description;
    public PhotoSO SO;
    public int sprite;
    public int audioClipId;
    public int modelId;
    
    public MarkerData(double longitude, double latitude, int audioClipId, int modelId, 
        int id = 0, string label = "", string description = "", PhotoSO SO = null)
    {
        this.id = id;
        this.latitude = latitude;
        this.longitude = longitude;
        this.label = label;
        this.description = description;
        this.SO = SO;
        this.audioClipId = audioClipId;
        this.modelId = modelId;
    }
}
