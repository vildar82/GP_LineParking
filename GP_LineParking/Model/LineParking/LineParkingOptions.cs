using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AcadLib.Files;
using AutoCAD_PIK_Manager;

namespace GP_LineParking.Model.LineParking
{
   [Serializable]
   public class LineParkingOptions
   {
      private static LineParkingOptions _instance;
      private static readonly string fileOptions = Path.Combine(
         AutoCAD_PIK_Manager.Settings.PikSettings.ServerShareSettingsFolder, "ГП\\LineParking.xml");
      private double _parkingWidth;
      private double _parkingLength;
      private string  _parkingLineLayer;

      private LineParkingOptions()
      {

      }

      public double ParkingWidth
      {
         get { return _parkingWidth; }
         set { _parkingWidth = value; }
      }
      public double ParkingLength
      {
         get { return _parkingLength; }
         set { _parkingLength = value; }
      }

      public string ParkingLineLayer
      {
         get { return _parkingLineLayer; }
         set { _parkingLineLayer = value; }
      }

      public static LineParkingOptions Instance
      {
         get
         {
            if(_instance == null)
            {
               _instance = load();
            }
            return _instance;
         }
      }

      private static LineParkingOptions load()
      {
         LineParkingOptions options = null;
         // загрузка из файла настроек         
         if (File.Exists(fileOptions))
         {
            SerializerXml xmlSer = new SerializerXml(fileOptions);
            try
            {
               options = xmlSer.DeserializeXmlFile<LineParkingOptions>();
               if (options != null)
               {
                  return options;
               }
            }
            catch (Exception ex)
            {
               Log.Error(ex, "Не удалось десериализовать настройки из файла {0}", fileOptions);
            }
         }
         return defaultOptions();
      }

      private static LineParkingOptions defaultOptions()
      {
         LineParkingOptions options = new LineParkingOptions ();
         options.ParkingLength = 5.5;
         options.ParkingWidth = 2.5;
         options.ParkingLineLayer = "";         
         return options;      
      }

      public static void Save()
      {
         try
         {
            if (!File.Exists(fileOptions))
            {
               Directory.CreateDirectory(Path.GetDirectoryName(fileOptions));
            }
            SerializerXml xmlSer = new SerializerXml(fileOptions);
            xmlSer.SerializeList(Instance);
         }
         catch (Exception ex)
         {
            Log.Error(ex, "Не удалось сериализовать настройки в {0}", fileOptions);
         }
      }
   }
}
