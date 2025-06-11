using UnityEngine;
using System;
using System.Collections.Generic;

public class LocalAttendanceRepository
{
    private const string SaveKey = "AttendanceSave";

    public void Save(List<AttendanceNode> nodes, DateTime lastLoginDate, bool todayChecked, int totalCheckedDays)
    {
        var dataList = new List<AttendanceNodeData>();
        foreach (var node in nodes)
        {
            dataList.Add(new AttendanceNodeData
            {
                Day = node.Day,
                IsClaimed = node.IsClaimed,
                GoldRewardAmount = node.GoldAmount,
                DiamondRewardAmount = node.DiamondAmount
            });
        }

        var wrapper = new AttendanceSaveData
        {
            Nodes = dataList,
            LastLoginDate = lastLoginDate.ToString("yyyy-MM-dd"),
            TodayChecked = todayChecked,
            TotalCheckedDays = totalCheckedDays // ✅ 저장
        };

        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public (List<AttendanceNodeData>, DateTime, bool, int) Load()
    {
        string json = PlayerPrefs.GetString(SaveKey, "");
        if (string.IsNullOrEmpty(json))
            return (null, DateTime.MinValue, false, 0);

        var wrapper = JsonUtility.FromJson<AttendanceSaveData>(json);
        var lastLogin = DateTime.TryParse(wrapper.LastLoginDate, out var result) ? result : DateTime.MinValue;
        return (wrapper.Nodes, lastLogin, wrapper.TodayChecked, wrapper.TotalCheckedDays); // ✅ 반환값 확장
    }
}
