using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public int health = 100;
        public float rotationspeed = 90f;
        private int currentWaypointIndex = 0;
        public float speed = 2f;
        
        [HideInInspector] public EnemyTargetPoints targetPoints;

        public event Action Death;

        public void Init(EnemyTargetPoints waypoints)
        {
            targetPoints = waypoints;
            if (targetPoints != null && targetPoints.Waypoints.Length > 0)
            {
                transform.position = targetPoints.Waypoints[0].position;
            }
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("Was " + health);
            health = health - damage;
            Debug.Log("now has " + health);

            if (health <= 0)
            {
                // ВАЖНО: Здесь вызывается метод с НОВЫМ именем
                DieEnemy(); 
            }
        }

        // Метод смерти с измененным названием
        private void DieEnemy()
        {
            // Проверяем, существует ли счетчик, чтобы избежать ошибки, если его забыли повесить на сцену
            if (KillCounter.Instance != null)
            {
                KillCounter.Instance.AddKill();
            }
            else
            {
                Debug.LogWarning("KillCounter Instance не найден! Убедитесь, что объект со скриптом KillCounter есть на сцене.");
            }

            Death?.Invoke();
            Destroy(gameObject);
        }

        private void Update()
        {
            // Защита от ошибок, если точки удалены или враг умирает
            if (targetPoints == null) return;
            
            WalkToWaypoint();
        }

        private void WalkToWaypoint()
        {
            Transform Targetpoint = targetPoints.Waypoints[currentWaypointIndex];
            float step = Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position, Targetpoint.position, step);
            
            Vector3 TargetDirection = Targetpoint.position - transform.position;
            
            if (TargetDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetDirection), Time.deltaTime * rotationspeed);
            }

            if (Vector3.Distance(transform.position, Targetpoint.position) < 0.1f)
            {
                NextWaypoint();
            }
        }

        private void NextWaypoint()
        {
            if (currentWaypointIndex < targetPoints.Waypoints.Length - 1)
            {
                currentWaypointIndex++;
            }   
        }
    }
}