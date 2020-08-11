using Discord;

namespace DiscordRichPresence.Activities
{
    class BuildingActivity : ActivityHolder
    {
        private readonly long startTimestamp;

        // TODO: add part count + cost
        public BuildingActivity(long startTimestamp)
        {
            this.startTimestamp = startTimestamp;
        }

        public override bool Equals(object obj)
        {
            return obj is BuildingActivity activity &&
                   startTimestamp == activity.startTimestamp;
        }

        public Activity GetActivity()
        {
            return new Activity
            {
                State = "Building a craft",
                Details = "Unknown cost and part count",
                Timestamps =
                {
                    Start = startTimestamp
                },
                Assets =
                {
                    LargeImage = "building_craft",
                    LargeText = "Building a craft",
                    SmallImage = "default",
                    SmallText = "Kerbal Space Program"
                }
            };
        }
    }
}
