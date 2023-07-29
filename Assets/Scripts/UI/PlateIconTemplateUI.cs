using UnityEngine;
using UnityEngine.UI;

public class PlateIconTemplateUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetKitchenObjectSo(KitchenObjectSO kitchenObjectSo)
    {
        image.sprite = kitchenObjectSo.sprite;
    }
}