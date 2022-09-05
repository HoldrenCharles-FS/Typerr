namespace Typerr.Model
{
    public class User
    {
        public int RecentWpm { get; set; }

        public User(int recentWpm)
        {
            RecentWpm = recentWpm;
        }
    }
}
