using System;
using System.Windows.Forms;

namespace GP_LineParking.Model.LineParking
{
   public partial class FormOptions : Form
   {
      private LineParkingOptions _opt;

      public FormOptions(LineParkingOptions opt)
      {
         _opt = opt;
         InitializeComponent();
         textBoxLayer.Text = opt.ParkingLineLayer;
         textBoxLength.Text = opt.ParkingLength.ToString();
         textBoxWidth.Text = opt.ParkingWidth.ToString();
      }

      private void buttonOk_Click(object sender, EventArgs e)
      {
         errorProvider1.Clear();
         if (!checkValues())
         {
            DialogResult = DialogResult.None;
         }
      }

      private bool checkDouble(TextBox textBox, out double dvalue)
      {
         if (!double.TryParse(textBox.Text, out dvalue))
         {
            errorProvider1.SetError(textBox, "Должно быть число");
            return false;
         }
         return true;
      }

      private bool checkValues()
      {
         bool res = true;
         double dvalue;
         // проверка длины
         res = checkDouble(textBoxLength, out dvalue);
         if (res)
         {
            _opt.ParkingLength = dvalue;
         }
         // проверка ширины
         res = checkDouble(textBoxWidth, out dvalue);
         if (res)
         {
            _opt.ParkingWidth = dvalue;
         }
         // проверка имени слоя
         if (!string.IsNullOrEmpty(textBoxLayer.Text))
         {
            res = textBoxLayer.Text.IsValidDbSymbolName();
            if (res)
            {
               _opt.ParkingLineLayer = textBoxLayer.Text;
            }
            else
            {
               errorProvider1.SetError(textBoxLayer, "Недопустимое имя слоя");
               res = false;
            }
         }
         else
         {
            _opt.ParkingLineLayer = string.Empty;
         }
         return res;
      }

      private void textBox_KeyPress(object sender, KeyPressEventArgs e)
      {
         if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
             (e.KeyChar != '.'))
         {
            e.Handled = true;
         }

         // only allow one decimal point
         if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
         {
            e.Handled = true;
         }
      }
   }
}