using UnityEngine;

public class Heart : PickUp
{
    protected override void PickUpAction(PlayerUnit playerUnit)
    {
        playerUnit.AddHealth(1);
    }
}
