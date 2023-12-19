using CMCore.Managers;
using CMCore.Utilities;
using CMCore.Utilities.Extensions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CMCore.Behaviors.UI.Page.Children
{
    public class LoadingUI : UIBase
    {
        private float _closeDelay;

        public float CloseDelay => _closeDelay == 0 ? _closeDelay = DataManager.GetValueFromJson(GameManager.DataManager.RemoteData, "loading_ui_delay", 0.0f) : _closeDelay;

        protected override void OnHide()
        {
            KillTweens();
            textLoading.DOColor(Color.clear, FadeOutDuration).SetDelay(CloseDelay);
            foreach (var image in loadingAnimatedIcons) image.DOColor(Color.clear, FadeOutDuration).SetDelay(CloseDelay);

            foreach (var bb in bgImage)
            {
                bb.DOColor(Color.clear, FadeOutDuration).SetDelay(CloseDelay).OnComplete(() =>
                {
                    base.OnHide();
                    KillTweens();
                });
            }
        }

        protected override void OnShow()
        {
            base.OnShow();

            KillTweens();
            foreach (var bb in bgImage)
            {
                bb.color = bgImageEnabledColor;
            }


            textLoading.color = textEnabledColor;
            foreach (var image in loadingAnimatedIcons) image.color = iconEnabledColor;
            gameObject.SetActive(true);
            GameManager.Instance.DelayedAction(Hide,
                new WaitUntil(() =>
                    !string.IsNullOrEmpty(GameManager.DataManager.RemoteData) || GameManager.DataManager.Attempts >=
                    this.Settings().MaximumAttemptToRequestData));
        }

        private void KillTweens()
        {
            foreach (var bb in bgImage)
            {
                bb.DOKill();
            }

            textLoading.DOKill();
            foreach (var image in loadingAnimatedIcons) image.DOKill();
        }

        /////////////////////////////


        [SerializeField] private Image[] bgImage;
        [SerializeField] private TextMeshProUGUI textLoading;
        [SerializeField] private Image[] loadingAnimatedIcons;


        [SerializeField] private Color iconEnabledColor;
        [SerializeField] private Color textEnabledColor;
        [SerializeField] private Color bgImageEnabledColor;

        private const float FadeOutDuration = 0.5f;
    }
}