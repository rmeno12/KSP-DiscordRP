using System;

namespace DiscordRichPresence
{
    class ActivityTimer
    {
        private readonly Predicate<GameScenes> precondictions;
        private readonly Predicate<GameScenes> check;

        public bool ActiveNow { get; private set; }
        public long Timestamp { get; private set; }

        public ActivityTimer(Predicate<GameScenes> precondictions, Predicate<GameScenes> check)
        {
            this.precondictions = precondictions;
            this.check = check;
            Timestamp = Utils.GetUnixTime();
        }

        public void Update()
        {
            var currentScene = HighLogic.LoadedScene;
            bool active = precondictions.Invoke(currentScene) && check.Invoke(currentScene);

            // Check if activity has changed status
            if (active != ActiveNow)
            {
                ActiveNow = active;

                if (active)
                {
                    Timestamp = Utils.GetUnixTime();
                }
            }
        }
    }
}
