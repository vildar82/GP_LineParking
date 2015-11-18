using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace GP_LineParking.Model.LineParking
{
   public class JigLineParking : DrawJig
   {
      private double _equalPoint = Tolerance.Global.EqualPoint;
      private List<Line> _lines;      
      private Point3d _ptFirst;
      private Point3d _ptLast;
      private LineParkingOptions _options;

      public JigLineParking(Point3d ptFirst, LineParkingOptions options)
      {
         _options = options;
         _ptFirst = ptFirst;
      }

      ~JigLineParking()
      {
      }

      public List<Line> Lines { get { return _lines; } }
      public Matrix3d UCS { get { return Application.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem; } }

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
            if (_ptLast.DistanceTo(ptNew) <= _equalPoint)
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

      private List<Line> getLines()
      {
         List<Line> lines = new List<Line>();

         // основная линия
         Line lineBase = new Line(_ptFirst, _ptLast);
         lines.Add(lineBase);

         var vec = _ptLast - _ptFirst;
         var remainLen = vec.Length;
         var vecNormal = vec.GetNormal();
         var gragAngleParking = (180-_options.ParkingAngle) * (Math.PI / 180);
         var vecParking = vecNormal.RotateBy(gragAngleParking, Vector3d.ZAxis);
         var ptParkingStart = _ptFirst;
         // первая линия парковки
         Point3d ptEndFirstLineParking = ptParkingStart + vecParking * _options.ParkingLength;
         Line firstLineParking = new Line(ptParkingStart, ptEndFirstLineParking);
         lines.Add(firstLineParking);
         // вторая линия парковки
         Point3d ptEndSecondLineParking = ptEndFirstLineParking - vecParking.GetPerpendicularVector() * _options.ParkingWidth;
         double widthBase = _options.ParkingWidth / (Math.Sin(gragAngleParking));
         ptParkingStart = ptParkingStart + vecNormal * widthBase;
         Line secondLineParking = new Line(ptParkingStart, ptEndSecondLineParking);
         lines.Add(secondLineParking);
         remainLen -= widthBase;
         // последующии линии парковки
         while (remainLen >= _options.ParkingWidth)
         {
            ptParkingStart = ptParkingStart + vecNormal * widthBase;
            ptEndSecondLineParking = ptEndSecondLineParking + vecNormal * widthBase;
            Line nextLineParking = new Line(ptParkingStart, ptEndSecondLineParking);
            lines.Add(nextLineParking);

            remainLen -= widthBase;
         }
         return lines;         
      }
   }
}