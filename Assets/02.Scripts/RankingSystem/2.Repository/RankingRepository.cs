using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RankingRepository
{
    private const string SaveKey = "RankingData";
    private List<RankingEntry> _rankingEntries = new List<RankingEntry>();

    [System.Serializable]
    private class Wrapper
    {
        public List<RankingEntryDTO> Entries = new List<RankingEntryDTO>();
    }

    public void Save()
    {
        var wrapper = new Wrapper
        {
            Entries = _rankingEntries.Select(e => new RankingEntryDTO(e)).ToList()
        };

        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey)) return;

        string json = PlayerPrefs.GetString(SaveKey);
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        _rankingEntries = wrapper?.Entries?.Select(dto => dto.ToEntity()).ToList() ?? new List<RankingEntry>();
    }

    // Optional: get access to internal entries
    public List<RankingEntry> GetAllEntries()
    {
        return _rankingEntries.OrderByDescending(e => e.Score).ToList();
    }

}
