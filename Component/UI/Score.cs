using Projet_S.Services;

namespace Projet_S.Component.UI
{
    public class Score : IScoreController
    {

        private int currentScore = 0;

        public Score()
        {
            ServicesLocator.Register<IScoreController>(this);
        }

        public void AddScore(int score) => currentScore += score;
        

        public int GetScore() 
        {
            return currentScore;
        }

        public void Reset()
        {
            currentScore = 0;
        }
    }
}
