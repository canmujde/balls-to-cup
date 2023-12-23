using CMCore.Managers;
using CMCore.Models;
using CMCore.Utilities;
using CMCore.Utilities.Extensions;
using UnityEngine;

namespace CMCore.Behaviors.Object
{
    public class LevelBehavior : PrefabBehavior
    {
        private const string c_CylinderPrefab = "CylinderFloor";
        private const string c_CupPrefab = "CupBehavior";
        private const string c_TubePrefab = "TubeBehavior";
        private const string c_TubeIndicatorPrefab = "ExistingBallIndicator";

        private int m_Star1WinBallCount;
        private int m_Star2WinBallCount;
        private int m_Star3WinBallCount;


        private int m_currentCollectedBallCount;
        private int m_currentBallCountInsideTube;
        private bool m_IsInEndState;

        private CupBehavior _cupBehavior;
        private TubeBehavior _tubeBehavior;
        private ExistingBallIndicator _tubeIndicator;

        public int CollectedBallCount => m_currentCollectedBallCount;
        public int InsideTubeBallCount => m_currentBallCountInsideTube;

        public int Star1Count => m_Star1WinBallCount;
        public int Star2Count => m_Star2WinBallCount;
        public int Star3Count => m_Star3WinBallCount;


        public override void ResetBehavior()
        {
            base.ResetBehavior();

            this.GetFromPool(c_CylinderPrefab, transform);
            _cupBehavior = this.GetFromPool(c_CupPrefab, transform).GetComponent<CupBehavior>();
            _tubeBehavior = this.GetFromPool(c_TubePrefab, transform).GetComponent<TubeBehavior>();
            _tubeIndicator = this.GetFromPool(c_TubeIndicatorPrefab, transform).GetComponent<ExistingBallIndicator>();

            

            m_IsInEndState = false;
            m_currentBallCountInsideTube = GameManager.LevelManager.Current.BallCount;
            m_currentCollectedBallCount = 0;

            m_Star1WinBallCount = GameManager.LevelManager.Current.BallCount / 3;
            m_Star2WinBallCount = m_Star1WinBallCount * 2;
            m_Star3WinBallCount = GameManager.LevelManager.Current.BallCount;
            
           
            _tubeBehavior.ResetBehavior();
            _cupBehavior.ResetBehavior();
            _tubeIndicator.ResetBehavior();
            _tubeIndicator.SetTubeContainer(_tubeBehavior.BallReleaserTransform); 
            _tubeIndicator.UpdateText(m_currentBallCountInsideTube);
            GameManager.UIManager.InGameUI.ToggleTutorial(false);
            
            if (!GameManager.LevelManager.TutorialPlayed) 
                GameManager.UIManager.InGameUI.ToggleTutorial(true); // Create TutorialUI and enrich with specific tutorials...

        }

        public void OnBallExitedTube(BallBehavior ball)
        {
            m_currentBallCountInsideTube--;
            _tubeIndicator.UpdateText(m_currentBallCountInsideTube);

            if (m_currentBallCountInsideTube > 0) return;
            if (m_IsInEndState) return;
            m_IsInEndState = true;
            GameManager.Instance.DelayedAction(
                () =>
                {
                    GameManager.EventManager.GameStateChanged?.Invoke(
                        m_currentCollectedBallCount >= m_Star1WinBallCount
                            ? Enums.GameState.Win
                            : Enums.GameState.Fail);
                }, 2.5f);
        }


        public void OnBallCollected(BallBehavior ball)
        {
            m_currentCollectedBallCount++;
            var collectParticle = this.GetFromPool("CollectParticle", transform);
            var particle = collectParticle.GetComponent<ParticleSystem>();

            var ballPosition = ball.transform.position;
            particle.transform.position = ballPosition;
            particle.startColor = ball.Rend.sharedMaterial.color;
            
            var collectIndicator = this.GetFromPool("CollectIndicator", transform);

            collectIndicator.transform.position = ballPosition + Vector3.up;
            
            GameManager.Instance.DelayedAction(() =>
            {
                this.RePool(collectParticle);
                this.RePool(collectIndicator);
            }, 0.5f);


            if (CollectedBallCount >= m_Star3WinBallCount) _cupBehavior.DoStar(1);
            else if (CollectedBallCount>= m_Star2WinBallCount) _cupBehavior.DoStar(2);
            else if (CollectedBallCount>= m_Star1WinBallCount) _cupBehavior.DoStar(3);
            
            
            _cupBehavior.OnCollect();
        }
    }
}