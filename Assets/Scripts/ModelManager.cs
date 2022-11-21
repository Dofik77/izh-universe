using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace DefaultNamespace
{
    public class ModelManager : MonoBehaviour
    {
        [SerializeField] private ModelSO models;
        [SerializeField] private Transform ModelPos;
        [SerializeField] private GameObject currentModel;
        [SerializeField] private Transform parentToModel;
        [SerializeField] private OrientationSetter orientationSetter;
        [SerializeField] private Camera camera;


        private ArgumentsHandler<int> argumentsHandlerModelData;

        public void InitializeModel()
        {
            orientationSetter.ChangeOrientation(OrientationSetter.Orientation.LandscapeFixed);
            
            if (currentModel != null)
                DestroyModel();
            
            argumentsHandlerModelData = ArgumentsHandler<int>.GetInstance();
            int modelId = argumentsHandlerModelData.GetArgs();
            GameObject model = models.Model[modelId];
            
            switch (modelId)
            {
                //Korpus
                case 0 : 
                    model = Instantiate(model, ModelPos.position, Quaternion.identity);
                    model.transform.SetParent(parentToModel);
                    ChangeCameraValue(new Vector3(0, -31.6f, -180), Vector3.zero);
                    break;
                //Gendom
                case 1 :
                    model = Instantiate(model, ModelPos.position, Quaternion.Euler(-90,0,0));
                    model.transform.SetParent(parentToModel);
                    model.transform.localPosition += new Vector3(-40, 0, 17);
                    ChangeCameraValue(new Vector3(-25,57,-187), new Vector3(30,0,0));
                    break;
                //Column
                case 2 :
                    model = Instantiate(model, ModelPos.position, Quaternion.Euler(-90,0,0));
                    model.transform.SetParent(parentToModel);
                    ChangeCameraValue(new Vector3(-3,12,-155), new Vector3(10,0,0));
                    break;
                //Izhmash
                case 3 :
                    model = Instantiate(model, ModelPos.position, Quaternion.Euler(-90,0,0));
                    model.transform.SetParent(parentToModel);
                    model.transform.localPosition += new Vector3(-6.4f, 0, -3.2f);
                    ChangeCameraValue( new Vector3(-1, -27f, -126), new Vector3(10,0,0));
                    break;
                
                default:
                    break;
            }
            currentModel = model;
        }

        private void ChangeCameraValue(Vector3 cameraPos, Vector3 cameraRotation = new Vector3())
        {
            camera.transform.localPosition = cameraPos;
            camera.transform.localRotation = Quaternion.Euler(cameraRotation);
        }

        private void DestroyModel()
        {
            Destroy(currentModel);
        }
    }
}