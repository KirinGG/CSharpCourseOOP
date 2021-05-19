using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperUI
{
    class ModelField
    {
        public bool IsBomb { get; set; }

        public bool IsDefused { get; set; }

        public bool IsOpened { get; set; }

        public int BombsCount { get; set; }

        public ModelField(bool isBomb)
        {
            IsBomb = isBomb;
        }

        public void Check()
        {
            if (IsBomb)
            {
                // TODO Game over
            }

            // TODO open field
        }

        public void Mark()
        {
            IsDefused = !IsDefused;
        }
    }
}
