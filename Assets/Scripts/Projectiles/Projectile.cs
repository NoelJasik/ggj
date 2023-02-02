using Unity.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Direction { get; set; }
    private float damage;
    private float speed;
    
    private Health targetHealth;
    private Mirror targetMirror;
    
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    Sprite playerProjectileSprite;
    [SerializeField]
    Sprite enemyProjectileSprite;
    
    // Projectile layer 
    // There is a layer for enemies projectiles and for players projectiles
    // When spawning projectiles you need to pass the correct layer in Initialize()
    // The layers interaction is in the Project settings -> Physics2D 
    
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void Initialize(Vector3 targetPos, float dmg, float projSpeed, int projectileLayer)
    {
        Direction = (targetPos - transform.position).normalized;
        speed = projSpeed;
        damage = dmg;
        gameObject.layer = projectileLayer;
        // Set the correct sprite for the projectile depending on what layer it is
        if(projectileLayer == 7)
        {
            spriteRenderer.sprite = playerProjectileSprite;
        }
        else if(projectileLayer == 6)
        {
            spriteRenderer.sprite = enemyProjectileSprite;
        }
    }

    private void Update()
    {
        transform.position += Direction * (speed * Time.deltaTime);
        transform.LookAt(PlayerMovement.PlayerTransform);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out targetMirror))
        {
            targetMirror.Reflect(this);
            return;
        }
        
        if (col.TryGetComponent(out targetHealth))
        {
            targetHealth.ReceiveDamage(damage);
        }
        
        if(col.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile") || col.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            return;
        }
        DestroyBullet();
    }
    
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}