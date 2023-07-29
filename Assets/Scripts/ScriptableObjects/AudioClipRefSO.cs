using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class AudioClipRefSO : ScriptableObject
    {
        public AudioClip[] chop;
        public AudioClip[] deliveryFailed;
        public AudioClip[] deliverySucess;
        public AudioClip[] footSteps;
        public AudioClip[] objectDrop;
        public AudioClip[] objectPickup;
        public AudioClip stoveSizzle;
        public AudioClip[] trash;
        public AudioClip[] warning;
    }
}