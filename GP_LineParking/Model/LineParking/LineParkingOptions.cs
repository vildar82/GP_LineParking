using System;
using System.IO;
using AcadLib.Files;

namespace GP_LineParking.Model.LineParking
{
   [Serializable]
   public class LineParkingOptions
   {
      private static readonly string fileOptions = Path.Combine(
         AutoCAD_PIK_Manager.Settings.PikSettings.ServerShareSettingsFolder, "ГП\\LineParking.xml");

      private static LineParkingOptions _instance;
      private double _parkingLength;
      private string _parkingLineLayer;
      private double _parkingWidth;
      private double _parkingAngle;

      private LineParkingOptions()
      {
      }

      public static LineParkingOptions Instance
      {
         get
         {
            if (_instance == null)
            {
               _instance = load();
            }
            return _instance;
         }
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

      public double ParkingWidth
      {
         get { return _parkingWidth; }
         set { _parkingWidth = value; }
      }

      public double ParkingAngle
      {
         get { return _parkingAngle; }
         set { _parkingAngle = value; }
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

      private static LineParkingOptions defaultOptions()
      {
         LineParkingOptions options = new LineParkingOptions();
         options.ParkingLength = 5.5;
         options.ParkingWidth = 2.5;
         options.ParkingLineLayer = "";
         options.ParkingAngle = 90;
         return options;
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
                  options.checkValues();
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

      private void checkValues()
      {
         if (!CheckAngle(ParkingAngle))
         {
            ParkingAngle = 90;
         }
         if (!CheckLen(ParkingLength))
         {
            ParkingLength = 5.5;
         }
         if (!CheckLen(ParkingWidth))
         {
            ParkingWidth = 2.5;
         }
         if (!CheckLayer(ParkingLineLayer))
         {
            ParkingLineLayer = ParkingLineLayer.GetValidDbSymbolName();
         }
      }

      public static bool CheckAngle(double angle)
      {
         return angle > 0 && angle <= 90;         
      }

      public static  bool CheckLen(double len)
      {
         return len > 0;
      }

      public static bool CheckLayer(string layer)
      {
         return layer.IsValidDbSymbolName();
      }
   }
}