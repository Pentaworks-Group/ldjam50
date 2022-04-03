using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public class GameState : GameFrame.Core.GameState
    {
        public float ElapsedTime { get; set; }
        public List<Rebel> Rebels { get; set; } = new List<Rebel>();
        public List<PoliceTroop> SecurityForces { get; set; } = new List<PoliceTroop>();
    }
}
