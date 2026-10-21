using System;
using UnityEngine;

public class Vessel : MonoBehaviour
{
    [SerializeField] private GameObject _interactMarker;
    
    public void Interact()
    {
        Debug.Log("Player interacted with " + gameObject.name);
    }

    public void ToggleInteractMarker(bool toggle)
    {
        _interactMarker.SetActive(toggle);
    }
}
