using System;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public TowerDataBase towerDataBase;
    public BuildableTower TowerPrefab;
    public TowerSelectionElement selectionPrefab;

    private void Start()
    {
        for (int i = 0; i < towerDataBase.Towers.Count; i++)
        {
        TowerSelectionElement instanse = Instantiate(selectionPrefab, transform);
        instanse.SetTowerPrefab(towerDataBase.Towers[i]);
        instanse.towerSelector = this;
        }
        
    }
}
