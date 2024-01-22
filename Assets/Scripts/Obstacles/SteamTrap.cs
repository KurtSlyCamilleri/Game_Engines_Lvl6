using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamTrap : MonoBehaviour {
    [SerializeField] public GameObject prefabToSpawn;
    [SerializeField] public float spawnInterval = 3f; // Adjust the interval as needed
    private GameObject spawnedPrefab;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject player;
    [SerializeField] private float damageDelay = 1f;
    [SerializeField] private AudioSource steamAudioSource;
    [SerializeField] private AudioClip steamAudioClip;

    private void Start() {
        // Initial spawn
        SpawnPrefab();
        InvokeRepeating("TogglePrefabVisibility", spawnInterval, spawnInterval);

        // Play steam audio if AudioSource and AudioClip are assigned
        if (steamAudioSource && steamAudioClip) {
            steamAudioSource.clip = steamAudioClip;
        }
    }

    private void SpawnPrefab() {
        spawnedPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        spawnedPrefab.SetActive(false); // Hide the prefab initially
    }

    private void TogglePrefabVisibility() {
        if (spawnedPrefab != null) {
            bool isActive = spawnedPrefab.activeSelf;

            spawnedPrefab.SetActive(!isActive);

            // Play or stop audio based on prefab visibility
            if (steamAudioSource && steamAudioClip) {
                if (!isActive) {
                    steamAudioSource.Play();
                } else {
                    StartCoroutine(FadeOutAudio());
                }
            }
        }
    }

    private IEnumerator FadeOutAudio() {
        float initialVolume = steamAudioSource.volume;

        while (steamAudioSource.volume > 0) {
            steamAudioSource.volume -= initialVolume * Time.deltaTime / damageDelay;
            yield return null;
        }

        steamAudioSource.Stop();
        steamAudioSource.volume = initialVolume; // Reset volume for next play
    }


    private bool damageCooldown = false;

    private void FixedUpdate() {
        Collider playerCollider = player.GetComponent<Collider>();
        Collider enemyCollider = spawnedPrefab.GetComponent<Collider>();

        if (playerCollider.bounds.Intersects(enemyCollider.bounds) && !damageCooldown) {
            StartCoroutine(DealDamageWithDelay());
        }
    }

    private IEnumerator DealDamageWithDelay() {
        damageCooldown = true;
        gameManager.playerHealth -= 1;

        // Adjust the delay time as needed (e.g., WaitForSeconds(1) for a 1-second delay)
        yield return new WaitForSeconds(damageDelay);

        damageCooldown = false;
    }
}
