using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();
    public void SetKitchenObject(KitchenObjects kitchenObject);
    public KitchenObjects GetKitchenObject();
    public void ClearKitchenObject();
    public bool HasKitchenObject();
} 