using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRate = 0.5f; // 弹幕生成频率
    public float bulletSpeed = 5f; // 弹幕速度

    void Start()
    {
        InvokeRepeating("SpawnBullet", 1f, spawnRate);
    }

    void SpawnBullet()
    {
        Vector2 spawnPosition = GetRandomEdgePosition();
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (Vector2.zero - spawnPosition).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    Vector2 GetRandomEdgePosition()
    {
        float x = 0f, y = 0f;
        int edge = Random.Range(0, 4);
        switch (edge)
        {
            case 0: // Top
                x = Random.Range(-10f, 10f);
                y = 6f;
                break;
            case 1: // Bottom
                x = Random.Range(-10f, 10f);
                y = -6f;
                break;
            case 2: // Left
                x = -10f;
                y = Random.Range(-6f, 6f);
                break;
            case 3: // Right
                x = 10f;
                y = Random.Range(-6f, 6f);
                break;
        }
        return new Vector2(x, y);
    }
}