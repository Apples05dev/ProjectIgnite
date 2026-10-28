using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject _interactMarker;

    // Changing this to virtual allows sub-classes to override it
    public virtual void Interact()
    {
       
    }

    private void Start()
    {
       
    }

    public void ToggleInteractMarker(bool toggle)
    {
        if (_interactMarker != null) 
        {
            _interactMarker.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _interactMarker.transform.localPosition = new Vector3(0, 3f, 0);
            _interactMarker.SetActive(toggle);
        }
             
    }
}
