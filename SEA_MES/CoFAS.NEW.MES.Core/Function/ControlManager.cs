using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core.Function
{
    public class ControlManager
    {

        delegate void delInvoke_Control_Text(System.Windows.Forms.Control ctl, string strText);
        /// <summary>
        /// Control에 TEXT를 변경한다.
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="strText"></param>
        public static void Invoke_Control_Text(System.Windows.Forms.Control ctl, string strText)
        {
            if (ctl.InvokeRequired)
                ctl.Invoke(new delInvoke_Control_Text(Invoke_Control_Text), new object[] { ctl, strText });
            else
                ctl.Text = strText;
        }

        delegate void delInvoke_Control_TextColor(System.Windows.Forms.Control ctl, string strText, Color _color);
        /// <summary>
        /// Control에 TEXT를 변경한다.
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="strText"></param>
        public static void Invoke_Control_Text(System.Windows.Forms.Control ctl, string strText, Color _color)
        {
            if (ctl.InvokeRequired)
                ctl.Invoke(new delInvoke_Control_TextColor(Invoke_Control_Text), new object[] { ctl, strText, _color });
            else
            {
                ctl.Text = strText;
                ctl.ForeColor = _color;
            }
        }
        public static void InvokeIfNeeded(Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }

        public static void InvokeIfNeeded(UserControl uccontrol, Action action)
        {
            if (uccontrol.InvokeRequired)
                uccontrol.Invoke(action);
            else
                action();
        }

        public static void InvokeIfNeeded<T>(Control control, Action<T> action, T arg)
        {
            if (control.InvokeRequired)
                control.Invoke(action, arg);
            else
                action(arg);
        }

        #region 콤보박스 invoke관련

        delegate void delInvoke_ComboBox_DataSource(ComboBox cmb, DataTable dt, string strDisplayMember);
        /// <summary>
        /// 콤보박스에 Data Binding한다
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="dt"></param>
        /// <param name="strDisplayMember"></param>
        public static void Invoke_ComboBox_DataSource(ComboBox cmb, DataTable dt, string strDisplayMember)
        {
            if (cmb.InvokeRequired)
            {
                cmb.Invoke(new delInvoke_ComboBox_DataSource(Invoke_ComboBox_DataSource), new object[] { cmb, dt, strDisplayMember });
                return;
            }
            cmb.DataSource = dt;
            cmb.DisplayMember = strDisplayMember;
            //cmb.SelectedIndex = -1;
        }

        delegate void delInvoke_ComboBox_AddItem(ComboBox cmb, string[] strItems);
        /// <summary>
        /// 콤보박스에 데이터를 추가하여 준다.
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="strItems"></param>
        public static void Invoke_ComboBox_AddItem(ComboBox cmb, string[] strItems)
        {
            if (cmb.InvokeRequired)
            {
                cmb.Invoke(new delInvoke_ComboBox_AddItem(Invoke_ComboBox_AddItem), new object[] { cmb, strItems });
                return;
            }

            foreach (string s in strItems)
            {
                cmb.Items.Add(s);
            }
        }

        delegate void delInvoke_ComboBox_SelectedIndex(ComboBox cmb, int intIndex);
        /// <summary>
        /// 콤보박스에 SelectedIndex 값을 설정한다.
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="intIndex"></param>
        public static void Invoke_ComboBox_SelectedIndex(ComboBox cmb, int intIndex)
        {
            if (cmb.InvokeRequired)
            {
                cmb.Invoke(new delInvoke_ComboBox_SelectedIndex(Invoke_ComboBox_SelectedIndex), new object[] { cmb, intIndex });
                return;
            }
            //if (cmb.Items.Count > intIndex)	--에러를 처리 삭제
            cmb.SelectedIndex = intIndex;
        }

        delegate void delInvoke_ComboBox_SelectedItem(ComboBox cmb, string strField, string strSelectText);
        /// <summary>
        /// 바인딩된 콤보박스에 특정 필드에 값으로 아이템 선택
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="strField">필드명</param>
        /// <param name="strSelectText">찾는 값</param>
        public static void Invoke_ComboBox_SelectedItem(ComboBox cmb, string strField, string strSelectText)
        {
            if (cmb.InvokeRequired)
            {
                cmb.Invoke(new delInvoke_ComboBox_SelectedItem(Invoke_ComboBox_SelectedItem), new object[] { cmb, strField, strSelectText });
                return;
            }

            try
            {
                foreach (object obj in cmb.Items)
                {
                    DataRowView dv = (DataRowView)obj;

                    if (dv[strField].ToString() == strSelectText)
                    {
                        cmb.SelectedItem = obj;
                        return;
                    }
                }
                cmb.SelectedIndex = -1;
            }
            catch
            {
                cmb.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 콤보 박스에 선택된 값을 가져온다.(Invoke 아님)
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="strField"></param>
        public static string Invoke_ComboBox_GetSelectValue(ComboBox cmb, string strField)
        {

            if (cmb.SelectedIndex < 0) return string.Empty;

            DataRowView dv = (DataRowView)cmb.SelectedItem;

            return ConvertManager.obj2String(dv[strField]);
        }

        delegate void delInvoke_ComboBox_ClearItem(ComboBox cmb);
        /// <summary>
        /// 콤보박스에 item를 전부 삭제한다.
        /// </summary>
        /// <param name="cmb"></param>
        public static void Invoke_ComboBox_ClearItem(ComboBox cmb)
        {
            if (cmb.InvokeRequired)
            {
                cmb.Invoke(new delInvoke_ComboBox_ClearItem(Invoke_ComboBox_ClearItem), new object[] { cmb });
                return;
            }

            if (cmb.DataSource != null)
                cmb.DataSource = null;
            else
                cmb.Items.Clear();
        }
        #endregion

        #region Etc invoke관련
        delegate void delInvoke_PictureBox_Image(PictureBox pb, Image im);
        /// <summary>
        /// Control에 TEXT를 변경한다.
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="strText"></param>
        public static void Invoke_PictureBox_Image(PictureBox pb, Image im)
        {
            if (pb.InvokeRequired)
                pb.Invoke(new delInvoke_PictureBox_Image(Invoke_PictureBox_Image), new object[] { pb, im });
            else
                pb.Image = im;
        }
        #endregion

        #region Listview invoke 관련

        delegate void delInvoke_ListView_ItemClear(ListView lv);
        /// <summary>
        /// listview에 item을 모두 제거 한다.
        /// </summary>
        /// <param name="lv"></param>
        public static void Invoke_ListView_ItemClear(ListView lv)
        {
            if (lv.InvokeRequired)
            {
                lv.Invoke(new delInvoke_ListView_ItemClear(Invoke_ListView_ItemClear), new object[] { lv });
                return;
            }

            lv.Items.Clear();

        }

        /// <summary>
        /// listview에 선택된 item을 모두 제거 한다.
        /// </summary>
        /// <param name="lv"></param>
        public static void Invoke_ListView_SelectedItemClear(ListView lv)
        {
            if (lv.InvokeRequired)
            {
                lv.Invoke(new delInvoke_ListView_ItemClear(Invoke_ListView_SelectedItemClear), new object[] { lv });
                return;
            }

            lv.SelectedItems.Clear();

        }

        delegate void delInvoke_DateTimePicker_Value(DateTimePicker dtp, DateTime value);
        /// <summary>
        /// Control에 Visible을 변경한다.
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="isVisible"></param>
        public static void Invoke_DateTimePicker_Value(DateTimePicker dtp, DateTime value)
        {
            if (dtp.InvokeRequired)
                dtp.Invoke(new delInvoke_DateTimePicker_Value(Invoke_DateTimePicker_Value), new object[] { dtp, value });
            else
                dtp.Value = value;
        }


        delegate void delInvoke_ListView_AddItem(ListView lv, bool isClear, DataTable dt, string[] strColumn);
        /// <summary>
        /// listview에 datatable에서 item을 추가 한다.
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="dt"></param>
        /// <param name="strColumn">datatabel 컬럼명 순서데로 filed 추가 : '__NO'는 count</param>
        public static void Invoke_ListView_AddItem(ListView lv, bool isClear, DataTable dt, string[] strColumn)
        {
            if (lv.InvokeRequired)
            {
                lv.Invoke(new delInvoke_ListView_AddItem(Invoke_ListView_AddItem), new object[] { lv, isClear, dt, strColumn });
                return;
            }

            if (isClear) lv.Items.Clear();

            string[] strValue = new string[strColumn.Length];

            int intRow = 1;
            foreach (DataRow dr in dt.Rows)
            {
                int intCol = 0;

                foreach (string strCol in strColumn)
                {
                    if (strCol == string.Empty)
                        strValue[intCol] = string.Empty;
                    else if (strCol == "__NO")
                        strValue[intCol] = (lv.Items.Count + 1).ToString();
                    else
                        strValue[intCol] = ConvertManager.obj2String(dr[strCol]);

                    intCol++;
                }

                ListViewItem item = new ListViewItem(strValue);

                lv.Items.Add(item);

                intRow++;
            }

        }

        delegate void delInvoke_ListView_AddItemString(ListView lv, bool isClear, string[] strData, bool isToTop, int intMaxRowCount);
        /// <summary>
        /// ListView에 string 배열로 부터 item을 추가 한다.
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="isClear">기존데이터 삭제 여부</param>
        /// <param name="strData">추가 데이터 string 배열</param>
        /// <param name="isToTop">listview에 위에 데이터 추가</param>
        /// <param name="intMaxRowCount">최대 item 숫자</param>
        public static void Invoke_ListView_AddItemString(ListView lv, bool isClear, string[] strData, bool isToTop, int intMaxRowCount)
        {
            if (lv.InvokeRequired)
            {
                lv.Invoke(new delInvoke_ListView_AddItemString(Invoke_ListView_AddItemString), new object[] { lv, isClear, strData, isToTop, intMaxRowCount });
                return;
            }

            if (isClear) lv.Items.Clear();

            ListViewItem li = new ListViewItem(strData);

            if (isToTop)
                lv.Items.Insert(0, li);
            else
                lv.Items.Add(li);

            while (lv.Items.Count > intMaxRowCount)
            {
                int intIndex;

                if (isToTop)
                    intIndex = lv.Items.Count - 1;
                else
                    intIndex = 0;

                lv.Items.RemoveAt(intIndex);
            }
        }

        delegate void delInvoke_ListView_AddItemStringColor(ListView lv, bool isClear, string[] strData, bool isToTop, int intMaxRowCount, object colForeColor, object colBackColor);
        /// <summary>
        /// string 배열로 부터 item을 추가 및 색상을 설정한다.
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="isClear">기존데이터 삭제 여부</param>
        /// <param name="strData">추가 데이터 string 배열</param>
        /// <param name="isToTop">listview에 위에 데이터 추가</param>
        /// <param name="intMaxRowCount">최대 item 숫자</param>
        /// <param name="colForeColor">글자색 (null : 기본색)</param>
        /// <param name="colBackColor">배경색 (null : 기본색)</param>
        public static void Invoke_ListView_AddItemStringColor(ListView lv, bool isClear, string[] strData, bool isToTop, int intMaxRowCount, object colForeColor, object colBackColor)
        {
            if (lv.InvokeRequired)
            {
                lv.Invoke(new delInvoke_ListView_AddItemStringColor(Invoke_ListView_AddItemStringColor), new object[] { lv, isClear, strData, isToTop, intMaxRowCount, colForeColor, colBackColor });
                return;
            }

            if (isClear) lv.Items.Clear();

            ListViewItem li = new ListViewItem(strData);

            if (colForeColor != null) li.ForeColor = (Color)colForeColor;
            if (colBackColor != null) li.BackColor = (Color)colBackColor;

            if (isToTop)
                lv.Items.Insert(0, li);
            else
                lv.Items.Add(li);

            while (lv.Items.Count > intMaxRowCount)
            {
                int intIndex;

                if (isToTop)
                    intIndex = lv.Items.Count - 1;
                else
                    intIndex = 0;

                lv.Items.RemoveAt(intIndex);
            }
        }

        delegate void delInvoke_ListView_ColumnHeader_Text(ListView li, ColumnHeader ch, string strText);
        /// <summary>
        /// listvivew에 컬럼해드 텍스트 변경 한다
        /// </summary>
        /// <param name="li"></param>
        /// <param name="ch"></param>
        /// <param name="strText"></param>
        public static void Invoke_ListView_ColumnHeader_Text(ListView li, ColumnHeader ch, string strText)
        {
            if (li.InvokeRequired)
            {
                li.Invoke(new delInvoke_ListView_ColumnHeader_Text(Invoke_ListView_ColumnHeader_Text), new object[] { li, ch, strText });
                return;
            }

            ch.Text = strText;
        }

        delegate void delInvoke_ListView_ColumnHeader_Width(ListView li, ColumnHeader ch, int intWidth);
        /// <summary>
        /// listvivew에 컬럼해드 너비 변경 한다
        /// </summary>
        /// <param name="li"></param>
        /// <param name="ch"></param>
        /// <param name="strText"></param>
        public static void Invoke_ListView_ColumnHeader_Width(ListView li, ColumnHeader ch, int intWidth)
        {
            if (li.InvokeRequired)
            {
                li.Invoke(new delInvoke_ListView_ColumnHeader_Width(Invoke_ListView_ColumnHeader_Width), new object[] { li, ch, intWidth });
                return;
            }

            ch.Width = intWidth;
        }

        delegate void delInvoke_ListViewItem_Set_SubItem(ListView li, int intItemRow, int intSubItemIndex, string strText);
        /// <summary>
        /// 리스트뷰아이템에 서브아이템 text 변경 한다
        /// </summary>
        /// <param name="li"></param>
        /// <param name="intItemRow"></param>
        /// <param name="intSubItemIndex"></param>
        /// <param name="strText"></param>
        public static void Invoke_ListViewItem_Set_SubItem(ListView li, int intItemRow, int intSubItemIndex, string strText)
        {
            if (li.InvokeRequired)
            {
                li.Invoke(new delInvoke_ListViewItem_Set_SubItem(Invoke_ListViewItem_Set_SubItem), new object[] { li, intItemRow, intSubItemIndex, strText });
                return;
            }

            li.Items[intItemRow].SubItems[intSubItemIndex].Text = strText;
        }

        delegate string delInvoke_ListViewItem_Get_SubItem(ListView li, int intItemRow, int intSubItemIndex);
        /// <summary>
        /// 리스트뷰아이템에 서브아이템 text 변경 한다
        /// </summary>
        /// <param name="li"></param>
        /// <param name="intItemRow"></param>
        /// <param name="intSubItemIndex"></param>
        /// <param name="strText"></param>
        public static string Invoke_ListViewItem_Get_SubItem(ListView li, int intItemRow, int intSubItemIndex)
        {
            if (li.InvokeRequired)
            {
                li.Invoke(new delInvoke_ListViewItem_Get_SubItem(Invoke_ListViewItem_Get_SubItem), new object[] { li, intItemRow, intSubItemIndex });
                return "";
            }

            return li.Items[intItemRow].SubItems[intSubItemIndex].Text.Trim();
        }

        /// <summary>
        /// 리스트뷰아이템에 서브아이템 text 리턴 한다
        /// </summary>
        /// <param name="li"></param>
        /// <param name="intSubItemIndex"></param>
        public static string ListViewItem_Get_SubItem(ListViewItem li, int intSubItemIndex)
        {
            return li.SubItems[intSubItemIndex].Text.Trim();
        }

        #endregion
    }
}
