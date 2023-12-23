using CMCore.Behaviors.Object;
using CMCore.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CupBehavior : PrefabBehavior
{
    [SerializeField] private Transform visualTransform;
    [SerializeField] private TextMeshProUGUI fillText;


    [SerializeField] private Image firstStar;
    [SerializeField] private Image secondStar;
    [SerializeField] private Image thirdStar;

    private const float c_baseScale = 0.6f;
    private const float c_punchForce = 0.08f;
    private const float c_punchDuration = 0.2f;

    public override void ResetBehavior()
    {
        base.ResetBehavior();
        fillText.text = 0 + "/" + GameManager.LevelManager.LevelBehavior.Star1Count;
        firstStar.gameObject.SetActive(false);
        secondStar.gameObject.SetActive(false);
        thirdStar.gameObject.SetActive(false);
    }

    public void OnCollect()
    {
        visualTransform.transform.DOKill();
        visualTransform.transform.localScale = c_baseScale * Vector3.one;
        visualTransform.transform.DOPunchScale(Vector3.one * c_punchForce, c_punchDuration, 0, 0);
        fillText.text = GameManager.LevelManager.LevelBehavior.CollectedBallCount + "/" +
                        GameManager.LevelManager.LevelBehavior.Star1Count;
    }

    public void DoStar(int i)
    {
        switch (i)
        {
            case 1:
                if (!firstStar.gameObject.activeSelf)
                {
                    GameManager.AudioManager.PlaySfx("star", 1, 1.4f);
                    firstStar.gameObject.SetActive(true);
                }

                break;
            case 2:
                if (!secondStar.gameObject.activeSelf)
                {
                    GameManager.AudioManager.PlaySfx("star", 1, 1.2f);
                    secondStar.gameObject.SetActive(true);
                }

                break;
            case 3:
                if (!thirdStar.gameObject.activeSelf)
                {
                    GameManager.AudioManager.PlaySfx("star", 1, 1);
                    
                    thirdStar.gameObject.SetActive(true);
                }

                break;
        }
    }
}