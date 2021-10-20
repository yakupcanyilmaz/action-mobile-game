using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
  [SerializeField] private string volumeParameter = "MasterVolume";
  [SerializeField] private Slider slider;
  [SerializeField] private Toggle toggle;
  [SerializeField] private Float volume;
  private bool disableToggleEvent;

  private void Awake()
  {
    slider.onValueChanged.AddListener(HandleSliderValueChanged);
    toggle.onValueChanged.AddListener(HandleToggleValueChanged);
  }

  private void Start()
  {
    slider.value = volume.Value;
    // slider.Value = PlayerPrefs.GetFloat(volumeParameter, volume.Value);
  }

  private void HandleSliderValueChanged(float value)
  {
    volume.Value = value;
    AudioManager.Instance.SetVolume(volumeParameter, volume.Value);
    disableToggleEvent = true;
    toggle.isOn = slider.value > slider.minValue;
    disableToggleEvent = false;
  }

  private void HandleToggleValueChanged(bool enableSound)
  {
    if (disableToggleEvent) return;
    if (enableSound)
    {
      slider.value = slider.maxValue;
    }
    else
    {
      slider.value = slider.minValue;
    }
  }

  private void OnDisable()
  {
    PlayerPrefs.SetFloat(volumeParameter, volume.Value);
  }
}
