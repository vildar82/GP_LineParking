using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace GP_LineParking.Model.LineParking
{
   public class JigLineParking : DrawJig
   {      
      private Point3d _ptFirst;
      private Point3d _ptLast;
      private double _parkingWidth;
      private double _parkingLength;
      private List<Line> _lines;
      private double _equalPoint = Tolerance.Global.EqualPoint;

      public JigLineParking(Point3d ptFirst, double parkingWidth, double parkingLength)
      {
         _parkingLength = parkingLength;
         _parkingWidth = parkingWidth;
         _ptFirst = ptFirst;
      }

      ~JigLineParking() { }      
      
      public Matrix3d UCS { get { return Application.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem; } }

      public List<Line> Lines { get { return _lines; } }

      protected override bool WorldDraw(WorldDraw draw)
      {
         WorldGeometry geo = draw.Geometry;
         if (geo != null)
         {
            // Отрисовка линий.            
            _lines = getLines();
            foreach (var line in _lines)
            {               
               geo.Draw(line);
            }                       
         }
         return true;
      }      

      protected override SamplerStatus Sampler(JigPrompts prompts)
      {
         SamplerStatus state = SamplerStatus.NoChange;
         JigPromptPointOptions prOpt = new JigPromptPointOptions("\nУкажите точку");

         prOpt.BasePoint = _ptFirst;
         prOpt.UseBasePoint = true;

         PromptPointResult prRes = prompts.AcquirePoint(prOpt);
         if (prRes.Status == PromptStatus.Error || prRes.Status == PromptStatus.Cancel)
         {
            state = SamplerStatus.Cancel;
         }
         else
         {
            var ptNew = prRes.Value.TransformBy(UCS);
            if (_ptLast.DistanceTo(ptNew)<= _equalPoint)
            {
               state = SamplerStatus.NoChange;
            }
            else
            {
               _ptLast = ptNew;
               state = SamplerStatus.OK;
            }            
         }
         return state;
      }

      private List<Line> getLines()
      {
         List<Line> lines = new List<Line>();

         // основная линия
         Line lineBase = new Line(_ptFirst, _ptLast);
         lines.Add(lineBase);

         var vec = _ptLast - _ptFirst;
         var remainLen = vec.Length;
         var vecNormal = vec.GetNormal();
         var vecPer = vecNormal.GetPerpendicularVector();

         var ptParkingStart = _ptFirst;

         // первая линия парковки
         Point3d ptEndFirstLineParking = ptParkingStart + vecPer * _parkingLength;
         Line firstLineParking = new Line(ptParkingStart, ptEndFirstLineParking);
         lines.Add(firstLineParking);

         // последующии линии парковки
         while (remainLen >= _parkingWidth)
         {
            ptParkingStart = ptParkingStart + vecNormal * _parkingWidth;
            Point3d ptEndSecondLineParking = ptParkingStart + vecPer * _parkingLength;
            Line secondLineParking = new Line(ptParkingStart, ptEndSecondLineParking);
            lines.Add(secondLineParking);

            remainLen -= _parkingWidth;
         }
         return lines;
      }
   }
}


