using System.Collections.Generic;
using System.Linq;
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
        List<Collider2D> colliders = parentTransform.GetComponents<Collider2D>().ToList();
        foreach (Collider2D collider in colliders) {
            Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), collider);
        }
       
       
    }
    
}
