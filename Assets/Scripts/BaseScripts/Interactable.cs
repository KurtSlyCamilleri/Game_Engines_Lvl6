using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public abstract void Use();
}
