using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    private const float MIN_PROCJECTILE_LIFE_TIME = 0.25f;
    private const float MAX_PROCJECTILE_LIFE_TIME = 1f;

    public Rigidbody2D Rigidbody
    {
        get { return _rigidBody; }
    }

    private void OnEnable()
    {
        Invoke(nameof(ProcjectileDeath), Random.Range(MIN_PROCJECTILE_LIFE_TIME, MAX_PROCJECTILE_LIFE_TIME));
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
            TowerManager.Get().ActiveBullets.Remove(this);
            TowerManager.Get().InactiveBullets.Add(this);
            gameObject.SetActive(false);
        }

    }
}
