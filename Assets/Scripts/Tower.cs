using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //[SerializeField]//Can't drag and drop unfortunatelly
    private TowerManager _towerManager;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public GameObject Projectile;
    public const int MaxAmmoCounter = 12;
    public int AmmoCounter = 12;
    public const float BULLET_SPEED = 4;

    public const int MinTowerRotation = 15;
    public const int MaxTowerRotation = 45;
    public const float TowerRotationCooldown = 0.5f;
    public const float TowerInitialCooldown = 6f;

    private Projectile _collidedProjectile;

    #region private

    private void Awake()
    {
        _towerManager = TowerManager.Get();

        for (int i = 0; i < MaxAmmoCounter/3; i++)
        {
            var bullet = Instantiate(Projectile, transform.position, Quaternion.identity);
            bullet.transform.SetParent(_towerManager.ProjectilesRoot.transform);
            bullet.SetActive(false);
            _towerManager.InactiveBullets.Add(bullet.GetComponent<Projectile>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CancelInvoke();
        _towerManager.TowerDestroyedUpdateText();
        collision.gameObject.SetActive(false);
        _collidedProjectile = collision.GetComponent<Projectile>();
        _towerManager.ActiveBullets.Remove(_collidedProjectile);
        _towerManager.InactiveBullets.Add(_collidedProjectile);
        gameObject.SetActive(false);
    }

    private void Fire()
    {
        gameObject.SetActive(true);
        ChangeColor(Color.red);
        AmmoCounter--;
        var projectile = GetAndActivateInactiveBullet();
        projectile.gameObject.transform.position = transform.position + transform.up;
        projectile.gameObject.SetActive(true);
        projectile.Rigidbody.velocity = gameObject.transform.up * BULLET_SPEED;
        if(AmmoCounter <= 0)
        {
            CancelInvoke();
            ChangeColor(Color.white);
        }
    }

    private Projectile GetAndActivateInactiveBullet()
    {
        Projectile bullet = _towerManager.InactiveBullets[0];
        _towerManager.InactiveBullets.RemoveAt(0);
        _towerManager.ActiveBullets.Add(bullet);
        return bullet;
    }

    private void Rotate()
    {
        this.transform.Rotate(new Vector3(0,0,Random.Range(MinTowerRotation,MaxTowerRotation)));
        Fire();
    }

    private IEnumerator ProcjectileDeath(Projectile bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if(bullet.gameObject.activeInHierarchy)
        {
            var tower = _towerManager.SpawnTower(bullet.transform.position);
            if(tower)
            {
                tower.InvokeRotatingAndShooting();
            }
            _towerManager.ActiveBullets.Remove(bullet);
            _towerManager.InactiveBullets.Add(bullet);
            bullet.gameObject.SetActive(false);
        }   
    }

    private void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    #endregion
    #region public

    public void InvokeRotatingAndShooting()
    {
        CancelInvoke();
        ChangeColor(Color.white);
        if (_towerManager.TowerCount != 1 && !_towerManager.EndGameLimitReached)
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
