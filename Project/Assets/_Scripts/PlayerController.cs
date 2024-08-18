using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // 获取输入
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 移动玩家
        Vector2 movement = new Vector2(moveX, moveY);

        if (movement != Vector2.zero)
        {
            // 计算玩家移动方向的角度
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;
            // 旋转玩家
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // 应用移动
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}