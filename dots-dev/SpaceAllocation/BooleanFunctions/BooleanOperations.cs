using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace dots_dev.SpaceAllocation.BooleanFunctions
{
    public class BooleanOperations : GH_Component
    {
        public BooleanOperations()
          : base("BooleanOperations", "bool",
              "Boolean Ops - union, difference, intersection",
              "DOTS", "BooleanFunctions")
        {
            // do nothing here
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve A", "crvA", "first curve - A", GH_ParamAccess.item);
            pManager.AddCurveParameter("Curve B", "crvB", "second curve - B", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("Union", "union", "Union of 2 curves", GH_ParamAccess.list);
            pManager.AddCurveParameter("Intersection", "intx", "Intersection of 2 curves", GH_ParamAccess.item);
            pManager.AddCurveParameter("Difference", "diff", "Difference of 2 curves", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve crvA = new PolyCurve();
            Curve crvB = new PolyCurve();

            DA.GetData(0, ref crvA);
            DA.GetData(1, ref crvB);

            List<Curve> crvs = new List<Curve>();
            crvs.Add(crvA);
            crvs.Add(crvB);

            Curve[] unionCrvs= Curve.CreateBooleanUnion(crvs);
            DA.SetDataList(0, unionCrvs);

            Curve[] intxCrvs = Curve.CreateBooleanIntersection(crvA, crvB);
            DA.SetDataList(1, intxCrvs);

            Curve[] diffCrvs = Curve.CreateBooleanDifference(crvA, crvB);
            DA.SetDataList(2, diffCrvs);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get { return null; }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("70dc397f-254c-483c-8c7a-9e9b85a05705"); }
        }
    }
}