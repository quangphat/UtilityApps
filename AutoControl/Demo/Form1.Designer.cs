namespace Demo
{
    partial class Form1
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
            this.btnToForm = new System.Windows.Forms.Button();
            this.btnToEntity = new System.Windows.Forms.Button();
            this.autoTextBox2 = new AutoControl.AutoTextBox();
            this.autoTextBox1 = new AutoControl.AutoTextBox();
            this.SuspendLayout();
            // 
            // btnToForm
            // 
            this.btnToForm.Location = new System.Drawing.Point(463, 85);
            this.btnToForm.Name = "btnToForm";
            this.btnToForm.Size = new System.Drawing.Size(75, 23);
            this.btnToForm.TabIndex = 2;
            this.btnToForm.Text = "ToForm";
            this.btnToForm.UseVisualStyleBackColor = true;
            this.btnToForm.Click += new System.EventHandler(this.btnToForm_Click);
            // 
            // btnToEntity
            // 
            this.btnToEntity.Location = new System.Drawing.Point(463, 156);
            this.btnToEntity.Name = "btnToEntity";
            this.btnToEntity.Size = new System.Drawing.Size(75, 23);
            this.btnToEntity.TabIndex = 3;
            this.btnToEntity.Text = "ToEntity";
            this.btnToEntity.UseVisualStyleBackColor = true;
            this.btnToEntity.Click += new System.EventHandler(this.btnToEntity_Click);
            // 
            // autoTextBox2
            // 
            this.autoTextBox2.BindingFor = "Product";
            this.autoTextBox2.BindingName = "ProductName";
            this.autoTextBox2.Location = new System.Drawing.Point(87, 156);
            this.autoTextBox2.Name = "autoTextBox2";
            this.autoTextBox2.Size = new System.Drawing.Size(199, 20);
            this.autoTextBox2.TabIndex = 1;
            // 
            // autoTextBox1
            // 
            this.autoTextBox1.BindingFor = "Product";
            this.autoTextBox1.BindingName = "ProductCode";
            this.autoTextBox1.Location = new System.Drawing.Point(87, 89);
            this.autoTextBox1.Name = "autoTextBox1";
            this.autoTextBox1.Size = new System.Drawing.Size(199, 20);
            this.autoTextBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 331);
            this.Controls.Add(this.btnToEntity);
            this.Controls.Add(this.btnToForm);
            this.Controls.Add(this.autoTextBox2);
            this.Controls.Add(this.autoTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AutoControl.AutoTextBox autoTextBox1;
        private AutoControl.AutoTextBox autoTextBox2;
        private System.Windows.Forms.Button btnToForm;
        private System.Windows.Forms.Button btnToEntity;
    }
}

