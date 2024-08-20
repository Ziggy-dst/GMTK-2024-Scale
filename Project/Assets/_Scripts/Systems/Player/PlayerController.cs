using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;          // 正常移动速度
    private Vector2 moveDirection;        // 移动方向

    [Header("Dash")]
    public float dashSpeed = 15f;         // 冲刺速度
    public float dashDuration = 0.2f;     // 冲刺持续时间
    public float dashCooldown = 1f;       // 冲刺冷却时间
    private bool isDashing = false;       // 是否正在冲刺
    private float dashTime;               // 当前冲刺时间
    private float dashCooldownTime;       // 冲刺冷却时间计时

    [Header("Shoot")]
    public GameObject bulletPrefab;       // 子弹预制体
    public float bulletSpeed = 10f;       // 子弹速度
    public Transform firePoint;           // 子弹发射位置
    public int resourcePerBullet = 1;

    private Vector2 shootDirection;

    void Update()
    {
        // 获取移动输入
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;

        // 检查是否可以冲刺
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time >= dashCooldownTime)
        {
            StartDash();
        }

        // 检查是否射击
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // 冲刺中，直接改变位置
            transform.position += (Vector3)(moveDirection * dashSpeed * Time.fixedDeltaTime);
            dashTime -= Time.fixedDeltaTime;

            if (dashTime <= 0)
            {
                EndDash();
            }
        }
        else
        {
            // 正常移动
            transform.position += (Vector3)(moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        // 角色朝向鼠标指针方向
        RotateTowardsMouse();
    }

    void StartDash()
    {
        print("Start dash");
        isDashing = true;
        dashTime = dashDuration;
        dashCooldownTime = Time.time + dashCooldown;
    }

    void EndDash()
    {
        isDashing = false;
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }
}
