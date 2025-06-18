[System.Serializable]
class RankingEntryDTO
{
    public string Email;
    public string NickName;
    public int Score;

    public RankingEntryDTO(RankingEntry entry)
    {
        Email = entry.Email;
        NickName = entry.NickName;
        Score = entry.Score;
    }

    public RankingEntry ToEntity()
    {
        return new RankingEntry(Email, NickName, Score);
    }
}