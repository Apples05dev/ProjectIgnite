using UnityEngine;

public class SoulObject : MonoBehaviour
{
   [SerializeField] internal SoulData chosenSoul;
   [SerializeField] private GameObject soulHolder;


    private void Awake()
    {
        soulHolder = GameObject.FindGameObjectWithTag("SoulHolder");
        gameObject.transform.parent = soulHolder.transform;
    }
    private void Update()
    {
        transform.position = soulHolder.transform.position;
    }

}
