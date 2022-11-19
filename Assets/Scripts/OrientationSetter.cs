using UnityEngine;

public class OrientationSetter : MonoBehaviour
{
    public void ChangeOrientation(Orientation screenOrientation)
    {
        switch (screenOrientation)
        {
            case Orientation.Any:
                Screen.orientation = ScreenOrientation.AutoRotation;
            
                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                break;
        
            case Orientation.Portrait:
                Screen.orientation = ScreenOrientation.Portrait;
                Screen.orientation = ScreenOrientation.AutoRotation;
            
                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
                break;
        
            case Orientation.PortraitFixed:
                Screen.orientation = ScreenOrientation.Portrait;
                break;
        
            case Orientation.Landscape:
                Screen.orientation = ScreenOrientation.Landscape;
                Screen.orientation = ScreenOrientation.AutoRotation;
            
                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                break;
        
            case Orientation.LandscapeFixed:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
        }
    }
    
    public enum Orientation
    {
        Any,
        Portrait,
        PortraitFixed,
        Landscape,
        LandscapeFixed
    }

}