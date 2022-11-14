namespace ImageEdgeDetection
{
    partial class MainForm
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
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.btnOpenOriginal = new System.Windows.Forms.Button();
            this.btnSaveNewImage = new System.Windows.Forms.Button();
            this.buttonApplyFilters = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxXFilter = new System.Windows.Forms.ListBox();
            this.listBoxYFilter = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.labelErrors = new System.Windows.Forms.Label();
            this.button21 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBarThreshold = new System.Windows.Forms.TrackBar();
            this.button13 = new System.Windows.Forms.Button();
            this.btnNoFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.Silver;
            this.pictureBoxPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPreview.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBoxPreview.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(600, 600);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 13;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Click += new System.EventHandler(this.picPreview_Click);
            // 
            // btnOpenOriginal
            // 
            this.btnOpenOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenOriginal.Location = new System.Drawing.Point(12, 618);
            this.btnOpenOriginal.Name = "btnOpenOriginal";
            this.btnOpenOriginal.Size = new System.Drawing.Size(150, 46);
            this.btnOpenOriginal.TabIndex = 15;
            this.btnOpenOriginal.Text = "Load Image";
            this.btnOpenOriginal.UseVisualStyleBackColor = true;
            this.btnOpenOriginal.Click += new System.EventHandler(this.btnOpenOriginal_Click);
            // 
            // btnSaveNewImage
            // 
            this.btnSaveNewImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNewImage.Location = new System.Drawing.Point(600, 946);
            this.btnSaveNewImage.Name = "btnSaveNewImage";
            this.btnSaveNewImage.Size = new System.Drawing.Size(150, 46);
            this.btnSaveNewImage.TabIndex = 16;
            this.btnSaveNewImage.Text = "Save Image";
            this.btnSaveNewImage.UseVisualStyleBackColor = true;
            this.btnSaveNewImage.Click += new System.EventHandler(this.btnSaveNewImage_Click);
            // 
            // buttonApplyFilters
            // 
            this.buttonApplyFilters.Location = new System.Drawing.Point(1202, 725);
            this.buttonApplyFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonApplyFilters.Name = "buttonApplyFilters";
            this.buttonApplyFilters.Size = new System.Drawing.Size(112, 35);
            this.buttonApplyFilters.TabIndex = 25;
            this.buttonApplyFilters.Text = "Apply Filters";
            this.buttonApplyFilters.UseVisualStyleBackColor = true;
            this.buttonApplyFilters.Click += new System.EventHandler(this.buttonApplyFilters_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(969, 643);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "Y Filter";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(755, 643);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "X Filter";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // listBoxXFilter
            // 
            this.listBoxXFilter.FormattingEnabled = true;
            this.listBoxXFilter.ItemHeight = 20;
            this.listBoxXFilter.Items.AddRange(new object[] {
            "",
            "Laplacian3x3",
            "Laplacian5x5",
            "Sobel3x3Horizontal",
            "Sobel3x3Vertical",
            "Prewitt3x3Horizontal",
            "Prewitt3x3Vertical",
            "Kirsch3x3Horizontal",
            "Kirsch3x3Vertical"});
            this.listBoxXFilter.Location = new System.Drawing.Point(755, 672);
            this.listBoxXFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxXFilter.Name = "listBoxXFilter";
            this.listBoxXFilter.Size = new System.Drawing.Size(178, 144);
            this.listBoxXFilter.TabIndex = 22;
            this.listBoxXFilter.SelectedIndexChanged += new System.EventHandler(this.listBoxXFilter_SelectedIndexChanged);
            // 
            // listBoxYFilter
            // 
            this.listBoxYFilter.FormattingEnabled = true;
            this.listBoxYFilter.ItemHeight = 20;
            this.listBoxYFilter.Items.AddRange(new object[] {
            "",
            "Laplacian3x3",
            "Laplacian5x5",
            "Sobel3x3Horizontal",
            "Sobel3x3Vertical",
            "Prewitt3x3Horizontal",
            "Prewitt3x3Vertical",
            "Kirsch3x3Horizontal",
            "Kirsch3x3Vertical"});
            this.listBoxYFilter.Location = new System.Drawing.Point(974, 672);
            this.listBoxYFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxYFilter.Name = "listBoxYFilter";
            this.listBoxYFilter.Size = new System.Drawing.Size(178, 144);
            this.listBoxYFilter.TabIndex = 21;
            this.listBoxYFilter.SelectedIndexChanged += new System.EventHandler(this.listBoxYFilter_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(666, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Result Image";
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.Location = new System.Drawing.Point(659, 37);
            this.pictureBoxResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(684, 601);
            this.pictureBoxResult.TabIndex = 26;
            this.pictureBoxResult.TabStop = false;
            // 
            // labelErrors
            // 
            this.labelErrors.AutoSize = true;
            this.labelErrors.Location = new System.Drawing.Point(957, 860);
            this.labelErrors.Name = "labelErrors";
            this.labelErrors.Size = new System.Drawing.Size(0, 20);
            this.labelErrors.TabIndex = 28;
            this.labelErrors.Click += new System.EventHandler(this.label3_Click);
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(190, 688);
            this.button21.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(147, 57);
            this.button21.TabIndex = 47;
            this.button21.Text = "Mega Filter Pink";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(190, 623);
            this.button10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(147, 46);
            this.button10.TabIndex = 46;
            this.button10.Text = "Night Filter";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(638, 899);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 49;
            this.label6.Text = "Threshold";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // trackBarThreshold
            // 
            this.trackBarThreshold.LargeChange = 10;
            this.trackBarThreshold.Location = new System.Drawing.Point(728, 885);
            this.trackBarThreshold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trackBarThreshold.Maximum = 255;
            this.trackBarThreshold.Name = "trackBarThreshold";
            this.trackBarThreshold.Size = new System.Drawing.Size(615, 69);
            this.trackBarThreshold.TabIndex = 48;
            this.trackBarThreshold.Value = 100;
            this.trackBarThreshold.Scroll += new System.EventHandler(this.trackBarThreshold_Scroll);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(500, 628);
            this.button13.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(112, 35);
            this.button13.TabIndex = 50;
            this.button13.Text = "back";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // btnNoFilter
            // 
            this.btnNoFilter.Location = new System.Drawing.Point(190, 770);
            this.btnNoFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNoFilter.Name = "btnNoFilter";
            this.btnNoFilter.Size = new System.Drawing.Size(147, 46);
            this.btnNoFilter.TabIndex = 51;
            this.btnNoFilter.Text = "No Filter";
            this.btnNoFilter.UseVisualStyleBackColor = true;
            this.btnNoFilter.Click += new System.EventHandler(this.button17_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1356, 1036);
            this.Controls.Add(this.btnNoFilter);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trackBarThreshold);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.labelErrors);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBoxResult);
            this.Controls.Add(this.buttonApplyFilters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxXFilter);
            this.Controls.Add(this.listBoxYFilter);
            this.Controls.Add(this.btnSaveNewImage);
            this.Controls.Add(this.btnOpenOriginal);
            this.Controls.Add(this.pictureBoxPreview);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Edge Detection";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button btnOpenOriginal;
        private System.Windows.Forms.Button btnSaveNewImage;
        private System.Windows.Forms.Button buttonApplyFilters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxXFilter;
        private System.Windows.Forms.ListBox listBoxYFilter;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.PictureBox pictureBoxResult;
        private System.Windows.Forms.Label labelErrors;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBarThreshold;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button btnNoFilter;
    }
}

