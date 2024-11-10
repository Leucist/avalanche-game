﻿using Avalanche.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche.Core
{
    public class GameObject : IGameObject
    {

        public GameObject(int x, int y)
        {
            this._x = x;
            this._y = y;
        }
        protected int _x { get; set; }
        protected int _y { get; set; }
    }
}