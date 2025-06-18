public class RankingEntry
{
    public string Email { get; }
    public string NickName { get; }
    public int Score { get; set; }

    public RankingEntry(string email, string nickname, int score)
    {
        Email = email;
        NickName = nickname;
        Score = score;
    }

    public bool TryUpdateScore(int newScore)
    {
        if(newScore > Score)
        {
            Score = newScore;
            return true;
        }
        return false;
    }
}
