using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelData", menuName = "ScriptableObjects/ModelSO", order = 2)]
public class ModelSO : ScriptableObject
{
    [SerializeField] public List<GameObject> Model;
}
