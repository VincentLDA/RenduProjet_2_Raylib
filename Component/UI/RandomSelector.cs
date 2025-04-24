using Projet_S.Component.PNJ;
using Projet_S.Enums;
using Projet_S.SceneUtils;
using Projet_S.Services;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S.Component.UI
{
    public class RandomSelector : Scene
    {

        private GameTimer selectorTimer;
        private GameTimer rollTime;
        private Snake snake;
        private CubeRandom cubeRandom;

        private int statut = 0 ;

        private CubeRandomBonus cubeRandomBonus;

        public RandomSelector(CubeRandom cubeRandom, Snake snake) { 
            this.snake = snake;
            this.cubeRandom = cubeRandom;

           
        }

        public override void Draw()
        {
            string bonus = cubeRandom.di_cubeRandomBonus.FirstOrDefault(c => c.Key == cubeRandom.CubeRandomBonus).Value.ToString() ;
            Raylib.DrawText($"{bonus}", 20, 250, 35, Color.White);            
        }

        public override void Load()
        {
            selectorTimer = new GameTimer((float)0.2f, updateRoll);
            rollTime = new GameTimer((float)5, null, false);
            selectorTimer.Stop();
            rollTime.Stop();
        }

        public override void Unload()
        {
        }

        public override void Update()
        {
            selectorTimer.Update(Raylib.GetFrameTime());
            rollTime.Update(Raylib.GetFrameTime());
        }

        public void updateRoll()
        {
            Console.WriteLine($"UPD {cubeRandom.di_cubeRandomBonus.FirstOrDefault(c => c.Key == cubeRandom.CubeRandomBonus).Value.ToString()}");
            cubeRandom.Rand();
            if (!rollTime.isRunning)
            {
                selectorTimer.Stop();
                rollTime.Stop();

                Console.WriteLine($"APPLY {cubeRandom.di_cubeRandomBonus.FirstOrDefault(c => c.Key == cubeRandom.CubeRandomBonus).Value.ToString()}");
                cubeRandom.ApplyBonus();
            }
        }

        public void ROLL()
        {

            Console.WriteLine($"Rand ROLL");
            rollTime.Start();
            selectorTimer.Start();
            
            
        }


    }
}
