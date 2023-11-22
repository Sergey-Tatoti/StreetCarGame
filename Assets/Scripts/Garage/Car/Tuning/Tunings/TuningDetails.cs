using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TuningDetails : MonoBehaviour
{
    [SerializeField] private List<Tuning> _tunings;
    [SerializeField] private Vector3 _cameraPosition;
    [SerializeField] private Vector3 _cameraRotation;
    [SerializeField] private Sprite _spriteTuning;

    private Tuning _currentTuning;
    private Tuning _temporaryTuning;
    private int _currentIndexTuning = 0;
    private int _temporaryIndexTuning;

    public Vector3 CameraPosition => _cameraPosition;
    public Vector3 CameraRotation => _cameraRotation;
    public Sprite SpriteTuning => _spriteTuning;
    public Tuning CurrentTuning => _currentTuning;
    public Tuning TemporaryTuning => _temporaryTuning;
    public int CountTunings => _tunings.Count;
    public int CurrentIndexTuning => _currentIndexTuning;

    public void SetCurrentTuning(List<int> indexes)
    {
        if (_tunings.Count > 0)
        {
            if (indexes != null)
            {
                SetBuyTuning(indexes);
                _currentIndexTuning = indexes[indexes.Count - 1];
            }

            _currentTuning = _tunings[_currentIndexTuning];
            _currentTuning.BuyTuning();

            _currentTuning.gameObject.SetActive(true);
            _temporaryIndexTuning = _currentIndexTuning;
        }
    }

    public void ChangeCurrentTuning()
    {
        _currentTuning = _temporaryTuning;
        _currentIndexTuning = _temporaryIndexTuning;
    }

    public void ShowTuning(int index)
    {

        _tunings[_temporaryIndexTuning].gameObject.SetActive(false);
        _tunings[index].gameObject.SetActive(true);

        _temporaryIndexTuning = index;
        _temporaryTuning = _tunings[_temporaryIndexTuning];
    }

    private void SetBuyTuning(List<int> indexes)
    {
        for (int i = 0; i < indexes.Count; i++)
        {
            if (_tunings[indexes[i]] != null)
                _tunings[indexes[i]].BuyTuning();
        }
    }
}