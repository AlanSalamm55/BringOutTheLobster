using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KithcenObjectSo_GameObject
    {
        public KitchenObjectSO KitchenObjectSo;
        public GameObject gameObject;
    }

    [SerializeField] private List<KithcenObjectSo_GameObject> kithcenObjectSoGameObjectList;
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (KithcenObjectSo_GameObject kithcenObjectSoGameObject in kithcenObjectSoGameObjectList)
        {
            kithcenObjectSoGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KithcenObjectSo_GameObject kithcenObjectSoGameObject in kithcenObjectSoGameObjectList)
        {
            if (kithcenObjectSoGameObject.KitchenObjectSo == e.KitchenObjectSo)
            {
                kithcenObjectSoGameObject.gameObject.SetActive(true);
            }
        }
    }
}