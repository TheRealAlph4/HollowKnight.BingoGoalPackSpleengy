using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BingoSync.Interfaces;
using UObject = UnityEngine.Object;
using BingoSync.CustomGoals;
using System.IO;
using System.Reflection;
using System.Linq;
using BingoAdvancedCustomGeneration.AdvancedGameModes;
using BingoSync;
using BingoSync.Settings;
using MonoMod.Utils;
using MonoMod.RuntimeDetour;

namespace BingoGoalPackSpleengy
{
    public class BingoGoalPackSpleengy : Mod
    {
        new public string GetName() => "BingoGoalPackSpleengy";
        public override string GetVersion() => "1.1";

        internal static BingoGoalPackSpleengy Instance;

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            OrderedLoader.OnReadyForGoalsGameModes += LoadSpleengyGoals;
            OrderedLoader.OnStandaloneGoalsGameModesLoaded += SetupGameMode;
            var _hook = new ILHook
                (
                typeof(DreamPlant).GetMethod("CheckOrbs", BindingFlags.NonPublic | BindingFlags.Instance).GetStateMachineTarget(),
                DreamTreesExtension.TrackDreamTrees
                );
        }

        private void LoadSpleengyGoals(object sender, EventArgs e)
        {
            Log("BingoGoalPackSpleengy.LoadSpleengyGoals");
            Assembly assembly = Assembly.GetExecutingAssembly();
            Dictionary<string, BingoGoal> spleenGoals = processEmbeddedJson(assembly, "spleengy");
            Goals.RegisterGoalsForCustom("spleengy", spleenGoals);
            AdvancedGameMode.PreCopyGoalGroup("spleengy");
        }

        private void SetupGameMode(object sender, EventArgs e)
        {
            Log("BingoGoalPackSpleengy.SetupGameMode");
            Goals.AddGameMode(new GameModeSpleengy());
        }

        private Dictionary<string, BingoGoal> processEmbeddedJson(Assembly assembly, string jsonName)
        {
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("Resources." + jsonName + ".json"));
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            return Goals.ProcessGoalsStream(stream);
        }
    }
}