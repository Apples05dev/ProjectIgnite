using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject _interactMarker;

    // Changing this to virtual allows sub-classes to override it
    public virtual void Interact()
    {
       
    }

    public void ToggleInteractMarker(bool toggle)
    {
        if (_interactMarker != null)
            _interactMarker.SetActive(toggle);
    }
}
