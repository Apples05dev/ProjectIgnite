using UnityEngine;

public class SoulObject : MonoBehaviour
{
   [SerializeField] internal SoulData chosenSoul;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Vessel"))
        {
           if (chosenSoul.correctVessel == collision.gameObject)
           {
                Debug.Log("Correct Vessel");
           }
           else
           {
                Debug.Log("Incorrect Vessel");
            }
        }
    }
}
