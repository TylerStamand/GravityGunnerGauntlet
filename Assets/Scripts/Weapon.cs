
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
        Debug.Log(parentTransform.gameObject.name);
        Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), parentTransform.GetComponent<Collider2D>()); 
       
    }
    
}
