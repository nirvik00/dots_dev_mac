using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dots_dev
{
    class GeomEntry
    {
        private string Name;
        private double Area;
        private double RatioA;
        private double RatioB;
        private double Length=0.00;
        private double Width=0.00;
        private int Number=1;

        string OPT = ""; // send option of the constructor

        public GeomEntry() { }
        public GeomEntry(string name, double area, double a, double b, int num)
        {
            Name = name;
            Area = area;
            RatioA = a;
            RatioB = b;
            Length = Area * RatioA;
            Width = Area * RatioB;
            Number = num;
            OPT = "opt-0";
        }
        public GeomEntry(string name, double l, double w, int num)
        {
            Name = name;
            Length = l;
            Width = w;
            Number = num;
            OPT = "opt-1";
        }
        public string displayString()
        {
            string s = Name + "," + Length + "," + Width + "," + Number + "," + OPT;
            return s;
        }
    }
    class MakeGeomObjList
    {
        List<string> input;
        List<string> geomObjLi;

        public MakeGeomObjList() { }

        public MakeGeomObjList(List<string> inputstrli)
        {
            geomObjLi = new List<string>();
            input = new List<string>();
            input = inputstrli;
        }

        public double GetDoubleFromString(string str)
        {
            double x = 0.00;
            string s = str.Trim();
            if (String.Equals(str, "")) return x;
            else return Convert.ToDouble(s);
        }

        public double GetInt16FromString(string str)
        {
            int x = 0;
            string s = str.Trim();
            if (String.Equals(str, "")) return x;
            else return Convert.ToInt16(s);
        }

        public List<string> GetGeomObjList()
        {
            string[] headerArr = input[0].Split(',');
            for (int i = 1; i < input.Count; i++)
            {
                int opt = 0; // if 0, use area, ratio else use length, width

                // format of the inputs:
                /// name[0], area[1], ratio (a:b)[2], number[3], length[4], width[5]

                string name = input[i].Split(',')[0];

                double area = GetDoubleFromString(input[i].Split(',')[1]);
                if (area < 0.01) opt++;

                double ratio = GetDoubleFromString(input[i].Split(',')[2]);
                double a = 0.0; double b = 0.0;
                if (ratio == 0.0) { opt++; }
                else
                {
                    a = 1 - ratio;
                    b = ratio;
                }

                int num = Convert.ToInt32(input[i].Split(',')[3]);

                double le = GetDoubleFromString(input[i].Split(',')[4]);
                double wi = GetDoubleFromString(input[i].Split(',')[5]);
                if (le > 0.0 && wi > 0.0) { opt = 0; }
                else { opt = 1; }

                if (num == 0) continue; // num =0, then continue - do not initialize
                else
                {
                    GeomEntry geom;
                    if (opt > 0) { geom = new GeomEntry(name, area, a, b, num); }
                    else { geom = new GeomEntry(name, le, wi, num); }
                    String str = geom.displayString();
                    geomObjLi.Add(str);
                }
            }
            return geomObjLi;
        }
    }
}
