using UnityEngine;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class InstallationUI : MonoBehaviour
{
    [SerializeField] private Vector3 originPos = new Vector3(28.44f, 0, 100);
    private Sequence sequence;
    [SerializeField] private List<UnderUI> selects;


    private void OnEnable()
    {
        sequence = DOTween.Sequence();
        sequence.
            Append(transform.DOMove(Vector3.zero, 1f)).
            AppendCallback(() =>
            {
                selects.ForEach(t => { t.FadeIn(1f); });
            });
    }

    public void Exit()
    {
        sequence = DOTween.Sequence();
        sequence.
            Append(transform.DOMove(originPos, 1f)).
        AppendCallback(() => { gameObject.SetActive(false); });
    }
}
