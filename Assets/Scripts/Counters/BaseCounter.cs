using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        private KitchenObjects kitchenObject;
        [SerializeField] private Transform counterTopTransform;
        public static event EventHandler OnAnyObjectDropped;

        public static void ResetStaticData()
        {
            OnAnyObjectDropped = null;
        }

        public virtual void Interact(Player player)
        {
        }

        public virtual void InteractAlt(Player player)
        {
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopTransform;
        }

        public void SetKitchenObject(KitchenObjects kitchenObject)
        {
            this.kitchenObject = kitchenObject;
            if (kitchenObject != null)
            {
                OnAnyObjectDropped?.Invoke(this, EventArgs.Empty);
            }
        }

        public KitchenObjects GetKitchenObject()
        {
            return kitchenObject;
        }

        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return kitchenObject != null;
        }
    }
}