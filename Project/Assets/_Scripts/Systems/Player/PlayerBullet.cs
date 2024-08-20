using System;
using UnityEngine;

namespace _Scripts.Systems.Player
{
    public class PlayerBullet : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                HitEnemyParticle.OnBulletHitEnemy?.Invoke(transform.position);
                other.gameObject.GetComponent<Enemy>().Shrink();
                Destroy(gameObject);
            }
        }
    }
}