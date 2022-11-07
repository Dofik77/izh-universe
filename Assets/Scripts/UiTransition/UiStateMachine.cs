using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine.UI;
using UniRx;
using Button = UnityEngine.UI.Button;
using UIScreenStateEnum = UIScreenStateClass.UIScreenStateEnum;

public class UiStateMachine : MonoBehaviour
{
    [Header("ScreenList")] 
    [SerializeField] public List<ScreenWindow> screens = new List<ScreenWindow>();

    [Header("Another Fields")] 
    [SerializeField] private float delayForTransition = 1.4f;

    [Header("Map")] [SerializeField] private OnlineMaps map;
    

    private Stack<UIScreenStateEnum> stackScreenStateName = new Stack<UIScreenStateEnum>();

    public void NextScreen(UIScreenStateEnum currentState, UIScreenStateEnum nextState, bool activeMap)
    {
        stackScreenStateName.Push(currentState);
        DisableScreen(currentState);
        SwitchScreenTo(nextState);

        if (activeMap)
            MapNeedToBeActivated();
    }

    public void BackScreen(UIScreenStateEnum currentState, bool activeMap)
    {
        var screen = stackScreenStateName.Pop();
        DisableScreen(currentState);
        SwitchScreenTo(screen);
        
        if (activeMap)
            MapNeedToBeActivated();
    }

    public void SwitchScreenTo(UIScreenStateEnum screenState)
    {
        switch (screenState)
        {
            case UIScreenStateEnum.LogoScreen :
                screens.Find(x => x.WindowState == UIScreenStateEnum.LogoScreen).gameObject.SetActive(true);
                break;
            
            case UIScreenStateEnum.PhotoLogoScreen :
                screens.Find(x => x.WindowState == UIScreenStateEnum.PhotoLogoScreen).gameObject.SetActive(true);
                break;
            
            case UIScreenStateEnum.SubModelsMenuScreen :
                screens.Find(x => x.WindowState == UIScreenStateEnum.SubModelsMenuScreen).gameObject.SetActive(true);
                break;
                
            case UIScreenStateEnum.MainMenuOnMapScreen :
                screens.Find(x => x.WindowState == UIScreenStateEnum.MainMenuOnMapScreen).gameObject.SetActive(true);
                break;
            
            case UIScreenStateEnum.ShirtDetailsScreen :
                screens.Find(x => x.WindowState == UIScreenStateEnum.ShirtDetailsScreen).gameObject.SetActive(true);
                break;
            
            case UIScreenStateEnum.AudioGuideScreen :
                screens.Find(x => x.WindowState == UIScreenStateEnum.AudioGuideScreen).gameObject.SetActive(true);
                break;
            
            case UIScreenStateEnum.ShowModelScreen :
                map.gameObject.SetActive(false);
                screens.Find(x => x.WindowState == UIScreenStateEnum.ShowModelScreen).gameObject.SetActive(true);
                break;
            
            default: 
                break;
        }
    }

    private void DisableScreen(UIScreenStateEnum disableScreen)
    {
        if(disableScreen == UIScreenStateEnum.ZeroState)
            return;
        
        screens.Find(x =>
            x.WindowState == disableScreen).gameObject.SetActive(false);
    }

    private void MapNeedToBeActivated() => map.gameObject.SetActive(true);
    
}
    [Serializable]
    public class UIScreenStateClass
    {
        [Serializable]
        public enum UIScreenStateEnum
        {
            ZeroState,
            LogoScreen,
            PhotoLogoScreen,
            SubModelsMenuScreen,
            MainMenuOnMapScreen,
            ShirtDetailsScreen,
            AudioGuideScreen,
            ShowModelScreen
        }
    }
    