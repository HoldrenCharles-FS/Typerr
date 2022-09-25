using System;
using System.Collections.Generic;

namespace Typerr.Model
{
    public struct Subscription
    {
        public string url;
        public string name;
    }
    public class User
    {
        public int RecentWpm { get; set; }

        public int Mode { get; set; }

        public int Minutes { get; set; }

        public List<DateTime> RequestTimes { get; set; }

        public List<Subscription> Subscriptions { get; set; }

        public User(int recentWpm, int mode, int minutes, List<DateTime> requestTimes, List<Subscription> subscriptions)
        {
            RecentWpm = recentWpm;
            Mode = mode;
            Minutes = minutes;
            RequestTimes = requestTimes;
            Subscriptions = subscriptions;
        }
    }
}
