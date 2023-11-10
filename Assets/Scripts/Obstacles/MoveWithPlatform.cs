using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Platform")) {
            transform.SetParent(collision.gameObject.transform);
        }
    }
    private void OnCollisionExit(Collision collision) {
        if (transform.parent == collision.gameObject.transform) {
            transform.SetParent(null);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
