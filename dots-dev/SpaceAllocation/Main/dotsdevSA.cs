using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace dots_dev
{
    public class dotsdevSA : GH_Component
    {
        // global variables
        List<string> adjMatrixStr = new List<string>(); // adjacency matrix
        List<string> geomSpaceStr = new List<String>(); // functional space - geometric requirements
        List<string> adjObjLi = new List<string>(); // final adj obj list
        List<string> geomObjLi = new List<string>(); // final geom object list 

        public dotsdevSA()
          : base("dots_dev", "inputs",
              "spatial allocation using binary partition",
              "DOTS", "BSP")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("File Path Adjacency", "iAdjacencyPath", "adjacency matrix: csv file", GH_ParamAccess.item);
            pManager.AddTextParameter("File Path Geometry", "iGeomPath", "geometric requirements: csv file", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            // pManager.AddTextParameter("Output Adjacency Intermediate", "oAdj0", "intermediate output of reading adjacency requirements", GH_ParamAccess.list);
            // pManager.AddTextParameter("Output Functions Intermediate", "oGeom0", "intermediate output of reading geometric requirements", GH_ParamAccess.list);
            pManager.AddTextParameter("Output Adjacency", "oAdj", "output of reading adjacency requirements", GH_ParamAccess.list);
            pManager.AddTextParameter("Output Functions", "oGeom", "output of reading spatial (geometric) requirements", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string adjFilePath = "null";
            string geomFilePath = "null";

            DA.GetData(0, ref adjFilePath);
            DA.GetData(1, ref geomFilePath);

            CsvParser csvParserAdj = new CsvParser(adjFilePath);
            adjMatrixStr = csvParserAdj.readFile(); // list of strings - not fields
            adjObjLi=csvParserAdj.GetAdjObjLi(adjMatrixStr); // list of adj objs

            CsvParser csvParserGeom = new CsvParser(geomFilePath);
            geomSpaceStr = csvParserGeom.readFile(); // list of strings - not fields
            geomObjLi = csvParserGeom.GetGeomObjLi(geomSpaceStr); // list of geom objs
            
            // geomSpaceStr
            // DA.SetDataList(0, adjMatrixStr); // deprecated
            // DA.SetDataList(1, geomSpaceStr); //deprecated
            DA.SetDataList(0, adjObjLi);
            DA.SetDataList(1, geomObjLi);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("73dd4e25-553b-4853-a8e4-5d17d96afa84"); }
        }
    }
}
