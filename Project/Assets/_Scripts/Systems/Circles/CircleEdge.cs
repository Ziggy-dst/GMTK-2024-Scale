using System;
using _Scripts.Managers;
using DG.Tweening;
using UnityEngine;

public class CircleEdge : MonoBehaviour
{
    private ResourceType _resourceType;
    private CircleArea _circleArea;

    public static Action onCircleExpand;

    private void Start()
    {
        _circleArea = transform.parent.GetComponent<CircleArea>();
        _resourceType = _circleArea.resourceType;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;

        GameManager.Instance.audioManager.PlaySfx("RGBHit");
        Destroy(other.gameObject);

        _circleArea.ExpandAll();

        onCircleExpand?.Invoke();

        print("edge: " + GameManager.Instance.playerController);
        GameManager.Instance.resourceManager.GainResource(_resourceType, GameManager.Instance.playerController.resourcePerBullet);
        // ResourceManager.OnResourceGained?.Invoke(resourceType);

        PlayerParticles.OnBulletHit(other.gameObject.transform.position, _resourceType);
    }
}
