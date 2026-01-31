using System.Collections;
using Enemies;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public LevelData levelData;
    public TowerPlacer towerPlacer;

    private void Awake()
    {
        // Инициализация уровня
        EnemyTargetPoints enemyTargetPoints = Instantiate(levelData.enemyTargetPoints, Vector3.zero, Quaternion.identity);
        Grid grid = Instantiate(levelData.GridPrefab, Vector3.zero, Quaternion.identity);
        towerPlacer.Init(grid);

        // Запускаем корутину спавна врагов
        enemySpawner.StartSpawning(levelData.Waves, enemyTargetPoints);
    }

   
}