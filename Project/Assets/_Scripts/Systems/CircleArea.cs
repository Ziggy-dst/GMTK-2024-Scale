using System;
using System.Linq;
using _Scripts.Managers;
using DG.Tweening;
using UnityEngine;

public class CircleArea : MonoBehaviour
{
    private Collider2D _circleAreaCollider;

    [Header("Resource")]
    public ResourceType resourceType;
    public float resourceConsumeRate = 1f;

    [Header("Expansion")]
    public float expandRate = 1.02f;
    public float maximumExpansion;

    private void Start()
    {
        _circleAreaCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Player")) circleAreaCollider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // 获取所有重叠的碰撞体
        Collider2D[] overlappingColliders = Physics2D.OverlapPointAll(other.transform.position, LayerMask.GetMask("Circle Area"));

        // foreach (var c in overlappingColliders)
        // {
        //    print(c.name + ": " + c.GetComponent<SpriteRenderer>().sortingOrder);
        // }

        // 找到Order in Layer最高的碰撞体（最上层）
        Collider2D topCollider = overlappingColliders
            .OrderByDescending(collider => collider.GetComponent<SpriteRenderer>().sortingOrder)
            .FirstOrDefault();

        if (topCollider == _circleAreaCollider)
        {
            // 当前对象是最上层的，执行相应的逻辑
            Debug.Log("Top collider triggered: " + gameObject.name);

            // 在这里处理碰撞逻辑
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.resourceManager.ConsumeResource(resourceType, resourceConsumeRate);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if (other.CompareTag("Player")) circleAreaCollider.enabled = true;
    }

    // TODO: check if reach the maximum expansion
    public void Expand()
    {
        transform.DOScale(transform.localScale * expandRate, .1f);
    }
}
