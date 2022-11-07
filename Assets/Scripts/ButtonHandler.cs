using System;
using UnityEngine;
using UnityEngine.UI;
using UIScreenStateEnum = UIScreenStateClass.UIScreenStateEnum;
using ButtonPurposeState = ButtonPurpose.ButtonPurposeState;

namespace DefaultNamespace
{
    public class ButtonHandler : MonoBehaviour
    {
        public event Action<UIScreenStateEnum, UIScreenStateEnum, ButtonPurposeState, bool> OnButtonClick;
        
        [SerializeField] private UIScreenStateEnum currentState;
        [SerializeField] private UIScreenStateEnum nextState;
        [SerializeField] private ButtonPurposeState buttonPurposeState;
        [SerializeField] private Button thisButton;
        [SerializeField] private bool mapNeedToBeActivated = false;

        private void Start()
        {
            thisButton.onClick.AddListener(OnClick);
        }

        void OnClick() => OnButtonClick?.Invoke(currentState, nextState, buttonPurposeState, mapNeedToBeActivated);
    }
}

[Serializable]
public class ButtonPurpose
{
    [Serializable]
    public enum ButtonPurposeState
    {
        Next,
        Back,
        None
    }
}