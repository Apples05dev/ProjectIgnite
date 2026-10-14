using UnityEngine;

public class SoulObject : MonoBehaviour
{
   [SerializeField] internal SoulData chosenSoul;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Vessel"))
        {
            Debug.Log(chosenSoul.correctVessel);
            Debug.Log(collision.gameObject);
            if (chosenSoul.correctVessel.name == collision.gameObject.name)
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
