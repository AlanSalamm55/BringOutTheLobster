using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class BurningRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float burningTimerMax;
    }
}