using UnityEngine;

public struct StageData
{
    public float respawnTime;
    public int maxEnemyCount;
}

public class StageDataSetter 
{
    private StageData _stageData;
    public StageData GetStageData(int level) // 스테이지 정보 반환
    {
        _stageData = new StageData();
        _stageData.respawnTime = Mathf.Max( 10 - level * 0.05f, 0.5f);
        _stageData.maxEnemyCount = 10 + level; 

        return _stageData;
    }
    public bool IsNextLevel(float time, int level) //60초마다 레벨업
    {
        if ((int)time/60>level)
        {
            return true;
        }
        return false;
    }
}
