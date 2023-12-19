using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public override void Use() {
        Debug.Log("FoundKey");
    }
}
