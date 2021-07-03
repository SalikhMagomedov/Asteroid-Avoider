using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private Camera _mainCamera;
    private float _timer;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (!(_timer <= 0)) return;

        SpawnAsteroid();
        _timer += secondsBetweenAsteroids;
    }

    private void SpawnAsteroid()
    {
        var side = Random.Range(0, 4);
        var spawnPoint = Vector2.zero;
        var direction = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        var worldSpawnPoint = _mainCamera.ViewportToWorldPoint(spawnPoint);

        worldSpawnPoint.z = 0;

        var selectedAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        var asteroidInstance = Instantiate(selectedAsteroid, worldSpawnPoint,
            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        var rb = asteroidInstance.GetComponent<Rigidbody>();

        rb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
    }
}