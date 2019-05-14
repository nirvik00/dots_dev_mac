using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino.Geometry;

namespace dots_dev
{
    public class BSP
    {
        public Plane BasePlane = Plane.WorldXY;
        List<string> AdjObjLi = new List<string>(); // final adj obj list
        List<string> GeomObjLi = new List<string>(); // final geom object list 

        public BSP(List<string> adjobjli, List<string> geomobjli)
        {
            AdjObjLi = new List<string>();
            GeomObjLi = new List<string>();
            AdjObjLi = adjobjli;
            GeomObjLi = geomobjli;
        }
    }
}