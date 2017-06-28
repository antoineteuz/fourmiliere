using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourmiliere.Model
{
    public class Position:ViewModelBase
    {
        private int x;
        private int y;
        private int oldX;
        private int oldY;

        public int X { get; }
        public int Y { get; }
        public int OldX { get; }
        public int OldY { get; }

        public Position(int x, int y)
        {
            X = (x < App.DimensionX) ? x: App.DimensionX;
            Y = (y < App.DimensionY) ? y: App.DimensionY;
        }

        public bool Update(int newX, int newY)
        {
            if (newX != oldX && newY != oldY)
            {
                if ((newX >= 0) && (newX < App.DimensionX))
                {
                    oldX = this.x;
                    this.x = newX;
                }
                else return false;

                if ((newY >= 0) && (newY < App.DimensionY))
                {
                    oldY = this.y;
                    this.y = newY;
                }
                else return false;
            }
            else return false;
            return true;
        }
    }
}
