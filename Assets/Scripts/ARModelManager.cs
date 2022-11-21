using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace DefaultNamespace
{
    public class ARModelManager : MonoBehaviour
    {
        [SerializeField] private ARModelSO arModel;
        [SerializeField] private ARTrackedImageManager imageManager;
        [SerializeField] private OrientationSetter orientationSetter;
        [SerializeField] private GameObject currentModel;
        
        private ArgumentsHandler<int> argumentsHandlerModelData;

        public void ARInitialize()
        {
            orientationSetter.ChangeOrientation(OrientationSetter.Orientation.Portrait);

            if (currentModel != null)
                Destroy(currentModel);

            argumentsHandlerModelData = ArgumentsHandler<int>.GetInstance();
            int modelId = argumentsHandlerModelData.GetArgs();
            GameObject model = arModel.Model[modelId];

            currentModel = model;
            imageManager.trackedImagePrefab = model;
        }
    }
}