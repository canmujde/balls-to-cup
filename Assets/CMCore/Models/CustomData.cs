using System;

namespace CMCore.Models
{
   
    [Serializable]
    public class CustomData
    {
        public string name;
        public bool isTaken;

        public CustomData(string name, bool isTaken)
        {
            this.name = name;
            this.isTaken = isTaken;
        }
    }
}