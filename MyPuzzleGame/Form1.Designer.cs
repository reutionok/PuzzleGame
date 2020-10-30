namespace MyPuzzleGame
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
            this.puzzleBoard = new System.Windows.Forms.GroupBox();
            this.imagePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.previewPB = new System.Windows.Forms.PictureBox();
            this.playMode = new System.Windows.Forms.GroupBox();
            this.btnCut = new System.Windows.Forms.Button();
            this.radioButton9x9 = new System.Windows.Forms.RadioButton();
            this.radioButton6x6 = new System.Windows.Forms.RadioButton();
            this.radioButton4x4 = new System.Windows.Forms.RadioButton();
            this.btnSolution = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.previewPB)).BeginInit();
            this.playMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // puzzleBoard
            // 
            this.puzzleBoard.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.puzzleBoard.Location = new System.Drawing.Point(13, 13);
            this.puzzleBoard.Name = "puzzleBoard";
            this.puzzleBoard.Size = new System.Drawing.Size(450, 450);
            this.puzzleBoard.TabIndex = 0;
            this.puzzleBoard.TabStop = false;
            this.puzzleBoard.Text = "Puzzle board";
            // 
            // imagePath
            // 
            this.imagePath.Location = new System.Drawing.Point(102, 477);
            this.imagePath.Name = "imagePath";
            this.imagePath.Size = new System.Drawing.Size(299, 20);
            this.imagePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 480);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose image";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(419, 477);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // previewPB
            // 
            this.previewPB.BackColor = System.Drawing.SystemColors.ControlLight;
            this.previewPB.Location = new System.Drawing.Point(505, 13);
            this.previewPB.Name = "previewPB";
            this.previewPB.Size = new System.Drawing.Size(122, 143);
            this.previewPB.TabIndex = 4;
            this.previewPB.TabStop = false;
            // 
            // playMode
            // 
            this.playMode.Controls.Add(this.btnSolution);
            this.playMode.Controls.Add(this.btnCut);
            this.playMode.Controls.Add(this.radioButton9x9);
            this.playMode.Controls.Add(this.radioButton6x6);
            this.playMode.Controls.Add(this.radioButton4x4);
            this.playMode.Location = new System.Drawing.Point(505, 176);
            this.playMode.Name = "playMode";
            this.playMode.Size = new System.Drawing.Size(122, 191);
            this.playMode.TabIndex = 5;
            this.playMode.TabStop = false;
            this.playMode.Text = "Play Mode";
            // 
            // btnCut
            // 
            this.btnCut.Enabled = false;
            this.btnCut.Location = new System.Drawing.Point(21, 100);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(75, 23);
            this.btnCut.TabIndex = 3;
            this.btnCut.Text = "Cut";
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // radioButton9x9
            // 
            this.radioButton9x9.AutoSize = true;
            this.radioButton9x9.Location = new System.Drawing.Point(35, 67);
            this.radioButton9x9.Name = "radioButton9x9";
            this.radioButton9x9.Size = new System.Drawing.Size(42, 17);
            this.radioButton9x9.TabIndex = 2;
            this.radioButton9x9.TabStop = true;
            this.radioButton9x9.Text = "9x9";
            this.radioButton9x9.UseVisualStyleBackColor = true;
            // 
            // radioButton6x6
            // 
            this.radioButton6x6.AutoSize = true;
            this.radioButton6x6.Location = new System.Drawing.Point(35, 44);
            this.radioButton6x6.Name = "radioButton6x6";
            this.radioButton6x6.Size = new System.Drawing.Size(42, 17);
            this.radioButton6x6.TabIndex = 1;
            this.radioButton6x6.TabStop = true;
            this.radioButton6x6.Text = "6x6";
            this.radioButton6x6.UseVisualStyleBackColor = true;
            // 
            // radioButton4x4
            // 
            this.radioButton4x4.AutoSize = true;
            this.radioButton4x4.Location = new System.Drawing.Point(35, 21);
            this.radioButton4x4.Name = "radioButton4x4";
            this.radioButton4x4.Size = new System.Drawing.Size(42, 17);
            this.radioButton4x4.TabIndex = 0;
            this.radioButton4x4.TabStop = true;
            this.radioButton4x4.Text = "4x4";
            this.radioButton4x4.UseVisualStyleBackColor = true;
            // 
            // btnSolution
            // 
            this.btnSolution.Enabled = false;
            this.btnSolution.Location = new System.Drawing.Point(19, 147);
            this.btnSolution.Name = "btnSolution";
            this.btnSolution.Size = new System.Drawing.Size(79, 28);
            this.btnSolution.TabIndex = 6;
            this.btnSolution.Text = "Solution";
            this.btnSolution.UseVisualStyleBackColor = true;
            this.btnSolution.Click += new System.EventHandler(this.btnSolution_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 509);
            this.Controls.Add(this.playMode);
            this.Controls.Add(this.previewPB);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imagePath);
            this.Controls.Add(this.puzzleBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Puzzle";
            ((System.ComponentModel.ISupportInitialize)(this.previewPB)).EndInit();
            this.playMode.ResumeLayout(false);
            this.playMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox puzzleBoard;
        private System.Windows.Forms.TextBox imagePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.PictureBox previewPB;
        private System.Windows.Forms.GroupBox playMode;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.RadioButton radioButton9x9;
        private System.Windows.Forms.RadioButton radioButton6x6;
        private System.Windows.Forms.RadioButton radioButton4x4;
        private System.Windows.Forms.Button btnSolution;
    }
}

