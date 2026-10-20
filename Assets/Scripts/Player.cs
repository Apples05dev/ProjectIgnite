using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f; // The player's maximum speed.
    [SerializeField] private float _accelRate = 7f; // How fast the player accellerates/deccelerates.

    private Rigidbody _rb;

    private readonly List<Vessel> _vesselsInRange = new();
    private int _selectionIndex = -1; // which of the vessels in range to interact with when pressing the interact button while in range of a vessel.

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _selectionIndex = GetClosestVessels();
        UpdateVesselsInRange();
    }

    public void Move(Vector2 input)
    {
        Vector3 forceX = CalculateForce(input.x, _rb.linearVelocity.x) * Vector3.right;
        Vector3 forceZ = CalculateForce(input.y, _rb.linearVelocity.z) * Vector3.forward;

        _rb.AddForce(forceX + forceZ);
    }

    public void Interact()
    {
        if (_selectionIndex < 0)
            return;

        _vesselsInRange[_selectionIndex].Interact();
    }

    private float CalculateForce(float input, float currentVelocity)
    {
        // Calculate speed relative to input (axis from -1 to 1)
        float targetSpeed = input * _speed;

        // calculate the difference between the target speed and current velocity and multiply it by the acceleration rate.
        float speedDiff = targetSpeed - currentVelocity;
        return speedDiff * _accelRate;
    }

    private int GetClosestVessels()
    {
        // if there are no vessels to interact with, do nothing.
        if (_vesselsInRange.Count <= 0)
            return -1;

        // If there is only one vessel to interact with, then don't bother with the distance calculations.
        if (_vesselsInRange.Count == 1)
            return 0;
        else
        {
            int closestVessel = 0;
            float closestDistance = 999;
            
            // Loop through every vessel in range and find the closest one.
            for (int i = 0; i < _vesselsInRange.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, _vesselsInRange[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestVessel = i;
                }
            }
            return closestVessel;
        }
    }
    
    private void UpdateVesselsInRange()
    {
        // If there is no vessel, do nothing.
        if (_selectionIndex < 0)
            return;

        // Loop through all vessels in range and set its interaction markers according to if its index is the same as the selection index.
        for (int i = 0; i < _vesselsInRange.Count; i++)
        {
            _vesselsInRange[i].ToggleInteractMarker(i == _selectionIndex);
        }
    }

    //
    // Collission checks
    //
    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger object has a Vessel script, then add it to the list if it's not already there.
        if (other.gameObject.TryGetComponent(out Vessel vessel))
        {
            if (!_vesselsInRange.Contains(vessel))
                _vesselsInRange.Add(vessel);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the trigger object has a Vessel script and if the vessel exists in the vesselsInRange List.
        if (other.gameObject.TryGetComponent(out Vessel vessel) && _vesselsInRange.Contains(vessel))
        {
            // If this is the last vessel in the list, make sure to turn off the interact marker.
            if (_vesselsInRange.Count == 1)
                vessel.ToggleInteractMarker(false);

            _vesselsInRange.Remove(vessel);
        }
    }
}
