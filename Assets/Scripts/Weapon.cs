
using UnityEngine;


public class Weapon : MonoBehaviour
{
    
    [SerializeField] Projectile projectilePrefab;

    

    public void Shoot() {
            Projectile shot = Instantiate(projectilePrefab, transform.position, transform.rotation);
    
    }
    
}
