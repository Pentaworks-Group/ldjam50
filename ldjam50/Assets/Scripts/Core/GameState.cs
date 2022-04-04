using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public class GameState : GameFrame.Core.GameState
    {
        public Boolean WasShopShown { get; set; }
        public GameFieldSettings Mode { get; set; }

        public CoreMapBase Palace { get; set; }
        public CoreMapBase MilitaryBase { get; set; }
        public float ElapsedTime { get; set; }
        public float NextRebelSpawn { get; set; }
        public float NextMoneySpawn { get; set; }
        public List<Rebel> Rebels { get; set; } = new List<Rebel>();
        public List<SecurityForce> SecurityForces { get; set; } = new List<SecurityForce>();
        public Decimal AvailableCredits { get; set; }
    }
}
