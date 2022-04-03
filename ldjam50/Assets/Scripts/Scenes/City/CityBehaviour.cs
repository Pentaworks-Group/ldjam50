
using System;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Scenes.City
{
    public class CityBehaviour : MonoBehaviour
    {
        private MapObjectSpawner mapObjectSpawner;
        private GameObject shopOverlay;
        private SecurityForceSlotBehaviour policeSlot;

        public void ShowShop()
        {
            Time.timeScale = 0;

            Debug.Log("Play Sound: ");
            Base.Core.Game.BackgroundAudioManager.Stop();
            Base.Core.Game.BackgroundAudioManager.Clips = Base.Core.Game.ShopClipList;
            Base.Core.Game.BackgroundAudioManager.Resume();
            Base.Core.Game.AmbienceAudioManager.Stop();

            shopOverlay.SetActive(true);
        }

        public void CloseShop()
        {
            Base.Core.Game.BackgroundAudioManager.Stop();
            Base.Core.Game.BackgroundAudioManager.Clips = Base.Core.Game.AudioClipListGame2;
            Base.Core.Game.BackgroundAudioManager.Resume();

            if (Base.Core.Game.State.Rebels.Count > 0)
            {
                Base.Core.Game.AmbienceAudioManager.Resume();
            }

            shopOverlay.SetActive(false);
            Time.timeScale = 1;
        }

        public void PauseToggled(Boolean isPaused)
        {
            if (isPaused)
            {
                Time.timeScale = 0;
                Assets.Scripts.Base.Core.Game.EffectsAudioManager?.Pause();
                Base.Core.Game.AmbienceAudioManager.Stop();
            }
            else
            {
                if (!shopOverlay.activeSelf)
                {
                    Assets.Scripts.Base.Core.Game.EffectsAudioManager?.Resume();

                    if (Base.Core.Game.State.Rebels.Count > 0)
                    {
                        Base.Core.Game.AmbienceAudioManager.Resume();
                    }
                    Time.timeScale = 1;
                }
            }
        }

        public void BuySecurityForce(SecurityForceSlotBehaviour selectedForce)
        {
            if (selectedForce?.SecurityForceDefault != default)
            {
                mapObjectSpawner.SpawnTroopFromDefault(selectedForce.SecurityForceDefault);
            }
        }

        private void Start()
        {
            this.mapObjectSpawner = this.transform.Find("Rotatotor/MapObjectSpawner").gameObject.GetComponent<MapObjectSpawner>();
            this.shopOverlay = this.transform.Find("Rotatotor/HUD/ShopOverlay").gameObject;
            this.policeSlot = shopOverlay.transform.Find("ContentArea/PoliceSecurityForceSlot").GetComponent<SecurityForceSlotBehaviour>();

            if (GameHandler.GameFieldSettings != default)
            {
                this.policeSlot.SecurityForceDefault = GameHandler.GameFieldSettings.TroopDefaults.FirstOrDefault(d => d.Type == "Police");
            }
        }

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
