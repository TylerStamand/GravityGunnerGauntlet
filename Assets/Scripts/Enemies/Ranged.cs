using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{
    [SerializeField] Weapon weapon;

    

    protected override void Update()
    {
        base.Update();

        weapon.Shoot();
    }
}
