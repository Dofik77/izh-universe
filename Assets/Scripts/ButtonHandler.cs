using System;
using UnityEngine;

namespace DefaultNamespace
{
    //public event Action<UIScreenStateClass.UIScreenStateEnum, UIScreenStateClass.UIScreenStateEnum> Spawned;
    /// <summary>
    /// сделать ивенты для события нажатия
    /// StateMachine подписываеться на событие каждой кнопки
    /// кнопка может как передавать 2 стейта, так и не передавать стеейты вовсе
    /// </summary>
    public class ButtonHandler : MonoBehaviour
    {
        [SerializeField] private UIScreenStateClass.UIScreenStateEnum currentState;
        [SerializeField] private UIScreenStateClass.UIScreenStateEnum nextState;
    }
}