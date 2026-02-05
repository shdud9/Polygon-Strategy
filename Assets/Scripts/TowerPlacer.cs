using System;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public CoinsManager coinsManager;
    public Camera cam;
    private Grid _grid;
    public TowerSelector towerSelector;

    private GameplayControls input;



    public void Init(Grid grid)
    {
        _grid = grid;
    } 
    private void Awake()
    {
    input = new GameplayControls();    
    }

    private void OnEnable()
    {
        input.Enable();
        input.Gameplay.Click.performed += context => OnClick();
    }

    private void OnDisable()
    {
        
        input.Gameplay.Click.performed -= context => OnClick();
        input.Disable();
    }
    

    private void OnClick()
    {
        if (towerSelector.TowerPrefab)
        {
            if (coinsManager.CoinsCount >= towerSelector.TowerPrefab.cost) 
            {
            Vector2 mousePosition = input.Gameplay.Point.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                BuildableTile tile = hit.collider.GetComponent<BuildableTile>();
                if (tile == null)
                {
                    return;
                }
                if (tile.IsOccupied)
                { 
                    return; 
                }
                Vector3Int cell = _grid.WorldToCell(hit.point);
                Vector3 center = _grid.GetCellCenterWorld(cell);
                center.y += 0.3f;
                BuildableTower tower = Instantiate(towerSelector.TowerPrefab, center, Quaternion.identity);
                tile.Placetower(tower);
                coinsManager.SpendCoins(towerSelector.TowerPrefab.cost);

                    
                    
                    
            }    
            }
                
        }
        
    }
    
}

