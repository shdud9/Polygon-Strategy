using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float spawnDelay = 0.5f;
        [SerializeField] private float timeBetweenWaves = 10f; // Задержка 10 секунд

        // Счетчик оставшихся врагов в текущей волне
        private int _remainingEnemiesInWave;

        public void StartSpawning(List<EnemyWave> waves, EnemyTargetPoints targetPoints)
        {
            StartCoroutine(SpawnRoutine(waves, targetPoints));
        }

        private IEnumerator SpawnRoutine(List<EnemyWave> waves, EnemyTargetPoints targetPoints)
        {
            foreach (var wave in waves)
            {
                // 1. Считаем общее количество врагов для этой волны
                _remainingEnemiesInWave = 0;
                foreach (var enemyInfo in wave.enemies)
                {
                    _remainingEnemiesInWave += enemyInfo.count;
                }

                // 2. Спавним врагов
                foreach (var enemyInfo in wave.enemies)
                {
                    for (int i = 0; i < enemyInfo.count; i++)
                    {
                        SpawnEnemy(enemyInfo.enemyPrefab, targetPoints);
                        yield return new WaitForSeconds(spawnDelay);
                    }
                }

                // 3. Ждем, пока счетчик не обнулится (пока не умрут все)
                while (_remainingEnemiesInWave > 0)
                {
                    yield return null; // Ждем следующего кадра
                }

                // 4. Все враги погибли. Ждем 10 секунд перед следующей волной
                Debug.Log("Волна окончена. Ждем 10 секунд...");
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            
            Debug.Log("Все волны завершены!");
        }

        private void SpawnEnemy(Enemy prefab, EnemyTargetPoints targetPoints)
        {
            Enemy enemy = Instantiate(prefab, transform.position, Quaternion.identity);
            enemy.Init(targetPoints);

            // Подписываемся на событие Death конкретного экземпляра врага
            enemy.Death += OnEnemyDied;
        }

        // Этот метод вызовется автоматически, когда у врага произойдет событие Death
        private void OnEnemyDied()
        {
            _remainingEnemiesInWave--;
        }
    }
}