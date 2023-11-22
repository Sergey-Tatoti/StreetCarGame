using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTuning : MonoBehaviour
{
    [SerializeField] private TuningDetails _rBump;
    [SerializeField] private TuningDetails _fBump;
    [SerializeField] private TuningDetails _spoiler;
    [SerializeField] private TuningDetails _skid;
    [SerializeField] private TuningDetails _hood;

    public TuningDetails RBump => _rBump;
    public TuningDetails FBump => _fBump;
    public TuningDetails Spoiler => _spoiler;
    public TuningDetails Skid => _skid;
    public TuningDetails Hood => _hood;

    public void SetCurrentTunings(List<int> indexesRBump = null, List<int> indexesFBump = null, List<int> indexesSpoiler = null,
                                  List<int> indexesSkid = null, List<int> indexesHood = null)
    {
        _rBump.SetCurrentTuning(indexesRBump);
        _fBump.SetCurrentTuning(indexesFBump);
        _spoiler.SetCurrentTuning(indexesSpoiler);
        _skid.SetCurrentTuning(indexesSkid);
        _hood.SetCurrentTuning(indexesHood);
    }
}