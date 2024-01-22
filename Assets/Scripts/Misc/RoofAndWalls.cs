using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofAndWalls : MonoBehaviour
{
    [SerializeField]private bool RoofEnabled = true;
    [SerializeField]private GameObject RoofAndWallsGameobject;

    // Start is called before the first frame update
    void Start()
    {
        
        if (RoofEnabled) {
            RoofAndWallsGameobject.SetActive(true);
        }
    }

}
