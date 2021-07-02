using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject Projectile;
    public const int MaxAmmoCounter = 12;
    public int AmmoCounter = 12;
    public const float BulletSpeed = 4;

    public const int MinTowerRotation = 15;
    public const int MaxTowerRotation = 45;
    public const float TowerRotationCooldown = 0.5f;
    public const float TowerInitialCooldown = 6f;

    #region private

    private void Awake()
    {
        for(int i = 0; i < MaxAmmoCounter/3; i++)
        {
            var bullet = Instantiate(Projectile, transform.position, Quaternion.identity);
            bullet.transform.SetParent(TowerManager.Get().ProjectilesRoot.transform);
            bullet.SetActive(false);
            bullet.AddComponent<Projectile>();
            TowerManager.Get().InactiveBullets.Add(bullet);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CancelInvoke();
        TowerManager.Get().TowerDestroyedUpdateText();
        collision.gameObject.SetActive(false);
        TowerManager.Get().ActiveBullets.Remove(collision.gameObject);
        TowerManager.Get().InactiveBullets.Add(collision.gameObject);
        gameObject.SetActive(false);
    }

    private void Fire()
    {
        gameObject.SetActive(true);
        ChangeColor(Color.red);
        AmmoCounter--;
        var projectile = GetAndActivateInactiveBullet();
        projectile.transform.position = transform.position + transform.up;
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * BulletSpeed;
        if(AmmoCounter <= 0)
        {
            CancelInvoke();
            ChangeColor(Color.white);
        }
    }

    private GameObject GetAndActivateInactiveBullet()
    {
        GameObject bullet = TowerManager.Get().InactiveBullets[0];
        TowerManager.Get().InactiveBullets.RemoveAt(0);
        TowerManager.Get().ActiveBullets.Add(bullet);
        return bullet;
    }

    private void Rotate()
    {
        this.transform.Rotate(new Vector3(0,0,Random.Range(MinTowerRotation,MaxTowerRotation)));
        Fire();
    }

    private IEnumerator ProcjectileDeath(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if(bullet.activeInHierarchy)
        {
            var tower = TowerManager.Get().SpawnTower(bullet.transform.position);
            if(tower)
            {
                tower.InvokeRotatingAndShooting();
            }
            TowerManager.Get().ActiveBullets.Remove(bullet);
            TowerManager.Get().InactiveBullets.Add(bullet);
            bullet.SetActive(false);
        }
      
    }

    private void ChangeColor(Color color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    #endregion
    #region public

    public void InvokeRotatingAndShooting()
    {
        CancelInvoke();
        ChangeColor(Color.white);
        if (TowerManager.Get().TowerCount != 1 && !TowerManager.Get().EndGameLimitReached)
        {
            InvokeRepeating(nameof(Rotate), TowerInitialCooldown, TowerRotationCooldown);
        }
        else
        {
            InvokeRepeating(nameof(Rotate), 0, TowerRotationCooldown);
        }
    }

    #endregion
}
