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
    [SerializeField] private List<ScreenWindow> screens;
    
    [Header("All Event Button")]
    [SerializeField] private Button historyButton;
    [SerializeField] private Button cityButton;
    
    [Header("All Screen")]

    [Header("Another Fields")] 
    [SerializeField] private float delayForTransition = 1.4f;


    private Stack<UIScreenStateEnum> stackScreenStateName = new Stack<UIScreenStateEnum>();
    private UIScreenStateEnum prevScreenState;
    private UIScreenStateEnum nextScreenName;

    private void Start()
    {
        stackScreenStateName.Push(UIScreenStateEnum.startScreen);
    }
    
    public void UpdateActualState(UIScreenStateEnum currentState, UIScreenStateEnum nextState)
    {
        if (currentState > stackScreenStateName.Pop()) 
            NextScreen(currentState, nextState);
        else 
            BackScreen();
    }

    public void NextScreen(UIScreenStateEnum currentState, UIScreenStateEnum nextState)
    {
        stackScreenStateName.Push(currentState);
        SwitchScreenTo(nextState);
    }

    public void BackScreen()
    {
        var screen = stackScreenStateName.Pop();
        SwitchScreenTo(screen);
    }

    private void SwitchScreenTo(UIScreenStateEnum screenState)
    {
        switch (screenState)
        {
            
        }
    }
    
}
    [Serializable]
    public class UIScreenStateClass
    {
        [Serializable]
        public enum UIScreenStateEnum
        {
            startScreen,
            smSoborScreen,
            chooseScreen,
            mainMenuScreen
        }
    }
    