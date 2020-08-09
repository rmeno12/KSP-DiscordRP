using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRichPresence.Activities
{
    class FlyingActivity : ActivityHolder
    {
        private readonly long startTimestamp;
        private readonly CelestialBody body;
        private readonly double altitude;
        private readonly double velocity;

        public FlyingActivity(long startTimestamp, CelestialBody body, double altitude, double velocity)
        {
            this.startTimestamp = startTimestamp;
            this.body = body;
            this.altitude = altitude;
            this.velocity = velocity;
        }

        public override bool Equals(object obj)
        {
            return obj is FlyingActivity activity &&
                   startTimestamp == activity.startTimestamp &&
                   EqualityComparer<CelestialBody>.Default.Equals(body, activity.body) &&
                   altitude == activity.altitude &&
                   velocity == activity.velocity;
        }

        public Activity GetActivity()
        {
            return new Activity
            {
                State = string.Format("Flying over {0}", body.displayName),
                Details = string.Format("Alt: {0:F0}, Vel: {1:F0}", altitude, velocity),
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
