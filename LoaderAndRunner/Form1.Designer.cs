namespace LoaderAndRunner
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
            this.itemsToDo = new System.Windows.Forms.ListView();
            this.itemsCompleted = new System.Windows.Forms.ListView();
            this.run = new System.Windows.Forms.Button();
            this.loading = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // itemsToDo
            // 
            this.itemsToDo.Location = new System.Drawing.Point(12, 12);
            this.itemsToDo.Name = "itemsToDo";
            this.itemsToDo.Size = new System.Drawing.Size(121, 97);
            this.itemsToDo.TabIndex = 0;
            this.itemsToDo.UseCompatibleStateImageBehavior = false;
            // 
            // itemsCompleted
            // 
            this.itemsCompleted.Location = new System.Drawing.Point(276, 12);
            this.itemsCompleted.Name = "itemsCompleted";
            this.itemsCompleted.Size = new System.Drawing.Size(121, 97);
            this.itemsCompleted.TabIndex = 1;
            this.itemsCompleted.UseCompatibleStateImageBehavior = false;
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(152, 12);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(100, 25);
            this.run.TabIndex = 2;
            this.run.Text = "Run";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // loading
            // 
            this.loading.Location = new System.Drawing.Point(152, 43);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(100, 25);
            this.loading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.loading.TabIndex = 3;
            this.loading.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 136);
            this.Controls.Add(this.loading);
            this.Controls.Add(this.run);
            this.Controls.Add(this.itemsCompleted);
            this.Controls.Add(this.itemsToDo);
            this.Name = "Form1";
            this.Text = "LoadAndRun";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView itemsToDo;
        private System.Windows.Forms.ListView itemsCompleted;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.ProgressBar loading;
    }
}

