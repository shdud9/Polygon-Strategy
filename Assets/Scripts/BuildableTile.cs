using UnityEngine;

public class BuildableTile : MonoBehaviour
{
    
    public BuildableTower towerInstance;

    public void Placetower(BuildableTower tower)
    {
        towerInstance = tower;
    }

    public bool IsOccupied => towerInstance != null;



}
