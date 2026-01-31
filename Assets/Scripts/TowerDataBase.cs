using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerDataBase", menuName = "Configs/TowerDataBase")]
public class TowerDataBase : ScriptableObject
{
 public List<BuildableTower> Towers = new List<BuildableTower>();    
}
