using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "ARModelData", menuName = "ScriptableObjects/ARModelSO", order = 3)]
    public class ARModelSO : ScriptableObject
    {
        [SerializeField] public List<GameObject> Model;
    }
}