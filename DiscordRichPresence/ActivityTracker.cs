using Discord;
using DiscordRichPresence.Activities;
using Expansions.Missions.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRichPresence
{
    class ActivityTracker
    {
        private readonly ActivityTimer flightTimer;
        private readonly ActivityTimer landedTimer;
        private readonly ActivityTimer buildingTimer;
        private readonly ActivityTimer missionBuilderTimer;
        private readonly ActivityTimer idleTimer;

        public bool Paused { private get; set; }

        public ActivityTracker()
        {
            flightTimer = new ActivityTimer(scene => HighLogic.LoadedSceneIsFlight,
                scene => !(FlightGlobals.ActiveVessel.situation == Vessel.Situations.LANDED
                           || FlightGlobals.ActiveVessel.situation == Vessel.Situations.SPLASHED
                           || FlightGlobals.ActiveVessel.situation == Vessel.Situations.PRELAUNCH));
            landedTimer = new ActivityTimer(scene => HighLogic.LoadedSceneIsFlight,
                scene => FlightGlobals.ActiveVessel.situation == Vessel.Situations.LANDED
                         || FlightGlobals.ActiveVessel.situation == Vessel.Situations.SPLASHED
                         || FlightGlobals.ActiveVessel.situation == Vessel.Situations.PRELAUNCH);
            buildingTimer = new ActivityTimer(scene => true, scene => scene == GameScenes.EDITOR);
            missionBuilderTimer = new ActivityTimer(scene => true, scene => scene == GameScenes.MISSIONBUILDER);
            idleTimer = new ActivityTimer(scene => true,
                scene => scene != GameScenes.EDITOR && scene != GameScenes.FLIGHT && scene != GameScenes.MISSIONBUILDER);

            UpdateTimers();
        }

        public void UpdateTimers()
        {
            flightTimer.Update();
            landedTimer.Update();
            buildingTimer.Update();
            missionBuilderTimer.Update();
            idleTimer.Update();
        }

        public ActivityHolder GetCurrentActivityHolder()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                return GetFlightActivityHolder(FlightGlobals.ActiveVessel);
            }
            else if (HighLogic.LoadedSceneIsEditor)
            {
                return new BuildingActivity(buildingTimer.Timestamp);
            }
            else if (HighLogic.LoadedSceneIsMissionBuilder)
            {
                return new MissionBuildingActivity(missionBuilderTimer.Timestamp);
            }
            else
            {
                return new IdleActivity(idleTimer.Timestamp, HighLogic.LoadedScene);
            }
        }

        private ActivityHolder GetFlightActivityHolder(Vessel vessel)
        {
            double apoapsis = vessel.orbit.ApR;
            double periapsis = vessel.orbit.PeR;
            CelestialBody body = vessel.mainBody;

            if (vessel.Landed)
            {
                return new LandedActivity(landedTimer.Timestamp, body, vessel.latitude, vessel.longitude);
            }
            else if (vessel.Splashed)
            {
                return new SplashedActivity(landedTimer.Timestamp, body, vessel.latitude, vessel.longitude);
            }
            else if (apoapsis > body.sphereOfInfluence || (apoapsis < 0.0 && periapsis > apoapsis)) // on an escape trajectory
            {
                return new EscapingActivity(flightTimer.Timestamp, body);
            }
            else if (vessel.altitude < body.atmosphereDepth) // within atmosphere
            {
                return new FlyingActivity(flightTimer.Timestamp, body, vessel.altitude, vessel.srfSpeed);
            }
            else
            {
                return new OrbitingActivity(flightTimer.Timestamp, body, vessel.orbit.semiMajorAxis, vessel.orbit.eccentricity);
            }
        }
    }
}
