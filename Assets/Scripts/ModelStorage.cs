using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace DefaultNamespace
{
    public class ModelStorage : MonoBehaviour
    {
        [SerializeField] private ModelSO models;

        [SerializeField] private Transform ModelPos;

        [SerializeField] private GameObject currentModel;

        [SerializeField] private Transform parentToModel;

        private ArgumentsHandler<int> argumentsHandlerModelData;

        public void InitializeModel()
        {
            if (currentModel != null)
                DestroyModel();
            
            argumentsHandlerModelData = ArgumentsHandler<int>.GetInstance();
            int modelId = argumentsHandlerModelData.GetArgs();

            GameObject model = models.Model[modelId];
            
            switch (modelId)
            {
                case 0 :
                    model = Instantiate(model, ModelPos.position, Quaternion.identity);
                    model.transform.SetParent(parentToModel);
                    break;
                case 1 :
                    model = Instantiate(model, ModelPos.position + new Vector3(20,0,26), Quaternion.Euler(-90,0,0));
                    model.transform.SetParent(parentToModel);
                    break;
                case 2 :
                    model = Instantiate(model, ModelPos.position, Quaternion.Euler(-90,0,0));
                    model.transform.SetParent(parentToModel);
                    break;
                case 3 :
                    model = Instantiate(model, ModelPos.position, Quaternion.Euler(-90,0,0));
                    model.transform.SetParent(parentToModel);
                    break;
                
                default:
                    break;
            }
            
            
            currentModel = model;
        }

        private void DestroyModel()
        {
            Destroy(currentModel);
        }
    }
}