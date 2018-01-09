namespace NSGA2_project
{
    partial class Window
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.xCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.yCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.f1Col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.f2Col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_minX = new System.Windows.Forms.TextBox();
            this.tb_maxX = new System.Windows.Forms.TextBox();
            this.tb_minY = new System.Windows.Forms.TextBox();
            this.tb_maxY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_populationSize = new System.Windows.Forms.TextBox();
            this.tb_noOfIterations = new System.Windows.Forms.TextBox();
            this.tb_crossoverRate = new System.Windows.Forms.TextBox();
            this.tb_mutationRate = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xCol,
            this.yCol,
            this.f1Col,
            this.f2Col});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(12, 181);
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(252, 287);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // xCol
            // 
            this.xCol.Text = "X";
            // 
            // yCol
            // 
            this.yCol.Text = "Y";
            // 
            // f1Col
            // 
            this.f1Col.Text = "f1";
            // 
            // f2Col
            // 
            this.f2Col.Text = "f2";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(282, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 540);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 503);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(251, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Generate new population";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 474);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Run";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "min X :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "min Y :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "max X :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(158, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "max Y :";
            // 
            // tb_minX
            // 
            this.tb_minX.Location = new System.Drawing.Point(53, 12);
            this.tb_minX.Margin = new System.Windows.Forms.Padding(2);
            this.tb_minX.Name = "tb_minX";
            this.tb_minX.Size = new System.Drawing.Size(67, 20);
            this.tb_minX.TabIndex = 10;
            this.tb_minX.Text = "0";
            // 
            // tb_maxX
            // 
            this.tb_maxX.Location = new System.Drawing.Point(201, 15);
            this.tb_maxX.Margin = new System.Windows.Forms.Padding(2);
            this.tb_maxX.Name = "tb_maxX";
            this.tb_maxX.Size = new System.Drawing.Size(61, 20);
            this.tb_maxX.TabIndex = 11;
            this.tb_maxX.Text = "20";
            // 
            // tb_minY
            // 
            this.tb_minY.Location = new System.Drawing.Point(53, 36);
            this.tb_minY.Margin = new System.Windows.Forms.Padding(2);
            this.tb_minY.Name = "tb_minY";
            this.tb_minY.Size = new System.Drawing.Size(67, 20);
            this.tb_minY.TabIndex = 12;
            this.tb_minY.Text = "0";
            // 
            // tb_maxY
            // 
            this.tb_maxY.Location = new System.Drawing.Point(201, 38);
            this.tb_maxY.Margin = new System.Windows.Forms.Padding(2);
            this.tb_maxY.Name = "tb_maxY";
            this.tb_maxY.Size = new System.Drawing.Size(61, 20);
            this.tb_maxY.TabIndex = 13;
            this.tb_maxY.Text = "20";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 65);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Population size :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "No. of iterations :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 128);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Crossover rate :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 158);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Mutation rate :";
            // 
            // tb_populationSize
            // 
            this.tb_populationSize.Location = new System.Drawing.Point(119, 65);
            this.tb_populationSize.Margin = new System.Windows.Forms.Padding(2);
            this.tb_populationSize.Name = "tb_populationSize";
            this.tb_populationSize.Size = new System.Drawing.Size(67, 20);
            this.tb_populationSize.TabIndex = 18;
            this.tb_populationSize.Text = "50";
            // 
            // tb_noOfIterations
            // 
            this.tb_noOfIterations.Location = new System.Drawing.Point(119, 97);
            this.tb_noOfIterations.Margin = new System.Windows.Forms.Padding(2);
            this.tb_noOfIterations.Name = "tb_noOfIterations";
            this.tb_noOfIterations.Size = new System.Drawing.Size(67, 20);
            this.tb_noOfIterations.TabIndex = 19;
            this.tb_noOfIterations.Text = "50";
            // 
            // tb_crossoverRate
            // 
            this.tb_crossoverRate.Location = new System.Drawing.Point(119, 128);
            this.tb_crossoverRate.Margin = new System.Windows.Forms.Padding(2);
            this.tb_crossoverRate.Name = "tb_crossoverRate";
            this.tb_crossoverRate.Size = new System.Drawing.Size(67, 20);
            this.tb_crossoverRate.TabIndex = 20;
            this.tb_crossoverRate.Text = "0.8";
            // 
            // tb_mutationRate
            // 
            this.tb_mutationRate.Location = new System.Drawing.Point(119, 158);
            this.tb_mutationRate.Margin = new System.Windows.Forms.Padding(2);
            this.tb_mutationRate.Name = "tb_mutationRate";
            this.tb_mutationRate.Size = new System.Drawing.Size(67, 20);
            this.tb_mutationRate.TabIndex = 21;
            this.tb_mutationRate.Text = "0.01";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(145, 474);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(201, 61);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(61, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "repaint";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(13, 529);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(249, 23);
            this.button5.TabIndex = 24;
            this.button5.Text = "Story behind";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 570);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tb_mutationRate);
            this.Controls.Add(this.tb_crossoverRate);
            this.Controls.Add(this.tb_noOfIterations);
            this.Controls.Add(this.tb_populationSize);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_maxY);
            this.Controls.Add(this.tb_minY);
            this.Controls.Add(this.tb_maxX);
            this.Controls.Add(this.tb_minX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "NSGA 2 demo - Dumitru-Daniel Vecliuc and Petru Dimitriu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader yCol;
        private System.Windows.Forms.ColumnHeader f1Col;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader xCol;
        private System.Windows.Forms.ColumnHeader f2Col;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_minX;
        private System.Windows.Forms.TextBox tb_maxX;
        private System.Windows.Forms.TextBox tb_minY;
        private System.Windows.Forms.TextBox tb_maxY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_populationSize;
        private System.Windows.Forms.TextBox tb_noOfIterations;
        private System.Windows.Forms.TextBox tb_crossoverRate;
        private System.Windows.Forms.TextBox tb_mutationRate;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

