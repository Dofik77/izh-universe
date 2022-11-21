using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace DefaultNamespace
{
    public class ARModelManager : MonoBehaviour
    {
        [SerializeField] private ARModelSO arModel;
        [SerializeField] private OrientationSetter orientationSetter;
        [SerializeField] private List<GameObject> currentModel;
        [SerializeField] private ARRaycastManager raycastManager;

        private ArgumentsHandler<int> argumentsHandlerModelData;

        private int modelId;
        private GameObject model;
        private GameObject spawnModel;

        public void ARManagerInitialize()
        {
            orientationSetter.ChangeOrientation(OrientationSetter.Orientation.Portrait);

            if (currentModel.Count > 0)
            {
                foreach (var obj in currentModel)
                {
                    Destroy(obj);
                }
            }

            argumentsHandlerModelData = ArgumentsHandler<int>.GetInstance();
            modelId = argumentsHandlerModelData.GetArgs();
            model = arModel.Model[modelId];
        }


        private void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                List<ARRaycastHit> touches = new List<ARRaycastHit>();

                raycastManager.Raycast(Input.GetTouch(0).position, touches, TrackableType.Planes);

                if (touches.Count > 0)
                {
                    switch (modelId)
                    {
                        //Korpus
                        case 0 :
                            spawnModel = Instantiate(model, touches[0].pose.position, touches[0].pose.rotation);
                            currentModel.Add(spawnModel); 
                            break;
                        //Gendom
                        case 1 :
                            spawnModel = Instantiate(model, touches[0].pose.position, Quaternion.Euler(-90, 0, 0));
                            currentModel.Add(spawnModel); 
                            break;
                        //Column
                        case 2 :
                            spawnModel = Instantiate(model, touches[0].pose.position, Quaternion.Euler(-90, 0, 0));
                            currentModel.Add(spawnModel); 
                            break;
                        //Izhmash
                        case 3 :
                            spawnModel = Instantiate(model, touches[0].pose.position, Quaternion.Euler(-90, 0, 0));
                            currentModel.Add(spawnModel); 
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}