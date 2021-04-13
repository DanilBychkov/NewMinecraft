﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Minecraft
{
    public class Maps
    {
        readonly int[,] mainMap = { {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},{2,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,2},{2,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,0,0,3,3,2},{2,0,0,0,0,0,0,0,0,0,0,3,3,3,3,0,0,0,0,3,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,3,3,3,0,3,0,0,3,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,3,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,3,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,3,3,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,3,3,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,5,1,1,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,4,4,0,4,5,5,5,5,1,1,2},
            {2,0,0,0,0,0,0,0,0,4,4,4,4,0,4,4,0,0,1,1,2},{2,0,0,0,0,0,0,0,4,4,4,5,5,5,5,5,5,5,1,1,2},{2,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,1,1,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,5,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},
            {2,0,0,0,0,0,0,6,6,6,6,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,6,6,0,0,6,6,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,6,0,0,0,0,6,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,6,6,0,0,0,6,0,0,0,6,6,0,1,1,2},
            {2,0,0,0,0,0,0,6,0,0,6,6,0,0,0,6,0,0,1,1,2},{2,0,0,0,0,0,0,6,0,0,6,0,0,6,6,6,0,0,1,1,2},{2,0,0,6,0,0,6,6,0,0,6,0,6,6,0,6,0,0,1,1,2},{2,0,6,6,6,0,6,0,0,0,6,6,6,0,0,6,0,0,1,1,2},
            {2,0,6,0,6,0,6,0,0,0,6,0,0,0,0,6,0,0,1,1,2},{2,0,6,6,6,0,6,0,0,0,6,6,6,0,0,6,0,0,1,1,2},{2,0,0,6,0,0,6,6,0,0,6,0,6,6,0,6,0,0,1,1,2},{2,0,0,0,0,0,0,6,0,0,6,0,0,6,6,6,0,0,1,1,2},
            {2,0,0,0,0,0,0,6,0,0,6,6,0,0,0,6,0,0,1,1,2},{2,0,0,0,0,0,6,6,0,0,0,6,0,0,0,6,6,0,1,1,2},{2,0,0,0,0,0,6,0,0,0,0,6,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,6,6,0,0,6,6,0,0,0,0,0,0,1,1,2},
            {2,0,0,0,0,0,0,6,6,6,6,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,4,4,5,5,5,1,1,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,4,5,5,5,1,1,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},
            {2,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,1,1,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,1,1,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,1,1,2},{2,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,1,1,2},{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        };

        readonly int[,] caveMap = { };
        readonly int[,] forestMap = { };
        readonly int[,] arenaTrial = { };

        public int[,] GetMainMap()
        {
            return mainMap;
        }

        public int[,] GetCaveMap()
        {
            return caveMap;
        }

        public int[,] GetArenaTrial()
        {
            return arenaTrial;
        }

        public int[,] GetForestTrial()
        {
            return forestMap;
        }

        public string GetImage()
        {
            return "";
        }
    }
}
