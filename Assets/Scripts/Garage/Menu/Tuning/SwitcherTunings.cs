using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitcherTunings : MonoBehaviour
{
    [SerializeField] private Button _buttonRBump;
    [SerializeField] private Button _buttonFBump;
    [SerializeField] private Button _buttonSpoiler;
    [SerializeField] private Button _buttonSkid;
    [SerializeField] private Button _buttonHood;

    private CarTuning _carTuning;

    public event UnityAction<TuningDetails> ClicledButtonTuning;

    private void OnEnable()
    {
        _buttonRBump.onClick.AddListener(() => ClickedButton(_carTuning.RBump)); ;
        _buttonFBump.onClick.AddListener(() => ClickedButton(_carTuning.FBump)); ;
        _buttonSpoiler.onClick.AddListener(() => ClickedButton(_carTuning.Spoiler)); ;
        _buttonSkid.onClick.AddListener(() => ClickedButton(_carTuning.Skid)); ;
        _buttonHood.onClick.AddListener(() => ClickedButton(_carTuning.Hood)); ;
    }

    private void OnDisable()
    {
        _buttonRBump.onClick.RemoveListener(() => ClickedButton(_carTuning.RBump)); ;
        _buttonFBump.onClick.RemoveListener(() => ClickedButton(_carTuning.FBump)); ;
        _buttonSpoiler.onClick.RemoveListener(() => ClickedButton(_carTuning.Spoiler)); ;
        _buttonSkid.onClick.RemoveListener(() => ClickedButton(_carTuning.Skid)); ;
        _buttonHood.onClick.RemoveListener(() => ClickedButton(_carTuning.Hood)); ;
    }

    public void SetCarTuning(CarTuning carTuning)
    {
        _carTuning = carTuning;
    }

    public void ChangeVisibilityButtons(bool isShow)
    {
        TryActivateButton(_carTuning.RBump, _buttonRBump, isShow);
        TryActivateButton(_carTuning.FBump, _buttonFBump, isShow);
        TryActivateButton(_carTuning.Spoiler, _buttonSpoiler, isShow);
        TryActivateButton(_carTuning.Skid, _buttonSkid, isShow);
        TryActivateButton(_carTuning.Hood, _buttonHood, isShow);
    }

    private void TryActivateButton(TuningDetails tuningDetails, Button button, bool isShow)
    {
        if (tuningDetails.CountTunings > 0)
            button.gameObject.SetActive(isShow);
    }

    private void ClickedButton(TuningDetails tuningDetails)
    {
        ClicledButtonTuning?.Invoke(tuningDetails);
    }
}