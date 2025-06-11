using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    [Header("출석 보상 템플릿")]
    public AttendanceBoardSO attendanceBoardSO;

    private List<AttendanceNode> _nodes;
    private AttendanceBoardData _boardData;

    private const string SaveKey = "AttendanceSave";
    private LocalAttendanceRepository _repository = new LocalAttendanceRepository();


    private void Awake()
    {
        LoadFromSaveData();

        // 첫 로그인일 경우 오늘 출석 체크 false 처리
        if (DateTime.Now.Date > DateTime.Parse(_boardData.LastLoginDate).Date)
        {
            _boardData.TodayChecked = false;
            _boardData.TotalLoginDays++;
        }

        _boardData.LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd");

        Save(); // 갱신한 내용 저장
    }

    private void LoadFromSaveData()
    {
        var (nodeDataList, lastLoginDate, todayChecked, totalCheckedDays) = _repository.Load();

        _boardData = new AttendanceBoardData
        {
            TotalLoginDays = totalCheckedDays,
            LastLoginDate = lastLoginDate.ToString("yyyy-MM-dd"),
            TodayChecked = todayChecked,
            ResetHour = 0
        };

        _nodes = new List<AttendanceNode>();
        var sorted = attendanceBoardSO.Rewards.OrderBy(r => r.Day).ToList();

        for (int i = 0; i < sorted.Count; i++)
        {
            var reward = sorted[i];
            var match = nodeDataList?.FirstOrDefault(n => n.Day == reward.Day);

            var nodeData = new AttendanceNodeData
            {
                Day = reward.Day,
                GoldRewardAmount = reward.GoldRewardAmount,
                DiamondRewardAmount = reward.DiamondRewardAmount,
                IsClaimed = match?.IsClaimed ?? false
            };

            _nodes.Add(new AttendanceNode(nodeData));
        }

        Debug.Log($"출석 노드 {_nodes.Count}개 초기화 완료");
    }


    /// <summary>
    /// UI 버튼에서 호출: 현재 날짜까지의 출석 보상을 일괄 수령합니다.
    /// </summary>
    public void OnClickClaimAll()
    {
        DateTime now = DateTime.Now;

        foreach (var node in _nodes)
        {
            if (node.TryClaimReward(now))
            {
                Debug.Log($"[출석 수령 완료] Day {node.Day}");
            }
        }
        Save();

    }

    private void Save()
    {
        _repository.Save(_nodes, DateTime.Parse(_boardData.LastLoginDate), _boardData.TodayChecked, _boardData.TotalLoginDays);
    }
    public bool IsTodayChecked()
    {
        return _boardData.TodayChecked;
    }

}
