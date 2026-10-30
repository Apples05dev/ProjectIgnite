using UnityEngine;
using UnityEngine.UIElements;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        eyeFollow();
    }

    void eyeFollow()
    {
        if (player == null) return;

       
        Vector3 playerPos = player.transform.position;

   
        Vector3 direction = playerPos - transform.position;

 
        direction.z = 0;

        
        if (direction != Vector3.zero)
        {
            transform.up = direction.normalized;
        }
    }
}

