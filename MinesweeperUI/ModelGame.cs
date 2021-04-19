using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperUI
{
    class ModelGame
    {
        ModelField[,] fields;
        ModelSetting setting;

        ModelGame ()
        {
            setting = new ModelSetting();
            fields = new ModelField[setting.Size, setting.Size];
        }

        public void Initialize()
        {
            var bombsNumber = 0;

            for(var i = 0; i < fields.GetLength(0); i++)
            {
                for(var j=0; j< fields.GetLength(1); j++)
                {
                    if (bombsNumber < setting.BomsNumber)
                    {
                        Random random = new Random(2);
                        fields[i, j] = new ModelField(random.Next() > 0);
                    }

                    fields[i, j] = new ModelField(false);
                }
            }
        }

        public void CheckField(ModelField field)
        {
            if(field.IsBomb && !field.IsDefused)
            {
                // TODO Игра закончена
            }

            if(!field.IsBomb && field.IsDefused)
            {
                // TODO Ничего не происходит
            }
        }

        public void MarkField()
        {

        }

        // число мин рядом 0 - 8
        // мина
        // флажек
    }
}
