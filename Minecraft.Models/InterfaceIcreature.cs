using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public interface ICreature
    {
        Point GetChangePosition(int[,] map, ICreature playerPosition,int[] decorationObject, ScreenPoint ChangingScreen);
        Point GetPosition();
        void ChangeHealth(double damage);
        bool IsDiedInConflit();
        bool IsSleep();
        void ChangeSleep(Point playerPosition);
        string GetTypeCreature();
        void MakeNewICreature(Point creatureFinish,String type,int tickCount, List<FlyingObject> flyingObjects);
        int GetSize();
    }
}
