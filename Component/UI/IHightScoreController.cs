using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S.Component.UI
{
    public interface IHightScoreController
    {
        List<int> LoadScores();

        void UpdateScores(int newScore);

        void Print(List<int> scores, int X_Title, int Y_Title);
    }
}
