using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempPlayer : MonoBehaviour
{
    [SerializeField] private List<RCC_CarControllerV3> _cars;
    [SerializeField] private Button _button;
    
    private int index = 0;

    private void OnEnable()
    {
        _button.onClick.AddListener(NextCar);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(NextCar);
    }

    private void Start()
    {
        _cars[index].gameObject.SetActive(true);
    }

    private void NextCar()
    {
        _cars[index].gameObject.SetActive(false);

        index++;

        if (index == _cars.Count)
            index = 0;

        _cars[index].gameObject.SetActive(true);
    }
}
