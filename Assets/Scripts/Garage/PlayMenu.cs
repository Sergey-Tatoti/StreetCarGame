using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] private Button _buttonPlay;

    private void OnEnable() 
    {
        _buttonPlay.onClick.AddListener(PlayGame);
    }

    private void OnDisable() 
    {
        _buttonPlay.onClick.RemoveListener(PlayGame);
    }

    private void PlayGame()
    {
       SceneManager.LoadScene(1);
    }
}