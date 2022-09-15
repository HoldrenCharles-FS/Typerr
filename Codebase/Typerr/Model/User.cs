namespace Typerr.Model
{
    public class User
    {
        public int RecentWpm { get; set; }

        public int Mode { get; set; }

        public int Minutes { get; set; }

        public User(int recentWpm, int mode, int minutes)
        {
            RecentWpm = recentWpm;
            Mode = mode;
            Minutes = minutes;
        }
    }
}
