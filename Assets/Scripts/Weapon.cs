
using UnityEngine;


public class Weapon : MonoBehaviour
{
    
    [SerializeField] Projectile projectilePrefab;

    Animator weaponAnimator;

    void Awake() {
        weaponAnimator = GetComponent<Animator>();
    }

    public void Shoot() {
        if(weaponAnimator != null) {
            weaponAnimator.SetTrigger("shoot");
        }
        GameObject parentTransform = transform.parent.gameObject;
        Projectile shot = Instantiate(projectilePrefab, transform.position, transform.rotation);
        shot.SetParentTransform(parentTransform);
    }
    
}
