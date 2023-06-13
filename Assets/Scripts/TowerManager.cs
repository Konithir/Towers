using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject TowerGameObject;
    public GameObject TowerRoot;
    public GameObject ProjectilesRoot;

    public bool EndGameLimitReached = false;
    public int TowerCount = 0;
    public const int MaxTowerCount = 100;

    public List<Tower> ListOfTowers = new List<Tower>();
    public List<Projectile> InactiveBullets = new List<Projectile>();
    public List<Projectile> ActiveBullets = new List<Projectile>();

    private static TowerManager _singleton;
    public static TowerManager Get()
    {
        return _singleton;
    }

    #region private

    private void Awake()
    {
        _singleton = this;
    }

    private void Start()
    {
        TowerRoot = new GameObject();
        TowerRoot.name = nameof(TowerRoot);

        ProjectilesRoot = new GameObject();
        ProjectilesRoot.name = nameof(ProjectilesRoot);

        for (int i=0;i<MaxTowerCount;i++)
        {
            var tower = Instantiate(TowerGameObject, Vector3.zero, Quaternion.identity);
            tower.name = "Tower";
            tower.transform.SetParent(TowerRoot.transform);
            tower.SetActive(false);
            ListOfTowers.Add(tower.GetComponent<Tower>());
        }

        var firstTower = SpawnTower(Vector3.zero);
        firstTower.InvokeRotatingAndShooting();
    }
    private Tower FindInactiveTower()
    {
        for(int i=0;i<MaxTowerCount;i++)
        {
            if(ListOfTowers[i].gameObject.activeInHierarchy == false)
            {
                return ListOfTowers[i];
            }
        }
        return null;
    }

    #endregion
    #region public

    public Tower SpawnTower(Vector3 position)
    {
        if (TowerCount < MaxTowerCount - 1 && !EndGameLimitReached)
        {
            var tower = FindInactiveTower();
            tower.gameObject.transform.position = position;
            tower.AmmoCounter = Tower.MaxAmmoCounter;
            tower.transform.rotation = new Quaternion(0, 0, 0, 0);
            tower.gameObject.SetActive(true);
            TowerCount++;
            UIManager.Get().UpdateTowerText(TowerCount);
            return tower;
        }
        else if (!EndGameLimitReached)
        {
            EndGameLimitReached = true;
            foreach (Tower tower in ListOfTowers)
            {
                tower.AmmoCounter = 12;
                tower.InvokeRotatingAndShooting();
            }         
        }
        return null;
    }

    public void TowerDestroyedUpdateText()
    {
        TowerCount--;
        UIManager.Get().UpdateTowerText(TowerCount);
    }

    #endregion
}
