using System;

namespace CMCore.Models
{
    [Serializable]
    public class PlayerData
    {
        public string name;
        public int exp;
        public int level;
        public bool playedInitialTutorial;
        public bool playedDynamicTutorial;

        public PlayerData(string name, int exp, int level, bool playedInitialTutorial, bool playedDynamicTutorial)
        {
            this.name = name;
            this.exp = exp;
            this.level = level;
            this.playedInitialTutorial = playedInitialTutorial;
            this.playedDynamicTutorial = playedDynamicTutorial;
        }
    }
}