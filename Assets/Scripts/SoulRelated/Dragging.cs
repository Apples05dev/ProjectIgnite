using UnityEngine;

public class Dragging : MonoBehaviour
{
    // THIS IS A TEMPORARY SCRIPT, IT IS NOT USED IN THE FINAL GAME. IT ONLY APLLIES THE EFFECT OF CHECKING WEATHER THE SOUL IS CORRECT OR NOT



    private Vector3 offset;
    private Vector3 screenPoint;

    private CircleCollider2D circleCollider;


    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        circleCollider.enabled = false;
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }
    
    void OnMouseUp()
    {
        circleCollider.enabled = true;
    }
}
