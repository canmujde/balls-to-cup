using Newtonsoft.Json;
using UnityEngine;

namespace CMCore.Models
{
    public static class Constants //Constant values that can be accessed everywhere.
    {
        public static class Messages
        {
            public const string RepeatLastLevelTooltip =
                "'0' means the game will loop all levels. \nOtherwise the game will loop last 'n' levels.";

            public const string NoPrefabInSceneMessage =
                "There is no prefab to save to level data in the current scene.";

            public const string NoLevelIDInTheListMessage = "Level id is not in the list.";
            public const string StateBecomeNullMessage = "State became null! Setting state to menu state.";
            public const string LevelLoadedMessage = "OnLevelLoaded";
            public const string LevelStartedMessage = "OnLevelStarted";
            public const string LevelEndedMessage = "OnLevelEnded | Result: ";
            public const string StateChangedMessage = "StateChangedEvent => ";
        }

        public static class PlayerPrefsKeys
        {
            public const string VibrationsEnabledPref = "vibrationsEnabled";
            public const string MusicEnabledPref = "musicEnabled";
            public const string SfxEnabledPref = "sfxEnabled";


            public const string CurrentLevelPref = "currentLevel";
            public const string CurrentLevelIDPref = "currentLevelId";
        }

        public static class Yields
        {
            public static WaitForSeconds Point1 = new WaitForSeconds(.1f);
            public static WaitForSeconds Point2 = new WaitForSeconds(.2f);
            public static WaitForSeconds Point3 = new WaitForSeconds(.3f);
            public static WaitForSeconds Point4 = new WaitForSeconds(.4f);
            public static WaitForSeconds Point5 = new WaitForSeconds(.5f);
            public static WaitForSeconds Point6 = new WaitForSeconds(.6f);
            public static WaitForSeconds Point7 = new WaitForSeconds(.7f);
            public static WaitForSeconds Point8 = new WaitForSeconds(.8f);
            public static WaitForSeconds Point9 = new WaitForSeconds(.9f);
            public static WaitForSeconds OneSecond = new WaitForSeconds(1);
            public static WaitForSeconds OneHalfSecond = new WaitForSeconds(1.5f);
            public static WaitForSeconds TwoSecond = new WaitForSeconds(2);
            public static WaitForSeconds TwoHalfSecond = new WaitForSeconds(2.5f);
            public static WaitForSeconds ThreeSecond = new WaitForSeconds(3);
            public static WaitForSeconds ThreeHalfSecond = new WaitForSeconds(3.5f);
        }

        public static class Words
        {
            public const string AmountWord = "Amount";
            public const string LevelWord = "Level";
            public const string PhaseWord = "Phase";
            public const string DayWord = "Day";
            public const string TargetWord = "Target";
            public const string StepWord = "Step";


            public const string AmountSpace = AmountWord + Chars.Space;
            public const string LevelSpace = LevelWord + Chars.Space;
            public const string PhaseSpace = PhaseWord + Chars.Space;
            public const string DaySpace = DayWord + Chars.Space;
            public const string TargetSpace = TargetWord + Chars.Space;
            public const string StepSpace = StepWord + Chars.Space;
        }

        public static class Defaults
        {
            public static readonly JsonSerializerSettings JsonSerializerSettings = new()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            public const int CurrentLevelPrefDefault = 1;
            public const int CurrentLevelIDPrefDefault = 0;
        }

        public static class AnimationKeys
        {
        }

        public static class Chars
        {
            public const string Slash = "/";
            public const string Space = " ";
        }

        public static class FileExtensions
        {
            public const string Json = ".json";
        }

        public static class Tags
        {
            public const string Player = "Player";
        }

        public static class Layers
        {
            public const string Player = "Player";
            public const string Default = "Default";
            public const string IgnoreRaycast = "IgnoreRaycast";
        }
    }
}