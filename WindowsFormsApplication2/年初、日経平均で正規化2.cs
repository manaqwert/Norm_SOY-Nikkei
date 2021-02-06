using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class 年初日経平均で正規化 : Form
    {
        public 年初日経平均で正規化()
        {
            InitializeComponent();
 
        }
        
        private void Get_file_List_Button(object sender, EventArgs e)
        {
            //
            //ファイルを開く
            //
            string dname;
            dname = opening_file();

            int progress_status = 0;

            
            //
            //縦軸日付、横軸銘柄の配列定義
            //
            string[,] in_data_file = new string[300, 5000];
            string[,] out_data_file = new string[300, 5000];
            double[,] in_data_file2 = new double[300, 5000];
            double[,] out_data_file2 = new double[300, 5000];

            string[] max_date = new string[5000];
            double[] max_value = new double[5000];
            string[] min_date = new string[5000];
            double[] min_value = new double[5000];
            string[] max_value2 = new string[5000];
            string[] min_value2 = new string[5000];

            double[] max_min_ratio = new double[5000];
            string[] max_min_ratio2 = new string[5000];
            int[] max_No = new int[300];
            int[] min_No = new int[300];

            int days2 = 0;
            
            int num_stock = 0;

            progress_status = 5;


            //
            //ここからファイルの中身読み込み
            //
            in_data_file = read_file( dname );

            num_stock = read_file2( dname, 1 );

            days2 = read_file2( dname, 2 );
            
            //（デバッグ用）リストボックスに書き出し
            //File_List_Box.Items.Clear();
            //for (days = 0; days < 300; days++)
            //{
            //    File_List_Box.Items.Add(num_stock + "\r\n");
            //
            //    File_List_Box.Items.Add(in_data_file[days, 0] + "\r\n");
            //}
            //
            //in_data_fileの読み込みはOK
            //

            progress_status = 20;



            //
            //ここから正規化計算
            //
            out_data_file = compute_normalize3( in_data_file, num_stock, days2 );
            max_date = compute_normalize4(in_data_file, num_stock, days2, 1);
            min_date = compute_normalize4(in_data_file, num_stock, days2, 2);
            max_value2 = compute_normalize4(in_data_file, num_stock, days2, 3);
            min_value2 = compute_normalize4(in_data_file, num_stock, days2, 4);


            //（デバッグ用）リストボックスに書き出し
            //File_List_Box.Items.Clear();
            //File_List_Box.Items.Add(num_stock + "\r\n");
            //File_List_Box.Items.Add(days2 + "\r\n");
            //for (days = 0; days < 300; days++)
            //{
            //    File_List_Box.Items.Add(out_data_file[days, 0] + "\r\n");
            //}
            //
            //out_data_fileの読み込みはOK
            //

            progress_status = 60;



            //
            //ここからファイル書き出し
            //
            write_file( dname, out_data_file, max_date, min_date, max_value2, min_value2, days2, num_stock );

            progress_status = 80;

            //
            //プログレスバーの設置
            //
            progress_status = 100;

            progressBar1.Increment(progress_status);
            
        }

        //
        //サブルーチン定義
        //

        string opening_file()
        {
            //フォルダ選択ダイアログの初期値指定
            //string file_sel.SelectedPath = @"C:\Data\OneDrive\stock";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();

            string folder_name = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);

            //dnameにテキストボックスのフォルダ名を入れる
            string dname = openFileDialog1.FileName;

            //テキストボックスに選択したファイル名を表示する
            Folder_place.Text = dname;

            if (System.IO.File.Exists(dname) == false)
            {
                MessageBox.Show(dname + "が見つかりません。", "通知");
                return dname;
            }

            return dname;

        }

        public string[,] read_file(string dname)
        {
            int days = 1;
            int days2 = 1;
            int stock_num = 0;
            string[,] in_data_file = new string[300, 5000];

            System.IO.StreamReader sr = new System.IO.StreamReader(dname, Encoding.GetEncoding("Shift_JIS"));

            for (days = 0; days <= 300; days++)
            {
                if (sr.EndOfStream == false)
                {
                    //ファイル一行読み込み、カンマ区切り毎にfield[]に格納
                    string file_contents_2 = sr.ReadLine();

                    string[] fields = file_contents_2.Split(',');

                    //
                    //列方向に文字があるだけ読んで配列に格納。要素数はfileds.Length
                    //
                    for (stock_num = 0; stock_num < fields.Length; stock_num++)
                    {
                        in_data_file[days, stock_num] = fields[stock_num];
                    }
                }
                else
                {
                    //最後の行まで読み終わった場合の終了処置
                    //file_contents_Box.Items.Add(days + ", " + stock_num + ", " + "EndOfStream");
                    days2 = days;
                    days = 300;
                }
            }

            //
            //ここまでファイル内容読み込み＆配列格納
            //days = 0は銘柄番号
            //days = 1は銘柄名称
            //days = 2が年初
            //
            //stock_num = 0は日付
            //stock_num = 1からは銘柄の終値
            //

            return in_data_file;
        }

        public int read_file2(string dname, int output)
        {
            int days = 1;
            int days2 = 1;
            int stock_num = 0;
            int num_stock = 0;
            string[,] in_data_file = new string[300, 5000];

            System.IO.StreamReader sr = new System.IO.StreamReader(dname, Encoding.GetEncoding("Shift_JIS"));

            for (days = 0; days <= 300; days++)
            {
                if (sr.EndOfStream == false)
                {
                    //ファイル一行読み込み、カンマ区切り毎にfield[]に格納
                    string file_contents_2 = sr.ReadLine();

                    string[] fields = file_contents_2.Split(',');

                    //
                    //列方向に文字があるだけ読んで配列に格納。要素数はfileds.Length
                    //
                    for (stock_num = 0; stock_num < fields.Length; stock_num++)
                    {
                        in_data_file[days, stock_num] = fields[stock_num];
                    }
                    num_stock = fields.Length;
                }
                else
                {
                    //最後の行まで読み終わった場合の終了処置
                    //file_contents_Box.Items.Add(days + ", " + stock_num + ", " + "EndOfStream");
                    days2 = days;
                    days = 300;
                }
            }

            //
            //ここまでファイル内容読み込み＆配列格納
            //days = 0は銘柄番号
            //days = 1は銘柄名称
            //days = 2が年初
            //
            //stock_num = 0は日付
            //stock_num = 1からは銘柄の終値
            //
            if (output == 1)
            {
                return num_stock;
            }
            else if (output == 2)
            {
                return days2;
            }
            else
            {
                return num_stock;
            }

        }
          
        public string[,] compute_normalize3( string[,] in_data_file, int num_stock, int days2 )
        {
            int stock_num = 0;

            string[,] out_data_file = new string[300, 5000];
            double[,] in_data_file2 = new double[300, 5000];
            double[,] out_data_file2 = new double[300, 5000];

            double[] max_min_ratio = new double[5000];
            string[] max_min_ratio2 = new string[5000];
            int[] max_No = new int[300];
            int[] min_No = new int[300];

            int days3 = 0;

            string dt;

            dt = DateTime.Now.ToString($"[yyyyMMddMHHmmss]");

            //（デバッグ用）リストボックスに書き出し
            //File_List_Box.Items.Clear();
            //File_List_Box.Items.Add(num_stock + "\r\n");
            //File_List_Box.Items.Add(days2 + "\r\n");
            //for (days = 0; days < 300; days++)
            //{
            //    File_List_Box.Items.Add(in_data_file[days, 1] + "\r\n");
            //}
            //
            //in_data_file、num_stockの読み込みはOK
            //


            //
            //日々の日経平均を正規化
            //

            for (stock_num = 0; stock_num < num_stock; stock_num++)
            {
                //
                //MAX値、MIN値の初期化
                //

                for (days3 = 0; days3 < days2; days3++)
                {
                    if (in_data_file[days3, 1] == "")
                    {
                        //int days3_old = days3 - 1;
                        out_data_file2[days3, stock_num] = out_data_file2[days3 - 1, stock_num];
                    }
                    else
                    {
                        //File_List_Box.Items.Add("308 in_data_file2[days3, 1] = " + in_data_file[days3, 1]);

                        if (stock_num >= 1)
                        {
                            if (days3 >= 2)
                            {
                                //
                                //stringの配列をdoubleの配列に変換
                                //ただし、銘柄名などdoubleに入らないものもあるので、個々だけで変換
                                //
                                if (in_data_file[days3, stock_num] == "")
                                {
                                    out_data_file[days3, stock_num] = in_data_file[days3, stock_num];
                                }
                                else
                                {
                                    in_data_file2[days3, stock_num] = Convert.ToDouble(in_data_file[days3, stock_num]);

                                    if (in_data_file2[2, stock_num] != 0)
                                    {
                                        //
                                        //年初の値及び日経平均で正規化計算
                                        //
                                        out_data_file2[days3, stock_num] = (in_data_file2[days3, stock_num] / in_data_file2[days3, 1]) / (in_data_file2[2, stock_num] / in_data_file2[2, 1]) * 100;

                                        //
                                        //正規化計算した結果をstringに変換
                                        //
                                        out_data_file[days3, stock_num] = Convert.ToString(out_data_file2[days3, stock_num]);
                                    }
                                    else
                                    {
                                        out_data_file[days3, stock_num] = "";
                                    }
                                }

                            }
                            else
                            {
                                out_data_file[days3, stock_num] = in_data_file[days3, stock_num];
                            }

                        }
                        else
                        {
                            out_data_file[days3, stock_num] = in_data_file[days3, stock_num];
                        }
                    }
                }
            }

            //（デバッグ用）リストボックスに書き出し
            //File_List_Box.Items.Clear();
            //File_List_Box.Items.Add(num_stock + "\r\n");
            //File_List_Box.Items.Add(days2 + "\r\n");

            //for (days = 0; days < 300; days++)
            //{
            //    File_List_Box.Items.Add(out_data_file[days, 0] + "\r\n");
            //}
            //
            //ここでのnum_stockの書き出しはOK
            //out_data_fileの書き出しはNG
            //

            return out_data_file;

        }

        public string[] compute_normalize4(string[,] in_data_file, int num_stock, int days2, int output)
        {
            int stock_num = 0;

            string[,] out_data_file = new string[300, 5000];
            double[,] in_data_file2 = new double[300, 5000];
            double[,] out_data_file2 = new double[300, 5000];

            string[] max_date = new string[5000];
            double[] max_value = new double[5000];
            string[] min_date = new string[5000];
            double[] min_value = new double[5000];
            string[] max_value2 = new string[5000];
            string[] min_value2 = new string[5000];

            double[] max_min_ratio = new double[5000];
            string[] max_min_ratio2 = new string[5000];
            int[] max_No = new int[300];
            int[] min_No = new int[300];

            int days3 = 0;


            //（デバッグ用）リストボックスに書き出し
            //File_List_Box.Items.Clear();
            //File_List_Box.Items.Add(num_stock + "\r\n");
            //File_List_Box.Items.Add(days2 + "\r\n");
            //for (days = 0; days < 300; days++)
            //{
            //    File_List_Box.Items.Add(in_data_file[days, 1] + "\r\n");
            //}
            //
            //in_data_file、num_stockの読み込みはOK
            //


            //
            //日々の日経平均を正規化
            //

            for (stock_num = 0; stock_num < num_stock; stock_num++)
            {
                //
                //MAX値、MIN値の初期化
                //
                max_value[stock_num] = 100;
                min_value[stock_num] = 100;
                max_No[0] = 0;
                min_No[0] = 0;


                for (days3 = 0; days3 < days2; days3++)
                {
                    if (in_data_file[days3, 1] == "")
                    {
                        //int days3_old = days3 - 1;
                        out_data_file2[days3, stock_num] = out_data_file2[days3 - 1, stock_num];
                    }
                    else
                    {
                        if (stock_num >= 1)
                        {
                            if (days3 >= 2)
                            {
                                //
                                //stringの配列をdoubleの配列に変換
                                //ただし、銘柄名などdoubleに入らないものもあるので、個々だけで変換
                                //
                                if (in_data_file[days3, stock_num] == "")
                                {
                                    out_data_file[days3, stock_num] = in_data_file[days3, stock_num];
                                }
                                else
                                {
                                    in_data_file2[days3, stock_num] = Convert.ToDouble(in_data_file[days3, stock_num]);
                                    //in_data_file2[2, stock_num] = double.Parse(in_data_file[2, stock_num]);

                                    if (in_data_file2[2, stock_num] != 0)
                                    {
                                        //
                                        //年初の値及び日経平均で正規化計算
                                        //
                                        out_data_file2[days3, stock_num] = (in_data_file2[days3, stock_num] / in_data_file2[days3, 1]) / (in_data_file2[2, stock_num] / in_data_file2[2, 1]) * 100;

                                        //
                                        //最大値探索
                                        //
                                        if (out_data_file2[days3, stock_num] >= max_value[stock_num])
                                        {
                                            max_value[stock_num] = out_data_file2[days3, stock_num];
                                            max_date[stock_num] = in_data_file[days3, 0];
                                            //max_No[stock_num] = days3;
                                        }
                                        else
                                        {
                                            max_value[stock_num] = max_value[stock_num];
                                        }

                                        //
                                        //最小値探索
                                        //
                                        if (out_data_file2[days3, stock_num] <= min_value[stock_num])
                                        {
                                            min_value[stock_num] = out_data_file2[days3, stock_num];
                                            min_date[stock_num] = in_data_file[days3, 0];
                                            //min_No[stock_num] = days3;
                                        }
                                        else
                                        {
                                            min_value[stock_num] = min_value[stock_num];
                                        }


                                        //
                                        //正規化計算した結果をstringに変換
                                        //
                                        out_data_file[days3, stock_num] = Convert.ToString(out_data_file2[days3, stock_num]);
                                        //
                                        //最大値及び最小値を変換
                                        //
                                        max_value2[stock_num] = Convert.ToString(max_value[stock_num]);
                                        min_value2[stock_num] = Convert.ToString(min_value[stock_num]);
                                    }
                                    else
                                    {
                                        out_data_file[days3, stock_num] = "";
                                    }
                                }

                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }

                }
            }

            //（デバッグ用）リストボックスに書き出し
            //File_List_Box.Items.Clear();
            //File_List_Box.Items.Add(num_stock + "\r\n");
            //File_List_Box.Items.Add(days2 + "\r\n");

            //for (days = 0; days < 300; days++)
            //{
            //    File_List_Box.Items.Add(out_data_file[days, 0] + "\r\n");
            //}
            //
            //ここでのnum_stockの書き出しはOK
            //out_data_fileの書き出しはNG
            //
            if ( output == 1 )
            {
                return max_date;
            }
            else if ( output == 2 )
            {
                return min_date;

            }
            else if ( output == 3 )
            {
                return max_value2;

            }
            else if ( output == 4 )
            {
                return min_value2;
            }
            else
            {
                return max_date;
            }

        }

        public void write_file(string dname, string[,] out_data_file, string[] max_date, string[] min_date, string[] max_value2, string[] min_value2, int days2, int num_stock )
        {
            int stock_num = 0;
            //int num_stock = 0;
            //string[,] in_data_file = new string[300, 5000];
            //string[,] out_data_file = new string[300, 5000];

            //string[] max_date = new string[5000];
            double[] max_date2 = new double[5000];
            double[] max_value = new double[5000];
            //string[] min_date = new string[5000];
            double[] min_date2 = new double[5000];
            double[] min_value = new double[5000];
            //string[] max_value2 = new string[5000];
            //string[] min_value2 = new string[5000];

            double[] max_min_ratio = new double[5000];
            string[] max_min_ratio2 = new string[5000];
            int[] max_No = new int[300];
            int[] min_No = new int[300];

            int days = 0;
            //int days2 = 0;

            //int num_stock = 0;

            //（デバッグ用）リストボックスに書き出し
            //File_List_Box.Items.Clear();
            //File_List_Box.Items.Add(num_stock + "\r\n");
            //File_List_Box.Items.Add(days2 + "\r\n");
            //for (days = 0; days < 300; days++)
            //{
            //    File_List_Box.Items.Add(out_data_file[days, 0] + "\r\n");
            //}
            //
            //out_data_fileの読み込みはOK
            //



            //
            //新たなファイルを用意する前に既存ファイル削除
            //


            //System.IO.File.Delete(@"M:\Manabu MyDocuments\ドキュメント\stock\test_mod.csv");
            System.IO.File.Delete(@dname + "_" + "_NikkeiNorm.csv");

            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            System.IO.StreamWriter writer = new System.IO.StreamWriter(@dname + "_" + "_NikkeiNorm.csv", true, sjisEnc);


            //
            //正規化した配列をファイル書き出し
            //

            //
            //銘柄番号、銘柄名の書き出し
            //
            for (days = 0; days <= 0; days++)
            {
                for (stock_num = 0; stock_num <= num_stock; stock_num++)
                {
                    writer.Write(out_data_file[days, stock_num] + ",");
                }
                writer.Write("\r\n");
            }


            //
            //銘柄番号、銘柄名の書き出し
            //
            for (days = 1; days <= 1; days++)
            {
                for (stock_num = 0; stock_num <= num_stock; stock_num++)
                {
                    writer.Write(out_data_file[days, stock_num] + ",");
                }
                writer.Write("\r\n");
            }

            //
            //MAX日付の書き出し
            //
            for (stock_num = 0; stock_num <= num_stock; stock_num++)
            {
                writer.Write(max_date[stock_num] + ",");
            }
            writer.Write("\r\n");


            //
            //MAX値の書き出し
            //
            max_value2[0] = "";

            for (stock_num = 0; stock_num <= num_stock; stock_num++)
            {
                //max_value2[stock_num] = Convert.ToString(max_value[stock_num]);
                writer.Write(max_value2[stock_num] + ",");
            }
            writer.Write("\r\n");


            //
            //MIN日付の書き出し
            //
            for (stock_num = 0; stock_num <= num_stock; stock_num++)
            {
                writer.Write(min_date[stock_num] + ",");
            }
            writer.Write("\r\n");


            //
            //MIN値の書き出し
            //
            min_value2[0] = "";

            for (stock_num = 0; stock_num <= num_stock; stock_num++)
            {
                //min_value2[stock_num] = Convert.ToString(min_value[stock_num]);
                writer.Write(min_value2[stock_num] + ",");
            }
            writer.Write("\r\n");

            //
            //MAX/MIN比の計算及び書き出し
            //
            for (stock_num = 0; stock_num <= num_stock; stock_num++)
            {
                max_min_ratio[stock_num] = 0;

                max_date2[stock_num] = Convert.ToDouble(max_date[stock_num]);
                min_date2[stock_num] = Convert.ToDouble(min_date[stock_num]);


                if (max_value2[stock_num] == "")
                {

                }
                else
                {
                    max_value[stock_num] = Convert.ToDouble(max_value2[stock_num]);
                    min_value[stock_num] = Convert.ToDouble(min_value2[stock_num]);

                    if (max_date2[stock_num] >= min_date2[stock_num])
                    {
                        max_min_ratio[stock_num] = (max_value[stock_num] / min_value[stock_num]) * 100;
                    }
                    else
                    {
                        max_min_ratio[stock_num] = (min_value[stock_num] / max_value[stock_num]) * 100;
                    }

                    //max_min_ratio[stock_num] = (max_value[stock_num] / min_value[stock_num] - 1) * 100;

                    max_min_ratio2[stock_num] = Convert.ToString(max_min_ratio[stock_num]);

                }

                if (max_min_ratio[stock_num] == 0)
                {
                    writer.Write("" + ",");
                }
                else if (max_min_ratio2[stock_num] == "NaN")
                {
                    writer.Write("" + ",");
                }
                else
                {
                    //if (max_date2[stock_num] >= min_date2[stock_num])
                    //{
                    //    writer.Write("+" + max_min_ratio2[stock_num] + ",");
                    //}
                    //else
                    //{
                    //    writer.Write("-" + max_min_ratio2[stock_num] + ",");
                    //}

                    if (max_date2[stock_num] >= min_date2[stock_num])
                    {
                        writer.Write(max_min_ratio2[stock_num] + ",");
                    }
                    else
                    {
                        writer.Write(max_min_ratio2[stock_num] + ",");
                    }

                }

            }
            writer.Write("\r\n");

            //
            //計算結果の書き出し
            //
            for (days = 2; days <= days2; days++)
            {
                for (stock_num = 0; stock_num <= num_stock; stock_num++)
                {
                    writer.Write(out_data_file[days, stock_num] + ",");
                }
                writer.Write("\r\n");

            }

            writer.Close();

            //
            //ここまでファイル書き出し
            //

        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void FolderBrowserDialog(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Quit_Button(object sender, EventArgs e)
        {
            MessageBox.Show("終了します。", "通知");
            //アプリケーションを終了する。
            Application.Exit();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void progress_num_TextChanged(object sender, EventArgs e)
        {

        }

        private void total_num_TextChanged(object sender, EventArgs e)
        {

        }

        private void file_contents_Box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
