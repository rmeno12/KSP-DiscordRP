using Discord;

namespace DiscordRichPresence.Activities
{
    class MissionBuildingActivity : ActivityHolder
    {
        private readonly long startTimestamp;

        // TODO: add other mission building info
        public MissionBuildingActivity(long startTimestamp)
        {
            this.startTimestamp = startTimestamp;
        }

        public override bool Equals(object obj)
        {
            return obj is MissionBuildingActivity activity &&
                   startTimestamp == activity.startTimestamp;
        }

        public Activity GetActivity()
        {
            return new Activity
            {
                State = "Creating a mission",
                Details = "Unknown mission details",
                Timestamps =
                {
                    Start = startTimestamp
                },
                Assets =
                {
                    LargeImage = "default",
                    LargeText = "Creating a mission",
                    SmallImage = "default",
                    SmallText = "Kerbal Space Program"
                }
            };
        }
    }
}
