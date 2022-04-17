
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.City
{
    public class CityBehaviour : MonoBehaviour
    {
        private Dictionary<Int32, SecurityForceBehaviour> boundSecurityForces = new Dictionary<Int32, SecurityForceBehaviour>();

        private MapObjectSpawner mapObjectSpawner;
        private GameObject shopOverlay;
        private GameObject moneyDisplay;
        private Text moneyText;
        private SecurityForceSlotBehaviour templateSlot;

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
                if (selectedForce.IsPurchasable)
                {
                    Base.Core.Game.State.AvailableCredits -= selectedForce.SecurityForceDefault.UnitCost;
                    Base.Core.Game.EffectsAudioManager.Play("Buy");
                    mapObjectSpawner.SpawnTroopFromDefault(selectedForce.SecurityForceDefault);
                }
                else
                {
                    Base.Core.Game.EffectsAudioManager.Play("Error");
                }
            }
        }

        public void PlayError()
        {
            Base.Core.Game.EffectsAudioManager.Play("Error");
        }

        private void Start()
        {
            if (Base.Core.Game.State?.Mode == null)
            {
                return;
            }

            var gameState = Base.Core.Game.State;

            this.mapObjectSpawner = this.transform.Find("Rotatotor/MapObjectSpawner").gameObject.GetComponent<MapObjectSpawner>();
            this.moneyDisplay = this.transform.Find("Rotatotor/HUD/Top/MoneyDisplay").gameObject;

            if (gameState.Mode.DisableShop)
            {
                moneyDisplay.SetActive(false);
            }
            else
            {
                this.moneyText = moneyDisplay.transform.Find("MoneyText").GetComponent<Text>();
            }

            this.shopOverlay = this.transform.Find("Rotatotor/HUD/ShopOverlay").gameObject;

            this.templateSlot = shopOverlay.transform.Find("ContentArea/SecurityForceSlotTemplate").GetComponent<SecurityForceSlotBehaviour>();

            var amount = gameState.Mode.TroopDefaults.Count;

            float relative = 1f / amount;

            for (int i = 0; i < gameState.Mode.TroopDefaults.Count; i++)
            {
                var forceSlot = GameObject.Instantiate(templateSlot, templateSlot.transform.parent);

                RectTransform rect = forceSlot.GetComponent<RectTransform>();

                rect.anchorMin = new Vector2((float)i * relative, rect.anchorMin.y);
                rect.anchorMax = new Vector2((float)(i + 1) * relative, rect.anchorMax.y);
                rect.offsetMin = new Vector2(0, 0);
                rect.offsetMax = new Vector2(0, 0);

                forceSlot.SecurityForceDefault = gameState.Mode.TroopDefaults[i];

                forceSlot.gameObject.SetActive(true);
            }

            if (gameState.Mode.MoneyStart > 0 && (!gameState.WasShopShown))
            {
                gameState.WasShopShown = true;

                ShowShop();
            }
        }

        void Update()
        {
            if (Assets.Scripts.Base.Core.Game.State != default)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    var isForward = true;

                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        isForward = false;
                    }

                    LoopSecurityForce(isForward);
                }

                var isControlDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl);

                if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
                {
                    HandleKey(0, isControlDown);
                }

                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    HandleKey(1, isControlDown);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    HandleKey(2, isControlDown);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                {
                    HandleKey(3, isControlDown);
                }

                if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                {
                    HandleKey(4, isControlDown);
                }

                if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
                {
                    HandleKey(5, isControlDown);
                }

                if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
                {
                    HandleKey(6, isControlDown);
                }

                if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
                {
                    HandleKey(7, isControlDown);
                }

                if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
                {
                    HandleKey(8, isControlDown);
                }

                if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
                {
                    HandleKey(9, isControlDown);
                }

                if (!Base.Core.Game.State.Mode.DisableShop)
                {
                    moneyText.text = Base.Core.Game.State.AvailableCredits.ToString("F2");

                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        if (shopOverlay.activeSelf)
                        {
                            CloseShop();
                        }
                        else
                        {
                            ShowShop();
                        }
                    }
                }
            }
        }

        private void LoopSecurityForce(Boolean isForward)
        {
            var index = GameHandler.SecurityForces.IndexOf(GameHandler.SelectedTroop);
            var startIndex = index;

            if (isForward)
            {
                do
                {
                    index++;
                    if (index > GameHandler.SecurityForces.Count - 1)
                    {
                        index = 0;
                    }
                    if (index == startIndex)
                    {
                        break;
                    }
                }
                while (!GameHandler.SecurityForces[index].IsMoveable());

            }
            else
            {
                do
                {
                    index--;

                    if (index < 0)
                    {
                        index = GameHandler.SecurityForces.Count - 1;
                    }
                    if (index == startIndex)
                    {
                        break;
                    }
                }
                while (!GameHandler.SecurityForces[index].IsMoveable());
            }

            if (index >= 0 && GameHandler.SecurityForces.Count > index)
            {
                GameHandler.SelectTroop(GameHandler.SecurityForces[index]);
            }
        }

        private void HandleKey(Int32 keyNumber, Boolean isControlDown)
        {
            if (isControlDown)
            {
                AssignSecurityForce(keyNumber);
            }
            else
            {
                SelectBoundSecurityForce(keyNumber);
            }
        }

        private void AssignSecurityForce(Int32 key)
        {
            var securityForce = GameHandler.SelectedTroop;

            if (securityForce.IsMoveable())
            {
                if (this.boundSecurityForces.TryGetValue(key, out var existingSecurityForce))
                {
                    existingSecurityForce.SecurityForce.AssignedKey = default;
                }

                securityForce.SecurityForce.AssignedKey = key;

                this.boundSecurityForces[key] = securityForce;
            }
        }

        private void SelectBoundSecurityForce(Int32 keyNumber)
        {
            if (boundSecurityForces.TryGetValue(keyNumber, out SecurityForceBehaviour securityForce) && securityForce.IsMoveable())
            {
                GameHandler.SelectTroop(securityForce);
            }
        }
    }
}
