using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitcherMenu : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private List<GameObject> _activeMenu;
    [SerializeField] private List<GameObject> _hiddenMenu;

    private void OnEnable()
    {
        _button.onClick.AddListener(SwitchMenu);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SwitchMenu);
    }

    private void SwitchMenu()
    {
        ChangeVisibility(_activeMenu, true);
        ChangeVisibility(_hiddenMenu, false);
    }

    private void ChangeVisibility(List<GameObject> objects, bool isShow)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].SetActive(isShow);
        }
    }
}