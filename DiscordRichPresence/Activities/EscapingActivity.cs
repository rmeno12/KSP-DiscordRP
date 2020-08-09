using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRichPresence.Activities
{
    class EscapingActivity : ActivityHolder
    {
        private readonly long startTimestamp;
        private readonly CelestialBody body;

        public EscapingActivity(long startTimestamp, CelestialBody body)
        {
            this.startTimestamp = startTimestamp;
            this.body = body;
        }

        public override bool Equals(object obj)
        {
            return obj is EscapingActivity activity &&
                   startTimestamp == activity.startTimestamp &&
                   EqualityComparer<CelestialBody>.Default.Equals(body, activity.body);
        }


        public Activity GetActivity()
        {
            return new Activity
            {
                State = string.Format("Escaping {0}", body.displayName),
                Details = "At escape velocity",
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
