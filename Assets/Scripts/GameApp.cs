using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UIScreenStateEnum = UIScreenStateClass.UIScreenStateEnum;
using ButtonPurposeState = ButtonPurpose.ButtonPurposeState;
using Orientation = OrientationSetter.Orientation;

namespace DefaultNamespace
{
    public class GameApp : MonoBehaviour
    {
        [Header("Initial Algoritm")]
        [SerializeField] private UiStateMachine uiStateMachine;

        [Header("App Button")] 
        [SerializeField] private List<ButtonHandler> appButton = new List<ButtonHandler>();
        
        [Header("Delay for App")] 
        [SerializeField] private ModelStorage modelStorage;

        [Header("Orientation")] 
        [SerializeField] private OrientationSetter orientationSetter;

        private void Start()
        {
            foreach (var button in appButton)
                button.OnButtonClick += ChangeScreen;

            ActivatedStartScreens(3);
        }


        private void ChangeScreen(UIScreenStateEnum currentState, 
            UIScreenStateEnum NextState, ButtonPurposeState buttonPurposeState, bool mapNeedToBeActivated = false)
        {
            if (NextState == UIScreenStateEnum.ShowModelScreen)
                modelStorage.InitializeModel();
            
            if (currentState == UIScreenStateEnum.ShowModelScreen && buttonPurposeState == ButtonPurposeState.Back)
                orientationSetter.ChangeOrientation(Orientation.PortraitFixed);
            
            switch (buttonPurposeState)
            {
                case ButtonPurposeState.Next :
                    uiStateMachine.NextScreen(currentState, NextState, mapNeedToBeActivated);
                    break;
                
                case ButtonPurposeState.Back :
                    uiStateMachine.BackScreen(currentState, mapNeedToBeActivated);
                    break;
            }
        }
        
        
        private void ActivatedStartScreens(int screenCount)
        {
            for (int stateNumber = 1; stateNumber <= screenCount; stateNumber++)
            {
                UIScreenStateEnum nextState = (UIScreenStateEnum)stateNumber;
                UIScreenStateEnum currentState = (UIScreenStateEnum)stateNumber - 1;
                DelayBetweenStartScreen(currentState, nextState, stateNumber * 2f);
            }
        }

        private void DelayBetweenStartScreen(UIScreenStateEnum currentState, UIScreenStateEnum nextState, float delay)
        {
            gameObject.transform.DOMove(Vector3.zero, delay).OnComplete(() =>
            {
                ChangeScreen(currentState, nextState, ButtonPurposeState.Next);
            });
        }
    }
}