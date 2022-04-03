﻿
using System;
using System.Linq;

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

                if (IsAnyDown(KeyCode.Alpha0, KeyCode.Keypad0))
                {
                    SelectSecurityForce(9);
                }

                if (IsAnyDown(KeyCode.Alpha1, KeyCode.Keypad1))
                {
                    SelectSecurityForce(0);
                }

                if (IsAnyDown(KeyCode.Alpha2, KeyCode.Keypad2))
                {
                    SelectSecurityForce(1);
                }

                if (IsAnyDown(KeyCode.Alpha3, KeyCode.Keypad3))
                {
                    SelectSecurityForce(2);
                }

                if (IsAnyDown(KeyCode.Alpha4, KeyCode.Keypad4))
                {
                    SelectSecurityForce(3);
                }

                if (IsAnyDown(KeyCode.Alpha5, KeyCode.Keypad5))
                {
                    SelectSecurityForce(4);
                }

                if (IsAnyDown(KeyCode.Alpha6, KeyCode.Keypad6))
                {
                    SelectSecurityForce(5);
                }

                if (IsAnyDown(KeyCode.Alpha7, KeyCode.Keypad7))
                {
                    SelectSecurityForce(6);
                }

                if (IsAnyDown(KeyCode.Alpha8, KeyCode.Keypad8))
                {
                    SelectSecurityForce(7);
                }

                if (IsAnyDown(KeyCode.Alpha9, KeyCode.Keypad9))
                {
                    SelectSecurityForce(8);
                }
            }
        }

        private Boolean IsAnyDown(params KeyCode[] keycodes)
        {
            if (keycodes?.Length > 0)
            {
                return keycodes.Any(k => Input.GetKeyDown(k));
            }

            return false;
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
                Debug.Log($"Selecting Securityforce at index {index}");
                GameHandler.SelectTroop(GameHandler.SecurityForces[index]);
            }
        }
    }
}
