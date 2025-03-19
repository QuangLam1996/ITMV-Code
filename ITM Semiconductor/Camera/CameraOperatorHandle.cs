using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionInspection
{
    class CameraOperatorHandle
    {
        public bool CrossCenter = false;
        public bool Gridview = false;
        public CameraOperatorHandle()
        {
            this.CrossCenter = false;
            this.Gridview = false;
        }
    }
}
