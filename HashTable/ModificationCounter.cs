using System;
using System.Collections.Generic;
using System.Text;

namespace HashTables
{
    class ModificationCounter
    {
        private bool enable;

        public int Count { get; private set; }

        public bool Enable
        {
            get
            {
                return enable;
            }
            set
            {
                if (value == false)
                {
                    Count = 0;
                }
            }
        }

        public ModificationCounter(bool isEnable)
        {
            Enable = isEnable;
        }

        public void Add()
        {
            if (Enable)
            {
                if (Count == int.MaxValue)
                {
                    Count = 0;
                }

                Count++;
            }
        }
    }
}
