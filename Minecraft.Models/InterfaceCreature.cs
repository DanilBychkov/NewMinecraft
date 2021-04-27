using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public interface ICreature
    {
        Point GetPosition();
        void ChangePositionX(int changeX);
        void ChangePositionY(int changeY);
        void ChangePoisition(Point lastPosition);
        bool IsDeadInConflict();
        void ChangeHealth(int damage);
        bool IsSleep();
        void ChangeSleep(bool newSleep);
    }
}
