using System;
using CMCore.Behaviors.Object;
using CMCore.Contracts;
using CMCore.Models;
using UnityEngine;

namespace CMCore.Managers
{
    public class EventManager
    {
        #region Properties & Fields
        
        public Action<Enums.GameState> GameStateChanged;
        

        #endregion

        
        public EventManager()
        {
        }

        
        
        
   
    }
}