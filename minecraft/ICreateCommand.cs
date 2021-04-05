using System;
using System.Collections.Generic;
using System.Text;

namespace Minecraft
{
    public interface ICreateCommand
    {
        static int x;
        static int y;
        static ICreateElement createElement;
        static bool deadInConflict;
        static int health;
        static int armor;
    }
}
