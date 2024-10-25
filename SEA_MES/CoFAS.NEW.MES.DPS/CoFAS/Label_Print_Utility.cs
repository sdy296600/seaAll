
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static CoFAS.NEW.MES.DPS.RawPrinterHelper;

namespace CoFAS.NEW.MES.DPS
{
    public class Label_Print_Utility
    {

        public bool Barcode_Print(string str)
        {
            try
            {
                bool ok = true;
                string print_name = "ZDesigner ZD421-203dpi ZPL";

                PrinterSettings settings = null;

                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    if (printer == print_name)
                    {
                        settings = new PrinterSettings();
                        settings.PrinterName = printer;
                    }
                }
                if (settings == null)
                {
                    MessageBox.Show("연결된 바코드 프린터가 없습니다.");
                    return false;
                }
                else
                {
                    _ucbtPRINT_Call(settings.PrinterName, str);
                }
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }
        private void _ucbtPRINT_Call(string printerName, string strPrintData)
        {
            try
            {
                if (printerName != null)
                {
                    // Create a buffer with the command
                    Byte[] buffer = new byte[strPrintData.Length];
                    string tmp_PrintData = strPrintData;
                    buffer = System.Text.Encoding.ASCII.GetBytes(tmp_PrintData);
                    SendStringToPrinter(printerName, tmp_PrintData);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public bool SendStringToPrinter(string szPrinterName, string szString)
        {
            try
            {
                IntPtr pBytes;
                Int32 dwCount;
                dwCount = (szString.Length + 1) * Marshal.SystemMaxDBCSCharSize;
                pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                Marshal.FreeCoTaskMem(pBytes);
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }
        public bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            try
            {
                Int32 dwError = 0, dwWritten = 0;
                IntPtr hPrinter = new IntPtr(0);
                DOCINFOA di = new DOCINFOA();
                bool bSuccess = false;
                di.pDocName = "My C#.NET RAW Document";
                di.pDataType = "RAW";
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        if (StartPagePrinter(hPrinter))
                        {
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
                return bSuccess;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }



    }

    public class RawPrinterHelper
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // 정상 처리 안되면 실패 처리 초기값 설정

            di.pDocName = "Zebra Printing";
            di.pDataType = "RAW";

            // 기본설정 프린터 열기
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // 프린터 문서 시작
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // 페이지 시작
                    if (StartPagePrinter(hPrinter))
                    {
                        // 바이트 쓰기
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // 프린터 성공 하지 못하면, GetLastError 가 오류 항목 제공
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }
    }

}
