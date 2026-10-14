using UnityEngine;

[CreateAssetMenu(fileName = "SoulData", menuName = "Scriptable Objects/SoulData")]
public class SoulData : ScriptableObject
{
    [SerializeField] internal GameObject correctVessel;
    [SerializeField] internal string[] hints;

    
}
