using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S.Component.UI
{
        public class HightScore : IHightScoreController
    {
        static string scoreFilePath = "scores.txt";
        static int maxScores = 10;
        private int currentScore = 0;

        public HightScore()
        {
            ServicesLocator.Register<IHightScoreController>(this);
        }

        public List<int> LoadScores()
        {
            if (!File.Exists(scoreFilePath))
            {
                var defaultScores = Enumerable.Repeat(0, maxScores).ToList();
                SaveScores(defaultScores);
                return defaultScores;
            }

            List<int> scores = new List<int>();
            if (File.Exists(scoreFilePath))
            {
                string[] lines = File.ReadAllLines(scoreFilePath);
                foreach (var line in lines)
                {
                    if (int.TryParse(line, out int score))
                        scores.Add(score);
                }
            }
            while (scores.Count < maxScores)
                scores.Add(0);

            return scores;
        }

        public void SaveScores(List<int> scores)
        {
            File.WriteAllLines(scoreFilePath, scores.Select(s => s.ToString()).ToArray());
        }

        public void UpdateScores(int newScore)
        {
            List<int> scores = LoadScores();
            scores.Add(newScore);
            scores = scores.OrderByDescending(s => s).Take(maxScores).ToList();
            SaveScores(scores);
        }


        public void Print(List<int> scores, int X_Title, int Y_Title)
        {
            int yHG = 150;
            Raylib.DrawText("TOP 10", X_Title, Y_Title, 40, Color.Gold);
            for (int i = 0; i < scores.Count; i++)
            {
                string pos = $"{i + 1}.";
                string scoreText = $" {scores[i]}";
                Raylib.DrawText(pos, X_Title, Y_Title + 45 + i * 35, 35, Color.White);
                Raylib.DrawText(scoreText, X_Title + 40, Y_Title + 45 + i * 35, 35, Color.White);
            }

        }

    }
}
