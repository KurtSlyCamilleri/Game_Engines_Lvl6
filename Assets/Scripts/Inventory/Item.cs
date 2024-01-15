using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

[CreateAssetMenu(menuName = "custom/item")]
public class Item : ScriptableObject{//scriptable object creates an asset file
    //Properties needed: name,

    public string name;
    public Sprite sprite;
    
}
