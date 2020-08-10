using Discord;
using DiscordRichPresence.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiscordRichPresence
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class DiscordRichPresence : MonoBehaviour
    {
        private Discord.Discord discord;
        private ActivityManager activityManager;
        private ActivityHolder activity;

        private ActivityTracker activityTracker;

        private float updateInterval = 10.0F; // Time between updates in seconds
        private float lastUpdateTime = 0.0F;

        private bool initialized;

        void Awake()
        {
            Debug.Log("DiscordRichPresence: Plugin Awake");
            lastUpdateTime = Time.time;
            activity = new IdleActivity(Utils.GetUnixTime(), GameScenes.LOADING);
        }

        void Start()
        {
            Debug.Log("DiscordRichPresence: Plugin Startup");

            // TODO: Create own application with own images, etc
            discord = new Discord.Discord(386261941259337738, (ulong)CreateFlags.NoRequireDiscord);
            Debug.Log(discord.ToString());
            activityManager = discord.GetActivityManager();
            activityTracker = new ActivityTracker();

            DontDestroyOnLoad(this);

            // TODO: Add pause detection/status update
        }

        void OnDisable()
        {
            Debug.Log("DiscordRichPresence: Plugin Disable");

            initialized = false;
        }

        void Update()
        {
            //Debug.Log("DiscordRichPresence: Plugin Update");
            activityTracker.UpdateTimers();

            float currentTime = Time.time;

            if (currentTime - lastUpdateTime > updateInterval || !initialized)
            {
                lastUpdateTime = currentTime;

                UpdateRichPresence(activityTracker.GetCurrentActivityHolder());

                initialized = true;
            }
            discord.RunCallbacks();
        }

        void UpdateRichPresence(ActivityHolder activityHolder)
        {
            ActivityHolder prev = activity;
            activity = activityHolder;

            if (!activity.Equals(prev) || !initialized)
            {
                Debug.Log("DiscordRichPresence: About to update presence");
                activityManager.UpdateActivity(activity.GetActivity(), (result) => 
                {
                    Debug.Log(string.Format("DiscordRichPresence: Result is ({0})", result));
                });
            }
        }
    }
}
