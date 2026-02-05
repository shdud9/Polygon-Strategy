using Enemies;
using UnityEngine;

namespace DefaultNamespace
{
    public class Ammo : MonoBehaviour
    {
        private int damage = 12;
        public float speed = 10f;
        
        private Enemy target;
        public void Shoot(Enemy enemy,int dmg)
        {
            target = enemy;
            damage = dmg;
        }

        private void Update()
        {
            if (target == null)
            {
                Destroy (gameObject); 
            }

            Move();


        }

        private void Move()
        {
            if (target == null)
            {
                Destroy(gameObject);
                    return;
            }
            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.position += dir * Time.deltaTime * speed;
            transform.LookAt(target.transform);
            Vector3 distance = transform.position - target.transform.position;
            if (distance.magnitude < 0.1f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}