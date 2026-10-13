using UnityEngine;

[CreateAssetMenu(fileName = "SoulData", menuName = "Scriptable Objects/SoulData")]
public class SoulData : ScriptableObject
{
    [SerializeField] private GameObject correctVessel;
    [SerializeField] private string startingHint;
    [SerializeField] private string secondHint;
    [SerializeField] private string thirdHint;
}
