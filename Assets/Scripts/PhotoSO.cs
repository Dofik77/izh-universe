using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PhotoSO", order = 1)]
public class PhotoSO : ScriptableObject
{
    [SerializeField] public Texture2D Photo;
    [SerializeField] public Texture2D ShirtPhoto;
}
