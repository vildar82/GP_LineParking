namespace GP_LineParking.Model.LineParking
{
   partial class FormOptions
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.label1 = new System.Windows.Forms.Label();
         this.textBoxWidth = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.textBoxLength = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.textBoxLayer = new System.Windows.Forms.TextBox();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
         this.SuspendLayout();
         // 
         // buttonCancel
         // 
         this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.buttonCancel.Location = new System.Drawing.Point(192, 131);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(75, 23);
         this.buttonCancel.TabIndex = 0;
         this.buttonCancel.Text = "Отмена";
         this.buttonCancel.UseVisualStyleBackColor = true;
         // 
         // buttonOk
         // 
         this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.buttonOk.Location = new System.Drawing.Point(111, 131);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.Size = new System.Drawing.Size(75, 23);
         this.buttonOk.TabIndex = 0;
         this.buttonOk.Text = "ОК";
         this.buttonOk.UseVisualStyleBackColor = true;
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(12, 15);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(153, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "Ширина парковочного места";
         // 
         // textBoxWidth
         // 
         this.textBoxWidth.Location = new System.Drawing.Point(171, 12);
         this.textBoxWidth.Name = "textBoxWidth";
         this.textBoxWidth.Size = new System.Drawing.Size(100, 20);
         this.textBoxWidth.TabIndex = 2;
         this.textBoxWidth.Text = "2.5";
         this.textBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(12, 47);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(147, 13);
         this.label2.TabIndex = 1;
         this.label2.Text = "Длина парковочного места";
         // 
         // textBoxLength
         // 
         this.textBoxLength.Location = new System.Drawing.Point(171, 44);
         this.textBoxLength.Name = "textBoxLength";
         this.textBoxLength.Size = new System.Drawing.Size(100, 20);
         this.textBoxLength.TabIndex = 2;
         this.textBoxLength.Text = "2.5";
         this.textBoxLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(12, 83);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(137, 13);
         this.label3.TabIndex = 1;
         this.label3.Text = "Слой для линии парковки";
         // 
         // textBoxLayer
         // 
         this.textBoxLayer.Location = new System.Drawing.Point(171, 80);
         this.textBoxLayer.Name = "textBoxLayer";
         this.textBoxLayer.Size = new System.Drawing.Size(100, 20);
         this.textBoxLayer.TabIndex = 2;
         this.toolTip1.SetToolTip(this.textBoxLayer, "Имя слоя или пусто для рисования в текущем слое");
         // 
         // errorProvider1
         // 
         this.errorProvider1.ContainerControl = this;
         // 
         // FormOptions
         // 
         this.AcceptButton = this.buttonOk;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.buttonCancel;
         this.ClientSize = new System.Drawing.Size(282, 166);
         this.Controls.Add(this.textBoxLayer);
         this.Controls.Add(this.textBoxLength);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.textBoxWidth);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.buttonOk);
         this.Controls.Add(this.buttonCancel);
         this.Name = "FormOptions";
         this.Text = "FormOptions";
         ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox textBoxWidth;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox textBoxLength;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox textBoxLayer;
      private System.Windows.Forms.ToolTip toolTip1;
      private System.Windows.Forms.ErrorProvider errorProvider1;
   }
}