 using System;
 using UnityEngine;
 using UnityEngine.UI;

 public class TowerSelectionElement : MonoBehaviour
{
    public Image towerIcon;
    public TowerSelector towerSelector;
    private BuildableTower towerPrefab;
    public Button button; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectTower()
    {
        towerSelector.TowerPrefab = towerPrefab;
        
    }

    private void Awake()
    {
        button.onClick.AddListener(SelectTower);
    }

    public void SetTowerPrefab(BuildableTower towerPrefab)
    {
        this.towerPrefab = towerPrefab;
        towerIcon.sprite = towerPrefab.icon;
    }
}
