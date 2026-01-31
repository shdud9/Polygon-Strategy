using System;
using System.Collections.Generic;

namespace Enemies
{
    [Serializable]
    public class EnemyWave
    {
    public List<WaveEnemyInfo> enemies;    
    }

    [Serializable]
    public class WaveEnemyInfo
    {
        public Enemy enemyPrefab;
        public int count;
        
    }
}
 
 