using System;
using System.Collections.Generic;

using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace dots_dev
{
    public class BspGeom
    {
        List<PolylineCurve> fPolys = new List<PolylineCurve>();
        List<string> rVal = new List<string>();
        Random rnd = new Random();

        public BspGeom() { }

        public List<PolylineCurve> GetFPolys()
        {
            return fPolys;
        }
        public List<string> GetRVals()
        {
            return rVal;
        }

        public PolylineCurve GenerateInitialCurve(List<GeomEntry> geomobjli)
        {

            double sum = 0.0;
            for (int i = 0; i < geomobjli.Count; i++)
            {
                double ar = geomobjli[i].getArea();
                int num = geomobjli[i].getNumber();
                sum += (ar * num);
            }

            double di = Math.Sqrt(sum);
            Point3d[] pts = new Point3d[5];
            pts[0] = new Point3d(0, 0, 0);
            pts[1] = new Point3d(di, 0, 0);
            pts[2] = new Point3d(di, di, 0);
            pts[3] = new Point3d(0, di, 0);
            pts[4] = new Point3d(0, 0, 0);
            PolylineCurve poly = new PolylineCurve(pts);
            return poly;
        }

        public void RunRecursions(PolylineCurve poly, int counter, List<PolylineCurve> POLYS)
        {
            counter++;
            double t = rnd.NextDouble();
            PolylineCurve[] polys;
            if (t < 0.5) { polys = verSplit(poly); }
            else { polys = horSplit(poly); }
            if (counter < 3)
            {
                RunRecursions(polys[0], counter, POLYS);
                RunRecursions(polys[1], counter, POLYS);
            }
            else
            {
                fPolys.Add(polys[0]);
                fPolys.Add(polys[1]);
            }
        }

        public PolylineCurve[] verSplit(PolylineCurve crv)
        {
            var T = crv.TryGetPolyline(out Polyline poly);
            List<Point3d> Pts = new List<Point3d>();
            IEnumerator<Point3d> pts = poly.GetEnumerator();
            while(pts.MoveNext()) { 
                Pts.Add(pts.Current); 
            }

            Point3d[] arr = Pts.ToArray();

            double t = rnd.NextDouble();
            if (t < 0.2) t = 0.2;
           
            rVal.Add(t.ToString());

            // input
            Point3d a = arr[0];
            Point3d b = arr[1];
            Point3d c = arr[2];
            Point3d d = arr[3];

            // vertical split
            Point3d e = new Point3d(a.X, (a.Y + d.Y) * t, 0);
            Point3d f = new Point3d(b.X, e.Y, 0);
            Point3d[] pts1 = { a, b, f, e, a };
            Point3d[] pts2 = { e, f, c, d, e };
            PolylineCurve poly1= new PolylineCurve(pts1);
            PolylineCurve poly2 = new PolylineCurve(pts2);
            PolylineCurve[] polys = { poly1, poly2 };
            return polys;
        }

        public PolylineCurve[] horSplit(PolylineCurve crv)
        {
            var T = crv.TryGetPolyline(out Polyline poly);
            List<Point3d> Pts = new List<Point3d>();
            IEnumerator<Point3d> pts = poly.GetEnumerator();
            while (pts.MoveNext())
            {
                Pts.Add(pts.Current);
            }

            Point3d[] arr = Pts.ToArray();

            double t = rnd.NextDouble();
            if (t < 0.2) t = 0.2;

            rVal.Add(t.ToString());

            // input 
            Point3d a = arr[0];
            Point3d b = arr[1];
            Point3d c = arr[2];
            Point3d d = arr[3];

            // horizontal split
            Point3d e = new Point3d((a.X +b.X)*t, a.Y, 0);
            Point3d f = new Point3d(e.X, d.Y , 0);
            Point3d[] pts1 = { a, e, f, d, a };
            Point3d[] pts2 = { e, b, c, f, e };
            PolylineCurve poly1 = new PolylineCurve(pts1);
            PolylineCurve poly2 = new PolylineCurve(pts2);
            PolylineCurve[] polys = { poly1, poly2 };
            return polys;
        }
    }
}






