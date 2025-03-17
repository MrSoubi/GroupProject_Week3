using UnityEngine;

public class RSE_PowerUpPickable : BT.ScriptablesObject.RuntimeScriptableEvent<float>{}

[CreateAssetMenu(fileName = "RSE_OnGhostPowerPicked", menuName = "RSE/PowerUp/RSE_OnGhostPowerPicked")]
public class RSE_OnGhostPowerPicked : RSE_PowerUpPickable {}