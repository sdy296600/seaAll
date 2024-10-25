
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class LabelPrintImagePopup : Form
    {
        #region ○ 폼 이동

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        private void tspMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        #endregion

        #region ○ 변수선언

        public object _Image =  null;

        #endregion
  
        #region ○ 생성자

        public LabelPrintImagePopup(/*object pImage*/)
        {
            //_Image = pImage;

            InitializeComponent();

            Load += new EventHandler(Form_Load);
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.AllowDrop = false;
                //
                pictureBox1.DragEnter += Flie_DragEnter;
                //
                pictureBox1.DragDrop += Flie_DragDrop;

                if (_Image != null)
                {
                    pictureBox1.Image = _Image as Bitmap;
                }

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #endregion

        private void Flie_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                //string [] ch = new string[]{ ".PNG",".jpg",""};


                bool ck = false;
                foreach (string item in files)
                {
                    if (item.ToUpper().Contains(".PNG") || item.ToUpper().Contains(".JPG"))
                    {
                        ck = true;
                    }
                }
                if(!ck)
                {
                    CustomMsg.ShowMessage("확장자를 확인해주세요.");
                    return;
                }
                pictureBox1.Image = Image.FromFile(files[0]);
        
                textBox1.Text = System.IO.Path.GetFileName(files[0]);
                //using (frmWaitAsyncForm frm = new frmWaitAsyncForm(new Action(delegate
                //{
                //    Crc32 crc32 = new Crc32();

                //    DataTable dt = DBClass.Get_Autoupdate(_seleted_value);

                //    int ok = 0;

                //    DateTime dateTime = DateTime.Now;

                //    foreach (string file in files)
                //    {
                //        string name = file.Substring(file.LastIndexOf("\\") + 1);

                //        byte[] as1 = crc32.GetPhoto(file);

                //        DataRow[] drs = dt.Select($"FILENAME2 = '{name}'");

                //        if (drs.Length != 0)
                //        {
                //            DBClass.Update_Autoupdate(_seleted_value, name, as1, dateTime);

                //            ok++;
                //        }
                //        else
                //        {
                //            DBClass.insert_Autoupdate(_seleted_value, "0.0.0.0", name, as1);
                //            ok++;
                //        }

                //    }

                //    MessageBox.Show(ok + "건 수정 되었습니다.");
                //})))
                //{
                //    frm.ShowDialog(this);
                //}

                //dataGridView1_CellClick(null, new DataGridViewCellEventArgs(0, 0));

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        private void Flie_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    string urlstring = textBox1.Text; // 입력된 Web API
                    Bitmap bmp = new CoFAS_Label().WebImageView(urlstring); // 이미지 가져오기
                    pictureBox1.Image = bmp; // 이미지 적용
                }
            }
            catch
            {

            }

            //using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files|*.*";
            //    openFileDialog.FilterIndex = 1;
            //    openFileDialog.RestoreDirectory = true;

            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        // The user selected a file, you can now work with the file path
            //        string selectedFilePath = openFileDialog.FileName;

            //        // Perform operations with the selected file path (e.g., load the image)
            //        // For example, you can set the image in a PictureBox:
            //        pictureBox1.Image = new System.Drawing.Bitmap(selectedFilePath);

            //        // You can also do other operations like getting the file name:

            //        textBox1.Text = System.IO.Path.GetFileName(selectedFilePath);
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Image = pictureBox1.Image;
            //this.DialogResult = DialogResult.OK;
            //this.Close();

            if (pictureBox1.Image == null)
            {
                return;
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Application.StartupPath;
                saveFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files|*.*";
                saveFileDialog.FilterIndex = 1;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName);
                }
                
            }
         
        }
    }
}



