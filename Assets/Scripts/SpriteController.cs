using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;

    private GameObject[] cubes;
    private int currentCubeIndex = 0;

    //edit later to make it change with angle instead of angle change
    //apply future iterations to work with all 2d assets within the 3d plane

    void Start() {
        // Initialize the array of cubes
        cubes = new GameObject[] { cube1, cube2, cube3, cube4 };

        // Activate the first cube, deactivate others
        cubes[currentCubeIndex].SetActive(true);
        for (int i = 1; i < cubes.Length; i++) {
            cubes[i].SetActive(false);
        }
    }

    void Update() {
        // Check if the player presses the shift key
        if (Input.GetKeyDown(KeyCode.Tab)) {
            // Deactivate the current cube
            cubes[currentCubeIndex].SetActive(false);

            // Move to the next cube or wrap around to the first
            currentCubeIndex = (currentCubeIndex + 1) % cubes.Length;

            // Activate the new current cube
            cubes[currentCubeIndex].SetActive(true);
        }
    }
}
