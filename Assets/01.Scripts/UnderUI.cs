using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UnderUI : MonoBehaviour
{
    [SerializeField] Color originColor;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        originColor = image.color;
        image.color = new Color32(0, 0, 0, 0);
    }

    public void FadeIn(float time)
    {
        image.DOColor(originColor, time);
    }

}
