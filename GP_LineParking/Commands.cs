using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using GP_LineParking.Model.LineParking;

[assembly: CommandClass(typeof(GP_LineParking.Commands))]

namespace GP_LineParking
{
   public class Commands
   {
      [CommandMethod("PIK", "GP-LineParking", CommandFlags.Modal)]
      public void LineParkingCommand ()
      {
         // рисование линии парковки
         Document doc = Application.DocumentManager.MdiActiveDocument;
         if (doc == null) return;         
         Database db = doc.Database;
         Editor ed = doc.Editor;
         
         // запрос первой точки
         PromptPointOptions optFisrstPt = new PromptPointOptions("Укажите первую точку");
         optFisrstPt.Keywords.Add("Options");         
         var resFirstPt = ed.GetPoint(optFisrstPt);
         if (resFirstPt.Status == PromptStatus.OK)
         {
            checkLayer(db);
            JigLineParking jigLineParking = new JigLineParking(resFirstPt.Value.TransformBy(ed.CurrentUserCoordinateSystem),
               LineParkingOptions.Instance.ParkingWidth, LineParkingOptions.Instance.ParkingLength);
            var jigRes = ed.Drag(jigLineParking);
            if (jigRes.Status == PromptStatus.OK && jigLineParking.Lines.Count>0)
            {
               // Добавление полилинии в чертеж.
               using (var tr = db.TransactionManager.StartTransaction())
               {
                  BlockTableRecord cs = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                  foreach (var line in jigLineParking.Lines)
                  {
                     line.SetDatabaseDefaults(db);
                     // слой
                     if (!string.IsNullOrWhiteSpace(LineParkingOptions.Instance.ParkingLineLayer))
                     {
                        line.Layer = LineParkingOptions.Instance.ParkingLineLayer;
                     }
                     cs.AppendEntity(line);
                     tr.AddNewlyCreatedDBObject(line, true);
                  }                  
                  tr.Commit();
               }
            }
         }
         else if (resFirstPt.Status ==  PromptStatus.Keyword)
         {
            if (resFirstPt.StringResult == "Options")
            {
               FormOptions formOpt = new FormOptions(LineParkingOptions.Instance);
               if ( Application.ShowModalDialog(formOpt)== System.Windows.Forms.DialogResult.OK)
               {
                  LineParkingOptions.Save();
               }
               LineParkingCommand();
            }
         }
      }

      private void checkLayer(Database db)
      {
         if (string.IsNullOrWhiteSpace(LineParkingOptions.Instance.ParkingLineLayer))
            return;// использовать текущий слой
         
         using (var lt = db.LayerTableId.Open( OpenMode.ForRead) as LayerTable) 
         {
            if (lt.Has(LineParkingOptions.Instance.ParkingLineLayer))
            {
               using (var layer = lt[LineParkingOptions.Instance.ParkingLineLayer].Open(OpenMode.ForRead) as LayerTableRecord)
               {
                  if (layer.IsOff || layer.IsLocked || layer.IsFrozen)
                  {
                     layer.UpgradeOpen();
                     if (layer.IsOff)
                     {
                        layer.IsOff = false;
                     }
                     if (layer.IsFrozen)
                     {
                        layer.IsFrozen = false;
                     }
                     if (layer.IsLocked)
                     {
                        layer.IsLocked = false;
                     }
                  }
               }
            }
            else
            {
               using (var layer = new LayerTableRecord())
               {
                  layer.Name = LineParkingOptions.Instance.ParkingLineLayer;
                  lt.UpgradeOpen();
                  lt.Add(layer);                  
               }
            }
         }
      }
   }
}
