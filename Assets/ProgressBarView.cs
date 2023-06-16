using UnityEngine;
using UnityEngine.UI;

public class ProgressBarView: MonoBehaviour
{
    [SerializeField] private Slider _loadingSlider;

    public void ShowLoadingProgress(float progress)
    {
        _loadingSlider.value = progress;
    }
}
