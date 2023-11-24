using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOne : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider Player) {
        gameManager.playerHealth--;
    }
}
