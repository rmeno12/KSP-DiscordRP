using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRichPresence.Activities
{
    class IdleActivity : ActivityHolder
    {
        private readonly long startTimestamp;
        private readonly GameScenes scene;

        public IdleActivity(long startTimestamp, GameScenes scene)
        {
            this.startTimestamp = startTimestamp;
            this.scene = scene;
        }

        public override bool Equals(object obj)
        {
            return obj is IdleActivity activity &&
                   startTimestamp == activity.startTimestamp &&
                   scene == activity.scene;
        }

        public Activity GetActivity()
        {
            string desc = GetDesc();

            return new Activity
            {
                State = "Idle",
                Details = desc,
                Timestamps =
                {
                    Start = startTimestamp
                },
                Assets =
                {
                    LargeImage = "default",
                    LargeText = "Idling",
                    SmallImage = GetSmallImage(),
                    SmallText = desc
                }
            };
        }

        private string GetDesc()
        {
            switch (scene)
            {
                case GameScenes.LOADING:
                    return "Loading";
                case GameScenes.LOADINGBUFFER:
                    return "Loading Game";
                case GameScenes.MAINMENU:
                    return "In the Main Menu";
                case GameScenes.SETTINGS:
                    return "Configuring their game";
                case GameScenes.SPACECENTER:
                    return "At the KSC";
                case GameScenes.TRACKSTATION:
                    return "In the Tracking Station";
                case GameScenes.CREDITS:
                    return "Watching the Credits";
                case GameScenes.MISSIONBUILDER:
                    return "Creating a mission";
            }

            return scene.ToString();
        }

        private string GetSmallImage()
        {
            switch (scene)
            {
                case GameScenes.LOADING:
                case GameScenes.LOADINGBUFFER:
                    return "loading";
                case GameScenes.SETTINGS:
                    return "settings";
                case GameScenes.SPACECENTER:
                    return "space_center";
                case GameScenes.TRACKSTATION:
                    return "tracking_station";
                case GameScenes.CREDITS:
                    return "heart";
            }

            return "";
        }
    }
}
