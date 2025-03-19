using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class ModelSetting
    {
        private int SetupColum;

        private string NameMachine;

        public int SetupColum1 { get => SetupColum; set => SetupColum = value; }
        public string NameMachine1 { get => NameMachine; set => NameMachine = value; }

        public ModelSetting() 
        { 
          this.SetupColum = 5;
          this.NameMachine = "ITM SEMICONDUCTOR";
        }

    }
}
