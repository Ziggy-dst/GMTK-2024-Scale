using UnityEngine;

public class MaskController : MonoBehaviour
{
    public Transform player;  // 玩家对象
    public Material maskMaterial;  // 遮罩材质
    public float radius = 0.2f;  // 初始半径
    public float radiusChangeSpeed = 0.1f;  // 半径改变速度
    public float minRadius = 0.1f;  // 最小半径
    public float maxRadius = 0.5f;  // 最大半径

    void Update()
    {
        // 使遮罩跟随玩家
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        // 调整半径大小
        if (Input.GetKey(KeyCode.Q))
        {
            radius += radiusChangeSpeed * Time.deltaTime;
            radius = Mathf.Clamp(radius, minRadius, maxRadius);
        }
        if (Input.GetKey(KeyCode.E))
        {
            radius -= radiusChangeSpeed * Time.deltaTime;
            radius = Mathf.Clamp(radius, minRadius, maxRadius);
        }

        // 更新遮罩半径
        maskMaterial.SetFloat("_Radius", radius);
    }
}