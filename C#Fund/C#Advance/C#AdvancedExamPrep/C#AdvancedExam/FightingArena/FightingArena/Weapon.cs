﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FightingArena
{
    public class Weapon
    {
        private int size;
        private int solidity;
        private int sharpness;

        public Weapon(int size,int solidity, int sharpness)
        {
            this.Size = size;
            this.Solidity = solidity;
            this.Sharpness = sharpness;
        }

        public int Size { get; set; }

        public int Solidity { get; set; }

        public int Sharpness { get; set; }
    }
}
