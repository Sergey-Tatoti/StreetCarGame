using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTouchTracker : MonoBehaviour
{
    public event UnityAction<GameObject> AlmostTouchedCar;
    public event UnityAction<bool> TouchedOppositeRoad;
    public event UnityAction<float> TouchedModifier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
            AlmostTouchedCar?.Invoke(other.gameObject);

        if (other.CompareTag("Center"))
            TouchedOppositeRoad?.Invoke(true);

        if (other.TryGetComponent<MoneyModifier>(out MoneyModifier moneyModifier))
        {
            TouchedModifier?.Invoke(moneyModifier.Modifier);
            moneyModifier.Hide();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Center"))
            TouchedOppositeRoad(false);
    }
}
