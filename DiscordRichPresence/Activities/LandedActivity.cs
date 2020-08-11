using Discord;

namespace DiscordRichPresence.Activities
{
    class LandedActivity : ActivityHolder
    {
        private readonly long startTimestamp;
        private readonly CelestialBody body;
        private readonly double latitude;
        private readonly double longitude;

        public LandedActivity(long startTimestamp, CelestialBody body, double latitude, double longitude)
        {
            this.startTimestamp = startTimestamp;
            this.body = body;
            this.longitude = longitude;
            this.latitude = latitude;
        }

        public override bool Equals(object obj)
        {
            return obj is LandedActivity activity &&
                   startTimestamp == activity.startTimestamp &&
                   System.Collections.Generic.EqualityComparer<CelestialBody>.Default.Equals(body, activity.body) &&
                   latitude == activity.latitude &&
                   longitude == activity.longitude;
        }

        public Activity GetActivity()
        {
            return new Activity
            {
                State = string.Format("Landed at {0}", body.displayName),
                Details = string.Format("Lat: {0:F3}, Long: {1:F3}", latitude, longitude),
                Timestamps =
                {
                    Start = startTimestamp
                },
                Assets =
                {
                    LargeImage = string.Format("body_{0}", body.name.ToLower()),
                    LargeText = body.displayName,
                    SmallImage = "default",
                    SmallText = "Kerbal Space Program"
                }
            };
        }
    }
}
