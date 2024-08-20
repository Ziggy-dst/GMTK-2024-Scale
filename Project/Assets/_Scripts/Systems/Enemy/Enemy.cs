using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Base Settings")]
    public float growthFactor;
    public float drainedGrowthFactor = 1.5f;
    public float shrinkFactor = 1.2f;

    [Header("Chase Settings")]
    public Transform chaseTarget;
    public float moveSpeed;

    [Header("Shoot Settings")]
    public float positionUpdateInterval = 2f; // 追逐玩家时，更新位置的间隔时间
    public float stopDistance = 5f; // 敌人停止的距离

    public float shootDelay = 2f; // 停止后发射弹幕的延迟时间
    public GameObject bulletPrefab; // 子弹的预制件
    public Transform firePoint; // 子弹发射的位置
    public int bulletCount = 20; // 弹幕的子弹数量
    public float bulletSpeed = 5f; // 子弹的速度

    private float shootCooldown = 0f; // 发射的冷却时间

    private Vector3 lastKnownPlayerPosition; // 记录最后一次定位到的玩家位置
    private float positionUpdateTimer = 0f; // 位置更新计时器
    // private float enemyRadius; // 敌人的半径

    private float currentGrowthFactor;

    private void Start()
    {
        currentGrowthFactor = growthFactor;
        // 计算敌人的半径
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // enemyRadius = (spriteRenderer.bounds.size.x / 2);
        lastKnownPlayerPosition = chaseTarget.position;
    }

    void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.InGame) return;
        AutoGrow();

        Chase();

        // shootCooldown -= Time.deltaTime;
        // if (shootCooldown <= 0f)
        // {
        //     ShootBullet();
        //     shootCooldown = shootDelay;
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果碰撞到玩家，游戏结束
        if (other.transform == chaseTarget)
        {
            GameManager.Instance.audioManager.PlaySfx("PlayerHit");
            Destroy(other.gameObject);
            GameManager.Instance.ChangeGameState(GameState.Lose);
        }
    }

    private void Chase()
    {
        // 定位玩家并间隔更新位置
        positionUpdateTimer -= Time.deltaTime;
        if (positionUpdateTimer <= 0f)
        {
            lastKnownPlayerPosition = chaseTarget.position;
            positionUpdateTimer = positionUpdateInterval;
        }

        // 计算敌人圆的边缘到玩家的距离
        float distanceToPlayer = Vector2.Distance(transform.position, lastKnownPlayerPosition);

        if (distanceToPlayer > stopDistance)
        {
            // 追逐玩家
            Vector2 direction = (lastKnownPlayerPosition - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, lastKnownPlayerPosition, moveSpeed * Time.deltaTime);
        }
    }

    void ShootBullet()
    {
        // 发射弹幕
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulletVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDirection = (bulletVector - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

            angle += angleStep;
        }
    }

    public void ChangeGrowthSpeed(bool isDrained = false)
    {
        if (isDrained) currentGrowthFactor = drainedGrowthFactor;
        else currentGrowthFactor = growthFactor;
    }

    // 自动增长
    private void AutoGrow()
    {
        float currentScale = transform.localScale.x;
        float increment = currentGrowthFactor / (currentScale + 1);
        float newScale = currentScale + increment * Time.deltaTime;
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    // 敌人死亡
    private void Die()
    {
        Destroy(gameObject);
        // 动画和游戏结束逻辑

        GameManager.Instance.ChangeGameState(GameState.Win);
    }

    // 缩小
    public void Shrink()
    {
        float currentScale = transform.localScale.x;
        float decrement = (growthFactor * currentScale) / (currentScale + 1);
        float newScale = currentScale - decrement * shrinkFactor;
        transform.localScale = new Vector3(newScale, newScale, newScale);

        if (transform.localScale.x <= 0) Die();
    }
}
