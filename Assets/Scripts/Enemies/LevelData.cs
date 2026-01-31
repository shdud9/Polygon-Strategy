using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    
    [CreateAssetMenu(fileName = "LevelData", menuName = "Configs/LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public List<EnemyWave> Waves;
        public Grid GridPrefab; 
        public EnemyTargetPoints enemyTargetPoints;
    }
}