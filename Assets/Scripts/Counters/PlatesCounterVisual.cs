using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private Transform plateVisualPrefab;
        [SerializeField] private PlatesCounter platesCounter;

        private List<GameObject> plateVisualGameObjectList;

        private void Awake()
        {
            plateVisualGameObjectList = new List<GameObject>();
        }

        private void Start()
        {
            platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
            platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        }

        private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
        {
            Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

            float plateOffsetY = 0.1f;
            plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * plateVisualGameObjectList.Count, 0);

            plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
        }

        private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
        {
            GameObject plateGameObject = plateVisualGameObjectList[^1];
            plateVisualGameObjectList.Remove(plateGameObject);
            Destroy(plateGameObject);
        }
    }
}