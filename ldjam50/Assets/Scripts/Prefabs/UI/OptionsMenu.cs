
using Assets.Scripts;
using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider EffectsVolumeSlider;
    public Slider AmbienceVolumeSlider;
    public Slider BackgroundVolumeSlider;
    public Toggle AnimationEnabledToggle;

    private void Start()
    {
        this.UpdateValues();
    }

    private void FixedUpdate()
    {
        this.UpdateValues();
    }

    public void OnForegroundSliderChanged()
    {
        Core.Game.EffectsAudioManager.Volume = EffectsVolumeSlider.value;
        Core.Game.Options.EffectsVolume = EffectsVolumeSlider.value;
    }

    public void OnAmbienceSliderChanged()
    {
        Core.Game.AmbienceAudioManager.Volume = AmbienceVolumeSlider.value;
        Core.Game.Options.AmbienceVolume = AmbienceVolumeSlider.value;
    }

    public void OnBackgroundSliderChanged()
    {
        Core.Game.BackgroundAudioManager.Volume = BackgroundVolumeSlider.value;
        Core.Game.Options.BackgroundVolume = BackgroundVolumeSlider.value;
    }

    public void OnAnimationEnabledToggleValueChanged()
    {
        Core.Game.Options.AreAnimationsEnabled = this.AnimationEnabledToggle.isOn;
    }

    public void OnRestoreDefaultsClick()
    {
        Core.Game.PlayButtonSound();
        EffectsVolumeSlider.value = 1f;
        AmbienceVolumeSlider.value = 0.125f;
        BackgroundVolumeSlider.value = 0.125f;
        Core.Game.Options.AreAnimationsEnabled = true;
    }

    private void UpdateValues()
    {
        if (Core.Game.Options != default)
        {
            if (this.EffectsVolumeSlider.value != Core.Game.Options.EffectsVolume)
            {
                this.EffectsVolumeSlider.value = Core.Game.Options.EffectsVolume;
            }

            if (this.AmbienceVolumeSlider.value != Core.Game.Options.AmbienceVolume)
            {
                this.AmbienceVolumeSlider.value = Core.Game.Options.AmbienceVolume;
            }

            if (this.BackgroundVolumeSlider.value != Core.Game.Options.BackgroundVolume)
            {
                this.BackgroundVolumeSlider.value = Core.Game.Options.BackgroundVolume;
            }

            if (this.AnimationEnabledToggle.isOn != Core.Game.Options.AreAnimationsEnabled)
            {
                this.AnimationEnabledToggle.isOn = Core.Game.Options.AreAnimationsEnabled;
            }
        }
    }
}
