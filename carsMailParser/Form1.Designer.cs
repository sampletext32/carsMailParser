namespace carsMailParser
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonBeginLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBeginLoad
            // 
            this.buttonBeginLoad.Location = new System.Drawing.Point(160, 72);
            this.buttonBeginLoad.Name = "buttonBeginLoad";
            this.buttonBeginLoad.Size = new System.Drawing.Size(456, 248);
            this.buttonBeginLoad.TabIndex = 0;
            this.buttonBeginLoad.Text = "Начать";
            this.buttonBeginLoad.UseVisualStyleBackColor = true;
            this.buttonBeginLoad.Click += new System.EventHandler(this.buttonBeginLoad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 425);
            this.Controls.Add(this.buttonBeginLoad);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBeginLoad;
    }
}

