using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    //Scriptable Object feilds are can be public if you only read them and not write their data. 
    //you can make it private [SerializeField]
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
 