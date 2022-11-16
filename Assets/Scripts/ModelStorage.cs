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
            model = Instantiate(model, ModelPos.position, Quaternion.identity);
            model.transform.SetParent(parentToModel);
            currentModel = model;
        }

        private void DestroyModel()
        {
            Destroy(currentModel);
        }
    }
}