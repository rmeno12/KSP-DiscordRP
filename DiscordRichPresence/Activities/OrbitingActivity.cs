using Discord;
using System.Collections.Generic;

namespace DiscordRichPresence.Activities
{
    class OrbitingActivity : ActivityHolder
    {
        private readonly long startTimestamp;
        private readonly CelestialBody body;
        private readonly double semimajorAxis;
        private readonly double eccentricity;

        public OrbitingActivity(long startTimestamp, CelestialBody body, double semimajorAxis, double eccentricity)
        {
            this.startTimestamp = startTimestamp;
            this.body = body;
            this.semimajorAxis = semimajorAxis;
            this.eccentricity = eccentricity;
        }

        public override bool Equals(object obj)
        {
            return obj is OrbitingActivity activity &&
                   startTimestamp == activity.startTimestamp &&
                   EqualityComparer<CelestialBody>.Default.Equals(body, activity.body) &&
                   semimajorAxis == activity.semimajorAxis &&
                   eccentricity == activity.eccentricity;
        }

        public Activity GetActivity()
        {
            return new Activity
            {
                State = string.Format("Orbiting around {0}", body.displayName),
                Details = string.Format("SMA: {0}, Ecc: {1:F2}", Utils.FormatDistance(semimajorAxis), eccentricity),
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
