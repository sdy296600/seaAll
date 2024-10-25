using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace CoFAS.NEW.MES.Upload
{
    public partial class Form_Upload : frmBase
    {
        public Form_Upload()
        {
            this.Font = new Font("Hack", 9, FontStyle.Bold);
            InitializeComponent();
             
        }

        public string _seleted_value = null;
        private void Form_Upload_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridView1ColumnSet();
                DataGridView2ColumnSet();
                DataGridView3ColumnSet();

                dataGridView1.DataSource = DBClass.Get_Updateinto();

                dataGridView1.CellClick += dataGridView1_CellClick;

                dataGridView1.MouseClick += dataGridView1_MouseClick;
                dataGridView2.MouseClick += dataGridView2_MouseClick;
                dataGridView3.MouseClick += dataGridView3_MouseClick;

                btn_save.Click += btn_save_Click;
                btn_reset.Click += btn_reset_Click;

                Base_btn.Click += new EventHandler(delegate
                {
                    if (MessageBox.Show("프로그램을 종료 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        this.Close();
                        Application.Exit();
                    }
                });

                dataGridView2.AllowDrop = false;

                dataGridView2.DragEnter += Flie_DragEnter;

                dataGridView2.DragDrop += Flie_DragDrop;

                this.Refresh();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                txt_UPDATETYPE.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txt_NAME.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                _seleted_value = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                dataGridView2.AllowDrop = true;
                dataGridView2.DataSource = DBClass.Get_Autoupdate(_seleted_value);
                dataGridView3.DataSource = DBClass.Get_Autoupdatehistory(_seleted_value);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        private void Flie_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void Flie_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);


                using (frmWaitAsyncForm frm = new frmWaitAsyncForm(new Action(delegate
                {
                    Crc32 crc32 = new Crc32();

                    DataTable dt = DBClass.Get_Autoupdate(_seleted_value);

                    int ok = 0;

                    DateTime dateTime = DateTime.Now;

                    foreach (string file in files)
                    {
                        string name = file.Substring(file.LastIndexOf("\\") + 1);

                        byte[] as1 = crc32.GetPhoto(file);

                        DataRow[] drs = dt.Select($"FILENAME2 = '{name}'");

                        if (drs.Length != 0)
                        {
                            DBClass.Update_Autoupdate(_seleted_value, name, as1, dateTime);

                            ok++;
                        }
                        else
                        {
                            DBClass.insert_Autoupdate(_seleted_value, "0.0.0.0", name, as1);
                            ok++;
                        }

                    }

                    MessageBox.Show(ok + "건 수정 되었습니다.");
                })))
                {
                    frm.ShowDialog(this);
                }

                dataGridView1_CellClick(null, new DataGridViewCellEventArgs(0, 0));

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridView grid = sender as DataGridView;

                    int currentMouseOverRow = grid.HitTest(e.X, e.Y).RowIndex;

                    if (currentMouseOverRow < 0)
                    {
                        return;
                    }

                    ContextMenuStrip m = new ContextMenuStrip();

                    ToolStripMenuItem itme = new ToolStripMenuItem("삭제");

                    itme.ShortcutKeys = Keys.Control | Keys.D;

                    m.Items.Add(itme);

                    m.Items[0].Click += new EventHandler(delegate
                    {
                        try
                        {
                            if (MessageBox.Show("데이터를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {

                                DBClass.Delete_UpdateInto(_seleted_value);
                                btn_reset.PerformClick();
                            }

                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                    });

                    grid.Rows[currentMouseOverRow].Selected = true;

                    m.Show(grid, new Point(e.X, e.Y));

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridView grid = sender as DataGridView;

                    int currentMouseOverRow = grid.HitTest(e.X, e.Y).RowIndex;

                    if (currentMouseOverRow < 0)
                    {
                        return;
                    }
                    ContextMenuStrip m = new ContextMenuStrip();

                    ToolStripMenuItem itme = new ToolStripMenuItem("삭제");

                    itme.ShortcutKeys = Keys.Control | Keys.D;

                    m.Items.Add(itme);

                    m.Items[0].Click += new EventHandler(delegate
                    {
                        try
                        {

                            DataRow row = (grid.DataSource as DataTable).Rows[grid.SelectedRows[0].Index];
                            DBClass.Delete_Autoupdate(row);
                            dataGridView1_CellClick(null, new DataGridViewCellEventArgs(0, 0));

                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                    });


                    grid.Rows[currentMouseOverRow].Selected = true;

                    m.Show(grid, new Point(e.X, e.Y));

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void dataGridView3_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridView grid = sender as DataGridView;

                    int currentMouseOverRow = grid.HitTest(e.X, e.Y).RowIndex;

                    if (currentMouseOverRow < 0)
                    {
                        return;
                    }
                    ContextMenuStrip m = new ContextMenuStrip();

                    ToolStripMenuItem itme = new ToolStripMenuItem("삭제");
                    itme.ShortcutKeys = Keys.Control | Keys.D;
                    m.Items.Add(itme);

                    ToolStripMenuItem itme1 = new ToolStripMenuItem("롤백");
                    itme1.ShortcutKeys = Keys.Control | Keys.R;
                    m.Items.Add(itme1);

                    m.Items[0].Click += new EventHandler(delegate
                    {
                        try
                        {
                            if (MessageBox.Show("데이터를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                DataRow row = (grid.DataSource as DataTable).Rows[grid.SelectedRows[0].Index];
                                DBClass.Delete_Autoupdatehistor(row);
                                dataGridView1_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                            }

                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                    });
                    m.Items[1].Click += new EventHandler(delegate
                    {
                        try
                        {
                            if (MessageBox.Show("데이터를 롤백 하시겠습니까?", "롤백", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                DataRow row = (grid.DataSource as DataTable).Rows[grid.SelectedRows[0].Index];
                                DBClass.Rollback_Autoupdatehistor(row);
                                dataGridView1_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                            }

                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                    });

                    dataGridView3.Rows[currentMouseOverRow].Selected = true;

                    m.Show(dataGridView3, new Point(e.X, e.Y));

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void DataGridView1ColumnSet()
        {
            try
            {

                DataGridViewUtil.InitSettingGridView(dataGridView1);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "타입", "TYPE", true, 100, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "프로그램타임", "UPDATETYPE", true, 300, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "업체이름", "NAME", true, 300, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "등록일자", "REGNT", true, 200, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "사용유무", "USE_YN", true, 100, DataGridViewContentAlignment.MiddleCenter);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void DataGridView2ColumnSet()
        {
            try
            {

                DataGridViewUtil.InitSettingGridView(dataGridView2);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "프로그램타임", "UPDATETYPE", true, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "UTYPE", "UTYPE", false, 200, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "버전", "VERSION", true, 100, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "파일이름", "FILENAME", true, 250, DataGridViewContentAlignment.MiddleLeft);

                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "FILENAME2", "FILENAME2", false, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "FILEDATE", "FILEDATE", false, 200, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "TEXT", "TEXT", false, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "최신업로드일", "UPLOADDATE", true, 200, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "CRC", "CRC", true, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView2, "FILESIZE", "FILESIZE", true, 150, DataGridViewContentAlignment.MiddleCenter);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void DataGridView3ColumnSet()
        {
            try
            {

                DataGridViewUtil.InitSettingGridView(dataGridView3);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "프로그램타임", "UPDATETYPE", true, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "UTYPE", "UTYPE", false, 200, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "버전", "VERSION", true, 100, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "파일이름", "FILENAME", true, 250, DataGridViewContentAlignment.MiddleLeft);

                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "FILENAME2", "FILENAME2", false, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "FILEDATE", "FILEDATE", false, 200, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "TEXT", "TEXT", false, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "최신업로드일", "UPLOADDATE", true, 200, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "CRC", "CRC", true, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView3, "FILESIZE", "FILESIZE", true, 150, DataGridViewContentAlignment.MiddleCenter);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void btn_save_Click(object sender, EventArgs e)
        {

            if (txt_UPDATETYPE.Text.Length == 0)
            {
                MessageBox.Show("업데이트타입을 입력해주세요.");
                return;
            }
            if (txt_NAME.Text.Length == 0)
            {
                MessageBox.Show("명칭을 입력해주세요.");
                return;
            }


            if (_seleted_value == null)
            {
                DBClass.insert_UpDateInto(txt_UPDATETYPE.Text, txt_NAME.Text);
                dataGridView1.DataSource = DBClass.Get_Updateinto();
            }
            else
            {
                DBClass.Update_UpDateInto(_seleted_value, txt_UPDATETYPE.Text, txt_NAME.Text);
                dataGridView1.DataSource = DBClass.Get_Updateinto();

            }

            btn_reset.PerformClick();

            MessageBox.Show("저장 되었습니다.");

        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            if (_seleted_value != null)
            {
                _seleted_value = null;
                dataGridView2.AllowDrop = false;
                dataGridView1.SelectedRows[0].Selected = false;
                txt_UPDATETYPE.Text = "";
                txt_NAME.Text = "";
                dataGridView1.DataSource = DBClass.Get_Updateinto();
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
            }
        }
        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            foreach (DataGridViewRow item in dgv.Rows)
            {
                if (item.Cells[4].Value.ToString() == "N")
                {
                    item.DefaultCellStyle.BackColor = Color.Gray;
                    item.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }


    }
}
