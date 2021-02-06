namespace WindowsFormsApplication2
{
    partial class 年初日経平均で正規化
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.Get_FileList = new System.Windows.Forms.Button();
            this.File_List_Box = new System.Windows.Forms.ListBox();
            this.Folder_place = new System.Windows.Forms.TextBox();
            this.Number_of_files = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.exit_button = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.progress_num = new System.Windows.Forms.TextBox();
            this.total_num = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Get_FileList
            // 
            this.Get_FileList.Location = new System.Drawing.Point(991, 21);
            this.Get_FileList.Name = "Get_FileList";
            this.Get_FileList.Size = new System.Drawing.Size(106, 21);
            this.Get_FileList.TabIndex = 1;
            this.Get_FileList.Text = "File List";
            this.Get_FileList.UseVisualStyleBackColor = true;
            this.Get_FileList.Click += new System.EventHandler(this.Get_file_List_Button);
            // 
            // File_List_Box
            // 
            this.File_List_Box.FormattingEnabled = true;
            this.File_List_Box.ItemHeight = 12;
            this.File_List_Box.Location = new System.Drawing.Point(8, 91);
            this.File_List_Box.Name = "File_List_Box";
            this.File_List_Box.Size = new System.Drawing.Size(1089, 196);
            this.File_List_Box.TabIndex = 2;
            this.File_List_Box.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Folder_place
            // 
            this.Folder_place.Location = new System.Drawing.Point(12, 23);
            this.Folder_place.Name = "Folder_place";
            this.Folder_place.Size = new System.Drawing.Size(958, 19);
            this.Folder_place.TabIndex = 3;
            this.Folder_place.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Number_of_files
            // 
            this.Number_of_files.Location = new System.Drawing.Point(118, 48);
            this.Number_of_files.Name = "Number_of_files";
            this.Number_of_files.Size = new System.Drawing.Size(105, 19);
            this.Number_of_files.TabIndex = 4;
            this.Number_of_files.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(15, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(97, 12);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "ファイル数：";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // exit_button
            // 
            this.exit_button.Location = new System.Drawing.Point(991, 321);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(110, 23);
            this.exit_button.TabIndex = 6;
            this.exit_button.Text = "終了";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.Quit_Button);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(12, 73);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(97, 12);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "ファイルリスト：";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 321);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(782, 25);
            this.progressBar1.TabIndex = 10;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(10, 303);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(185, 12);
            this.textBox4.TabIndex = 11;
            this.textBox4.Text = "進捗：";
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // progress_num
            // 
            this.progress_num.Location = new System.Drawing.Point(819, 323);
            this.progress_num.Name = "progress_num";
            this.progress_num.Size = new System.Drawing.Size(62, 19);
            this.progress_num.TabIndex = 12;
            this.progress_num.TextChanged += new System.EventHandler(this.progress_num_TextChanged);
            // 
            // total_num
            // 
            this.total_num.Location = new System.Drawing.Point(908, 323);
            this.total_num.Name = "total_num";
            this.total_num.Size = new System.Drawing.Size(62, 19);
            this.total_num.TabIndex = 13;
            this.total_num.TextChanged += new System.EventHandler(this.total_num_TextChanged);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Location = new System.Drawing.Point(887, 328);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(15, 12);
            this.textBox7.TabIndex = 14;
            this.textBox7.Text = "/";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // 年初日経平均で正規化
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 366);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.total_num);
            this.Controls.Add(this.progress_num);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Number_of_files);
            this.Controls.Add(this.Folder_place);
            this.Controls.Add(this.File_List_Box);
            this.Controls.Add(this.Get_FileList);
            this.Name = "年初日経平均で正規化";
            this.Text = "年初＆日経平均で正規化";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Get_FileList;
        private System.Windows.Forms.ListBox File_List_Box;
        private System.Windows.Forms.TextBox Folder_place;
        private System.Windows.Forms.TextBox Number_of_files;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox progress_num;
        private System.Windows.Forms.TextBox total_num;
        private System.Windows.Forms.TextBox textBox7;
    }
}

