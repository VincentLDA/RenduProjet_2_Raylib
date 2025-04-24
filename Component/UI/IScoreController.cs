using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S.Component.UI
{
    public interface IScoreController
    {
        void AddScore(int score);
        int GetScore();

        void Reset();
    }
}
