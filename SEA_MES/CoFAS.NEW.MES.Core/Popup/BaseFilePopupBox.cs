
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class BaseFilePopupBox : Form
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

        public byte[] _File =  null;
        public string _File_name = string.Empty; 
        #endregion

        #region ○ 생성자

        public BaseFilePopupBox(byte[] File, string File_name)
        {
            _File = File;
            _File_name = File_name;
            InitializeComponent();

            Load += new EventHandler(Form_Load);
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                //pictureBox1.AllowDrop = false;
                ////
                //pictureBox1.DragEnter += Flie_DragEnter;
                ////
                //pictureBox1.DragDrop += Flie_DragDrop;

                textBox1.Text = _File_name;

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }



        #endregion



        private void btn_저장_Click(object sender, EventArgs e)
        {
            if (_File != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void btn_찾기_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    //openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // The user selected a file, you can now work with the file path
                        string selectedFilePath = openFileDialog.FileName;

                        // Perform operations with the selected file path (e.g., load the image)
                        // For example, you can set the image in a PictureBox:
                        Crc32 crc32 = new Crc32();
                        _File = crc32.GetPhoto(selectedFilePath);
                        _File_name = openFileDialog.SafeFileName; 
                        //byte[] as1 = crc32.GetPhoto(file);
                        // You can also do other operations like getting the file name:

                        textBox1.Text = System.IO.Path.GetFileName(selectedFilePath);
                    }
                }
            }
            catch(Exception err)
            {

            }
        }

        private void btn_내려받기_Click(object sender, EventArgs e)
        {
            try
            {
                if (_File != null)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                        InitialDirectory = @"C:\",
                        FileName = _File_name
                    };

                    // 대화 상자를 표시하고 결과를 확인합니다.
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 사용자가 파일을 선택한 경우 파일 경로를 가져옵니다.
                        string selectedFilePath = saveFileDialog.FileName;

                        // 파일의 내용을 읽습니다.
                        //byte[] fileData = File.ReadAllBytes(selectedFilePath);

                        //// 저장할 파일의 경로와 이름을 지정합니다.
                        //string saveFilePath = @"C:\path\to\your\saved_file" + Path.GetExtension(selectedFilePath);

                        // 파일에 바이너리 데이터를 씁니다.
                        File.WriteAllBytes(selectedFilePath, _File);

                        CustomMsg.ShowMessageLink("파일로 저장 하였습니다.\r\n" + saveFileDialog.FileName.ToString(), "Excel 저장 알림");
                        
                        this.Close();

                    }
                }
               
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
    }
}



