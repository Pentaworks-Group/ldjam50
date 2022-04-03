
using System;

using UnityEngine;

namespace Assets.Scripts.Scenes.City
{
    public class CityBehaviour : MonoBehaviour
    {
        void Update()
        {
            if (Assets.Scripts.Base.Core.Game.State != default)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    var isForward = true;

                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        isForward = false;
                    }

                    LoopSecurityForce(isForward);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha0 | KeyCode.Keypad0))
                {
                    SelectSecurityForce(0);
                }
            }
        }

        private void LoopSecurityForce(Boolean isForward)
        {
            var index = GameHandler.SecurityForces.IndexOf(GameHandler.SelectedTroop);

            if (isForward)
            {
                index++;

                if (index > GameHandler.SecurityForces.Count - 1)
                {
                    index = 0;
                }
            }
            else
            {
                index--;

                if (index < 0)
                {
                    index = GameHandler.SecurityForces.Count - 1;
                }
            }

            SelectSecurityForce(index);
        }

        private void SelectSecurityForce(int index)
        {
            if (index >= 0 && GameHandler.SecurityForces.Count > index)
            {
                GameHandler.SelectTroop(GameHandler.SecurityForces[index]);
            }
        }
    }
}
