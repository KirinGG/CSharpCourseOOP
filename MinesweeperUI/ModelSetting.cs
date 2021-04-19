using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperUI
{
    class ModelSetting
    {
        public const int DefaultSize = 6;
        public const int DefaultBombsNumber = 8;

        public int Size { get; set; }

        public int BomsNumber { get; set; }

        public ModelSetting()
        {
            Size = DefaultSize;
            BomsNumber = DefaultBombsNumber;
        }
    }
}
