using System;
using DefaultNamespace;
using Enemies;
using UnityEngine;

public class TurretAttackComponent : MonoBehaviour
{
     [Header("References")]
     [SerializeField] Transform towerPivot;
     [SerializeField] private SphereCollider detectionZone;
     [SerializeField] private LayerMask enemyLayer;
     [SerializeField] private float rotationSpeed = 5f;
     private  Enemy currentTarget;
     [SerializeField] private Transform ammoSpawnPosition;
     private Vector3 direction;
     public Ammo ammoPrefab;

     [Header("Stats")] public int damage = 50;
     public float attackCooldown = 3f;

     private float _currentAttackCooldown;

     private void OnEnable()
     {
          _currentAttackCooldown = attackCooldown;
     }


     private void Update()
     {
          FindTarget();
          RotateToTarget();
          UpdateAttackCooldown();
          Attack();
     }

     
     private void FindTarget()
     {
          Vector3 WorldCenter = detectionZone.transform.TransformPoint(detectionZone.center);
          float worldRadius =
               detectionZone.radius * Mathf.Max(
                    detectionZone.transform.lossyScale.x,
                    detectionZone.transform.lossyScale.y,
                    detectionZone.transform.lossyScale.z
                    
               );
          Collider[] hits = Physics.OverlapSphere(WorldCenter, worldRadius, enemyLayer, QueryTriggerInteraction.Ignore);
          if (hits.Length == 0)
          {
               currentTarget = null;
               return;
          }

          float closestDistance = float.MaxValue;
          Enemy closestTarget = null;
          foreach (var hit in hits)
          {
               float distance = (hit.transform.position - towerPivot.position).sqrMagnitude;
               if (distance < closestDistance)
               {
                    closestDistance = distance;
                    closestTarget = hit.gameObject.GetComponent<Enemy>();
               }
          }
          currentTarget = closestTarget;
          
     }
     private void RotateToTarget()
     {
          if (currentTarget == null)
               return;

           direction =
               currentTarget.transform.position - towerPivot.position;

          direction.y = 0f;

          if (direction.sqrMagnitude < 0.0001f)
               return;

          Quaternion targetRotation =
               Quaternion.LookRotation(direction);

          towerPivot.rotation = Quaternion.Slerp(
               towerPivot.rotation,
               targetRotation,
               rotationSpeed * Time.deltaTime
          );
     }

     private void UpdateAttackCooldown()
     {
          if (_currentAttackCooldown < attackCooldown)
          {
               _currentAttackCooldown += Time.deltaTime;
          }
     }

     private void Attack()
     {
          if (currentTarget && _currentAttackCooldown >= attackCooldown)
          {
               Ammo ammo  = Instantiate(ammoPrefab, ammoSpawnPosition.position, Quaternion.identity);
               ammo.Shoot(currentTarget,damage);
              
               _currentAttackCooldown = 0f;
               ammo.transform.rotation = towerPivot.rotation;


          }
          
     }
}
