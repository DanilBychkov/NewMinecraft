using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    class TrialsInfo
    {
        public TrialsInfo()
        { 
        
        }

        public Point GetStartPoint(Trials trial)
        {
            return new Point();
        }

        public Point GetPointToNextLevel(Trials trial)
        {
            return new Point();
        }

        public Trials GetNextLevel(Trials trial)
        {
            return Trials.MainTrial;
        }
    }
}
