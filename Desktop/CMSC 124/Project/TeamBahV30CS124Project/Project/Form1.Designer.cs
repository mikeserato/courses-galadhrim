namespace WindowsFormsApplication1
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
            this.input_console = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lexemes_table = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.symbol_table = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPENToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBOUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pROJECTDETAILSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dEVELOPERSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.clear_button = new System.Windows.Forms.Button();
            this.interpret_button = new System.Windows.Forms.Button();
            this.output_console = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LineNumber = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lexemes_table)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symbol_table)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // input_console
            // 
            this.input_console.AcceptsTab = true;
            this.input_console.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input_console.Location = new System.Drawing.Point(17, 53);
            this.input_console.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.input_console.Name = "input_console";
            this.input_console.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.input_console.Size = new System.Drawing.Size(471, 285);
            this.input_console.TabIndex = 0;
            this.input_console.Text = "";
            this.input_console.SelectionChanged += new System.EventHandler(this.input_console_SelectionChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(500, 27);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(492, 362);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lexemes_table);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Size = new System.Drawing.Size(484, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lexemes_table
            // 
            this.lexemes_table.AllowUserToDeleteRows = false;
            this.lexemes_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lexemes_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.lexemes_table.Location = new System.Drawing.Point(0, -2);
            this.lexemes_table.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lexemes_table.Name = "lexemes_table";
            this.lexemes_table.Size = new System.Drawing.Size(504, 329);
            this.lexemes_table.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "LEXEMES";
            this.Column1.Name = "Column1";
            this.Column1.Width = 222;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "TOKEN TAGS";
            this.Column2.Name = "Column2";
            this.Column2.Width = 223;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.symbol_table);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Size = new System.Drawing.Size(484, 330);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // symbol_table
            // 
            this.symbol_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symbol_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5});
            this.symbol_table.Location = new System.Drawing.Point(-4, -4);
            this.symbol_table.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.symbol_table.Name = "symbol_table";
            this.symbol_table.Size = new System.Drawing.Size(488, 338);
            this.symbol_table.TabIndex = 0;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "VARIABLE";
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "TYPE";
            this.Column4.Name = "Column4";
            this.Column4.Width = 145;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "VALUE";
            this.Column5.Name = "Column5";
            this.Column5.Width = 150;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fILEToolStripMenuItem,
            this.aBOUTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1006, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oPENToolStripMenuItem,
            this.eXITToolStripMenuItem});
            this.fILEToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(40, 21);
            this.fILEToolStripMenuItem.Text = "File";
            // 
            // oPENToolStripMenuItem
            // 
            this.oPENToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oPENToolStripMenuItem.Name = "oPENToolStripMenuItem";
            this.oPENToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.oPENToolStripMenuItem.Text = "Open";
            this.oPENToolStripMenuItem.Click += new System.EventHandler(this.oPENToolStripMenuItem_Click);
            // 
            // eXITToolStripMenuItem
            // 
            this.eXITToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
            this.eXITToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.eXITToolStripMenuItem.Text = "Exit";
            this.eXITToolStripMenuItem.Click += new System.EventHandler(this.eXITToolStripMenuItem_Click);
            // 
            // aBOUTToolStripMenuItem
            // 
            this.aBOUTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pROJECTDETAILSToolStripMenuItem,
            this.dEVELOPERSToolStripMenuItem});
            this.aBOUTToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aBOUTToolStripMenuItem.Name = "aBOUTToolStripMenuItem";
            this.aBOUTToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.aBOUTToolStripMenuItem.Text = "About";
            // 
            // pROJECTDETAILSToolStripMenuItem
            // 
            this.pROJECTDETAILSToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pROJECTDETAILSToolStripMenuItem.Name = "pROJECTDETAILSToolStripMenuItem";
            this.pROJECTDETAILSToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.pROJECTDETAILSToolStripMenuItem.Text = "Project Details";
            this.pROJECTDETAILSToolStripMenuItem.Click += new System.EventHandler(this.pROJECTDETAILSToolStripMenuItem_Click);
            // 
            // dEVELOPERSToolStripMenuItem
            // 
            this.dEVELOPERSToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dEVELOPERSToolStripMenuItem.Name = "dEVELOPERSToolStripMenuItem";
            this.dEVELOPERSToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.dEVELOPERSToolStripMenuItem.Text = "Developers";
            this.dEVELOPERSToolStripMenuItem.Click += new System.EventHandler(this.dEVELOPERSToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "CODE";
            // 
            // clear_button
            // 
            this.clear_button.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear_button.Location = new System.Drawing.Point(220, 344);
            this.clear_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(130, 45);
            this.clear_button.TabIndex = 4;
            this.clear_button.Text = "CLEAR";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // interpret_button
            // 
            this.interpret_button.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.interpret_button.Location = new System.Drawing.Point(358, 344);
            this.interpret_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.interpret_button.Name = "interpret_button";
            this.interpret_button.Size = new System.Drawing.Size(130, 45);
            this.interpret_button.TabIndex = 5;
            this.interpret_button.Text = "INTERPRET";
            this.interpret_button.UseVisualStyleBackColor = true;
            this.interpret_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // output_console
            // 
            this.output_console.BackColor = System.Drawing.SystemColors.Window;
            this.output_console.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.output_console.EnableAutoDragDrop = true;
            this.output_console.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output_console.Location = new System.Drawing.Point(14, 395);
            this.output_console.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.output_console.Name = "output_console";
            this.output_console.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.output_console.Size = new System.Drawing.Size(974, 120);
            this.output_console.TabIndex = 6;
            this.output_console.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 366);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "CONSOLE OUTPUT";
            // 
            // LineNumber
            // 
            this.LineNumber.AutoSize = true;
            this.LineNumber.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineNumber.Location = new System.Drawing.Point(23, 341);
            this.LineNumber.Name = "LineNumber";
            this.LineNumber.Size = new System.Drawing.Size(54, 20);
            this.LineNumber.TabIndex = 8;
            this.LineNumber.Text = "Line: 1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 529);
            this.Controls.Add(this.LineNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.output_console);
            this.Controls.Add(this.interpret_button);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.input_console);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Team BAH: LOL Code Interpreter";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lexemes_table)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.symbol_table)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox input_console;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView symbol_table;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView lexemes_table;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPENToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBOUTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pROJECTDETAILSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dEVELOPERSToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.Button interpret_button;
        private System.Windows.Forms.RichTextBox output_console;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LineNumber;
    }
}

