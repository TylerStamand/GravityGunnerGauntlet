using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject {
    public bool GravityEnabled;
    public bool BootsEnabled;

    [ContextMenu("ResetPlayerData")]
    public void ResetPlayerData() {
        GravityEnabled = false;
        BootsEnabled = false;
    }
}
