using UnityEngine;

public class Projectile : MonoBehaviour
{
    private const float MinProjectileLifeTime = 0.25f;
    private const float MaxProjectileLifeTime = 1f;

    private void OnEnable()
    {
        Invoke(nameof(ProcjectileDeath), Random.Range(MinProjectileLifeTime, MaxProjectileLifeTime));
    }

    private void ProcjectileDeath()
    {
        if (gameObject.activeInHierarchy)
        {
            var tower = TowerManager.Get().SpawnTower(gameObject.transform.position);
            if (tower)
            {
                tower.InvokeRotatingAndShooting();
            }
            TowerManager.Get().ActiveBullets.Remove(gameObject);
            TowerManager.Get().InactiveBullets.Add(gameObject);
            gameObject.SetActive(false);
        }

    }
}
