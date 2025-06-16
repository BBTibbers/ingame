using System;
using UnityEngine;

public class StageLevelManager : MonoBehaviour
{
    public int Level=1;
    public Action OnLevelChanged;
    public StageDataSetter StageDataSetter;
    void Start()
    {
        StageDataSetter = new StageDataSetter();
        OnLevelChanged?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        NextLevel();
    }
    private void NextLevel()
    {
        if(StageDataSetter.IsNextLevel(Time.time,Level))
        {
            Level++;
            OnLevelChanged?.Invoke();
        }
    }
}
