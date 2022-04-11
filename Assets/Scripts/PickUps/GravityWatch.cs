using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWatch : PickUp
{
    protected override void PickUpAction(PlayerUnit playerUnit) {
        playerUnit.EnableGravity();
    }
}
