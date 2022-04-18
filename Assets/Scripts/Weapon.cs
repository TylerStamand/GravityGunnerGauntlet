
using UnityEngine;


public class Weapon : MonoBehaviour
{
    
    [SerializeField] Projectile projectilePrefab;

    

    public void Shoot() {
        GameObject parentTransform = transform.parent.gameObject;
        Projectile shot = Instantiate(projectilePrefab, transform.position, transform.rotation);
        shot.SetParentTransform(parentTransform);
    }
    
}
