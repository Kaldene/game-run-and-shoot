using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarPlayer : MonoBehaviour
{
    [SerializeField] private Image mask;
    [SerializeField] private float originalSize;

    public static UIHealthBarPlayer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            return;
        }

        Instance = this;
    }

    private void  Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
