using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace CoFAS.NEW.MES.DPS
{
    public partial class LabelPrinter : Form
    {
        static SerialPort serialPort;
        string barcodeVal = "";
        string barcode = "";
        string stop_barcode = "^XA ^HH ^XZ";


        public LabelPrinter()
        {
            InitializeComponent();
        }

        private void LabelPrinter_Load(object sender, EventArgs e)
        {
            LabelPrinter_conn();

            //textBox1.Text = "^XA ^SEE:UHANGUL.DAT^FS ^CW1,E:KFONT3.FNT^CI26^FS ^FO145,50^A1N,20,20^FD(주)이앤아이비^FS ^FO30,100^GB500,180,3^FS ^FO200,100^GB3,180,3^FS ^FO400,160^GB3,120,3^FS ^CF0,40 ^CF0,190 ^FO30,160^GB500,3,3^FS ^FO30,220^GB370,3,3^FS ^CF0,22 ^FO50,125 ^A1N,15,15 ^FDPRODUCT NAME ^FS ^FO95,185 ^A1N,15,15 ^FDSIZE ^FS ^FO95,245 ^A1N,15,15 ^FDLOT.^FS ^FO220,125 ^A1N,15,15 ^FD@품명 ^FS ^FO220,185 ^A1N,15,15 ^FD@규격 ^FS ^FO220,245 ^A1N,15,15 ^FD@LOT ^FS ^FO400,300 ^A1N,15,15 ^FD^FS ^FO415,170 ^BQN,2,4 ^FDQA@큐알바코드 ^FS ^FO50,285^BY2^B3N,N,15,Y,N^FD@바코드^FS ^XZ";

            textBox1.Text = @"^XA 
^SEE:UHANGUL.DAT^FS ^CW1,E:KFONT3.FNT^CI26^FS ^FO270,50^A1N,40,40^FD(주)이앤아이비^FS 
^FO150,125^GB500,140,3^FS 
^FO340,125^GB3,140,3^FS
^FO150,175^GB500,3,3^FS 
^FO150,220^GB500,3,3^FS ^CF0,22
^FO170,140 ^A1N,20,20 ^FDPRODUCT NAME ^FS 
^FO200,190 ^A1N,20,20 ^FDSIZE ^FS
^FO200,235 ^A1N,20,20 ^FDLOT.^FS
^FO350,140 ^A1N,20,20 ^FD@품명 ^FS 
^FO350,190 ^A1N,20,20 ^FD@규격 ^FS
^FO350,235 ^A1N,20,20 ^FD@LOT ^FS
^FO170,270 ^BCN,20,Y,N,Y^FD@바코드^FS 
^XZ";













            barcodeVal = textBox1.Text;
            barcode = textBox1.Text; //초기화때 재사용

            textBox2.Focus();
        }

        private void LabelPrinter_conn()
        {
            // 시리얼 포트 설정
            string portName = "COM1"; // 사용할 포트명 지정
            int baudRate = 9600; // 통신 속도 설정
            Parity parity = Parity.None; // 패리티 설정
            int dataBits = 8; // 데이터 비트 설정
            StopBits stopBits = StopBits.One; // 정지 비트 설정

            // SerialPort 객체 생성
            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

            try
            {
                // 포트 열기
                serialPort.Open();
            }
            catch (Exception ex)
            {

            }
        }
        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           
        }

        private void Output_label(string data)
        {
            if (btncmd06.Text == "발행")
            {
                return;
            }
            else
            {
                Label_Print_Utility label = new Label_Print_Utility();
                label.Barcode_Print(data);
                //byte[] bytes = Encoding.Default.GetBytes(data);
                //serialPort.Write(bytes, 0, bytes.Length);
            }
        }


        private async void BtnCmd_Click(object sender, EventArgs e)
        {
            Button pCmd = sender as Button;
            string sCls = pCmd.Name.Substring(6, 2);
            switch (sCls)
            {
                case "01":  // 초기화

                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox1.Text = barcode;
                    break;
                case "02":  

                    break;
                case "03": 
                    break;
                case "04":  
                    break;
                case "05":  // 전체 적용

                   
                    barcodeVal = barcode;
                   
                    barcodeVal = barcodeVal.Replace("@품명", textBox2.Text);
                    barcodeVal = barcodeVal.Replace("@규격", textBox3.Text);
                    barcodeVal = barcodeVal.Replace("@LOT", textBox4.Text);
                    //barcodeVal = barcodeVal.Replace("@큐알바코드", textBox5.Text);
                    barcodeVal = barcodeVal.Replace("@바코드", textBox5.Text);

                    textBox1.Invoke(new MethodInvoker(delegate
                    {
                        // 실제 UI 업데이트 작업 수행
                        textBox1.Text = barcodeVal;
                        
                    }));
                    break;
                case "06":  // 발행
                   
                    if (pCmd.Text == "발행")
                    {
                       
                            if (textBox6.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0)
                        {
                            btncmd06.Invoke(new MethodInvoker(delegate
                            {
                                // 실제 UI 업데이트 작업 수행
                                btncmd06.Text = "중지";
                            }));

                            // 비활성화 해제
                            control_readonly(true);

                            //Label_Print_Utility label = new Label_Print_Utility();
                            for (int i = 0; i < int.Parse(textBox6.Text); i++)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    //label.Barcode_Print(barcodeVal);
                                    Output_label(barcodeVal);  // 라벨 발행
                                });
                                await DelayAsync(2000); // 2000 밀리초 (2초) 딜레이

                            }

                            btncmd06.Invoke(new MethodInvoker(delegate
                            {
                                // 실제 UI 업데이트 작업 수행
                                btncmd06.Text = "발행";
                            }));
                            control_readonly(false);
                        }
                        else
                        {
                            MessageBox.Show("모든 데이터를 입력해주세요.");
                        }
                    }
                    else // 중지 일 경우
                    {
                        btncmd06.Invoke(new MethodInvoker(delegate
                        {
                                // 실제 UI 업데이트 작업 수행
                                btncmd06.Text = "발행";
                        }));

                        Output_label(stop_barcode);  // 라벨 발행 중지 명령


                        // 비활성화 해제
                        control_readonly(false);
                        return;
                       
                    }
                   
                    break;

                default: break;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 입력된 문자가 숫자인지 확인
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // 숫자가 아니면 입력을 막음
            }
        }

        private void control_readonly(bool value)
        {
            if(value)
            {
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                btncmd05.Enabled = false;
            }
            else
            {
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                btncmd05.Enabled = true;
            }
        }

        private async Task DelayAsync(int milliseconds)
        {
            // 비동기 딜레이 적용
            await Task.Delay(milliseconds);
        }
    }
}
