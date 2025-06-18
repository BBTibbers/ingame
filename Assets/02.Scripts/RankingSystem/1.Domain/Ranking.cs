using UnityEngine;

public class Ranking 
{
    public string Email { get; set; }
    public string Nickname { get; set; }
    public int Score { get; set; }

    public Ranking(string email, string nickname, int score)
    {
        Email = email;
        Nickname = nickname;
        Score = score;
    }
}
