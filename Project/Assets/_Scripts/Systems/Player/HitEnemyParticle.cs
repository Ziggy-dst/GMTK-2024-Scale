using UnityEngine;
using System;
using _Scripts.Managers;

namespace _Scripts.Systems.Player
{
    public class HitEnemyParticle : MonoBehaviour
    {
        public static Action<Vector3> OnBulletHitEnemy;
        private ParticleSystem _particleSystem;

        private void OnEnable()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            OnBulletHitEnemy += FireParticles;
        }

        private void OnDisable()
        {
            OnBulletHitEnemy -= FireParticles;
        }

        void FireParticles(Vector3 position)
        {
            transform.position = position;
            // 计算从圆心到切点的向量
            Vector2 direction = GameManager.Instance.enemy.transform.position - position;

            // 计算法线角度，使用 Atan2 得到的角度是与 x 轴的夹角
            float normalAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 为了与切面垂直，我们需要将法线方向旋转90度
            float tangentAngle = normalAngle + 90f; // 顺时针旋转90度

            // 或者使用 -90f 来逆时针旋转90度，如果需要逆时针方向
            // float tangentAngle = normalAngle - 90f;

            // 将粒子系统的旋转设置为与切面垂直
            transform.rotation = Quaternion.AngleAxis(tangentAngle, Vector3.back);

            _particleSystem?.Play();
        }
    }
}