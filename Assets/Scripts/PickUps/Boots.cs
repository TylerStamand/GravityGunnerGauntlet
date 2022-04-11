using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : PickUp
{
    protected override void PickUpAction(PlayerUnit playerUnit) {
        playerUnit.EnableBoots();
    }
    
}
