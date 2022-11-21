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
        [Header("Map")] 
        [SerializeField] private OnlineMaps map;

        [Header("Initial Algoritm")]
        [SerializeField] private UiStateMachine uiStateMachine;

        [Header("App Button")] 
        [SerializeField] private List<ButtonHandler> appButton = new List<ButtonHandler>();
        
        [Header("Model Manager")] 
        [SerializeField] private ModelManager modelManager;

        [Header("AR Manager")] 
        [SerializeField] private ARModelManager arModelManager;
        
        [Header("Orientation")] 
        [SerializeField] private OrientationSetter orientationSetter;

        private void Start()
        {
            map.gameObject.SetActive(true);
            
            foreach (var button in appButton)
                button.OnButtonClick += ChangeScreen;

            ActivatedStartScreens(3);
        }


        private void ChangeScreen(UIScreenStateEnum currentState, 
            UIScreenStateEnum NextState, ButtonPurposeState buttonPurposeState, bool mapNeedToBeActivated = false)
        {
            CheckСrutchCase(NextState, currentState, buttonPurposeState);
            
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


        private void CheckСrutchCase(UIScreenStateEnum NextState, UIScreenStateEnum currentState, ButtonPurposeState buttonPurposeState)
        {
            if(NextState != UIScreenStateEnum.ShowARScreen)
                arModelManager.gameObject.SetActive(false);

            if (NextState == UIScreenStateEnum.ShowModelScreen)
                modelManager.ModelManagerInitialize();

            if (NextState == UIScreenStateEnum.ShowARScreen)
                arModelManager.ARManagerInitialize();
            
            if (currentState == UIScreenStateEnum.ShowModelScreen && buttonPurposeState == ButtonPurposeState.Back)
                orientationSetter.ChangeOrientation(Orientation.PortraitFixed);
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