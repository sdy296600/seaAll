using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Ports
{
    /// <summary>
    /// 바우드레이트 : bit/sec 
    /// </summary>
    public enum BaudRate : int
    {
        b110 = 110,
        b300 = 300,
        b1200 = 1200,
        b2400 = 2400,
        b4800 = 4800,
        b9600 = 9600,
        b19200 = 19200,
        b38400 = 38400,
        b57600 = 57600,
        b115200 = 115200,
        b230400 = 230400,
        b460800 = 460800,
        b921600 = 921600
    };
    /// <summary>
    /// 데이터비트 bit
    /// </summary>
    public enum DataBits : int
    {
        Bit5 = 5,
        Bit6 = 6,
        Bit7 = 7,
        Bit8 = 8
    };

}



namespace CoFAS.NEW.MES.Core.Function
{
    public class CoFAS_Serial : IDisposable
    {
        SerialPort clsSn;

        public delegate void delReceive(byte[] yReceiveData);
        /// <summary>
        /// Receive 이벤트를 사용하기 위한 이벤트를 선언한다. 
        /// </summary>
        public delReceive evtReceived;

        public delegate void delReceivePort(byte[] yReceiveData,string port);

        public delReceivePort evtReceivedPort;

        //public enum RtsEnable : int
        //{
        //    RtsE_True = 1,
        //    RtsE_False = 0
        //};

        //public bool bRtsEnable
        //{
        //    get{
        //            return clsSn.RtsEnable;

        //    }
        //    set
        //    {
        //        clsSn.RtsEnable = value;
        //    }
        //}


        /// <summary>
        /// Serial PortName
        /// </summary>
        public string PortName
        {
            get
            {
                return clsSn.PortName;
            }
            set
            {
                clsSn.PortName = value;
            }
        }
        /// <summary>
        /// 개체의 BaudRate를 가져오거나 설정합니다.
        /// </summary>
        public BaudRate BaudRate
        {
            get
            {
                return (BaudRate)clsSn.BaudRate;
            }
            set
            {
                clsSn.BaudRate = (int)value;
            }
        }

        /// <summary>
        /// 개체의 패리티 비트를 가져오거나 설정합니다.
        /// </summary>
        public Parity Parity
        {
            get
            {
                return clsSn.Parity;
            }
            set
            {
                clsSn.Parity = value;
            }
        }


        /// <summary>
        /// 바이트당 데이터 비트의 표준 길이를 가져오거나 설정합니다.
        /// </summary>
        public DataBits DataBits
        {
            get
            {
                return (DataBits)clsSn.DataBits;
            }
            set
            {
                clsSn.DataBits = (int)value;
            }
        }


        /// <summary>
        /// 바이트당 정지 비트의 표준 개수를 가져오거나 설정합니다.
        /// </summary>
        public StopBits StopBits
        {
            get
            {
                return clsSn.StopBits;
            }
            set
            {
                clsSn.StopBits = value;
            }

        }

        /// <summary>
        /// 데이터의 직렬 포트 전송을 위한 핸드셰이킹 프로토콜을 가져오거나 설정합니다.
        /// </summary>
        public Handshake Handshake
        {
            get
            {
                return clsSn.Handshake;
            }
            set
            {
                clsSn.Handshake = value;
            }
        }


        /// <summary>
        /// 직렬포트 열림 또는 닫힘 상태를 나타내는 값을 가져옵니다.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return clsSn.IsOpen;
            }
        }



        /// <summary>
        /// 클래스생성
        /// </summary>
        public CoFAS_Serial()
        {
            clsSn = new SerialPort();

            Init();
        }

        /// <summary>
        /// 클래스생성
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        public CoFAS_Serial(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            clsSn = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            Init();
        }


        /// <summary>
        /// 클래스생성
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        public CoFAS_Serial(string portName, int baudRate, string parity, int dataBits, string stopBits)
        {
            clsSn = new SerialPort(
                                portName,
                                baudRate,
                                String2Parity(parity),
                                dataBits,
                                String2stopBits(stopBits));

            Init();
        }

        /// <summary>
        /// 핸드쉐이크 추가 객체생성 485/422 관련
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        public CoFAS_Serial(string portName, BaudRate baudRate, Parity parity, DataBits dataBits, StopBits stopBits)
        {
            clsSn = new SerialPort();

            this.PortName = portName;
            this.BaudRate = baudRate;
            this.Parity = parity;
            this.DataBits = dataBits;
            this.StopBits = stopBits;
            this.Handshake = System.IO.Ports.Handshake.RequestToSend;
            clsSn.DtrEnable = true;
            Init();
        }





        /// <summary>
        /// 새 직렬 포트 연결을 엽니다.
        /// </summary>
        public void Open()
        {
            clsSn.Open();
        }

        /// <summary>
        /// 포트 연결을 닫고, System.IO.Ports.SerialPort.IsOpen 속성을 false로 설정하고, 내부 System.IO.Stream
        /// 개체를 삭제합니다.
        /// </summary>
        public void Close()
        {
            clsSn.Close();
        }

        //
        // 요약:
        //     매개 변수 문자열을 출력에 씁니다.
        //
        // 매개 변수:
        //   text:
        //     출력할 문자열입니다.
        //
        // 예외:
        //   System.ArgumentNullException:
        //     str가 null인 경우
        //
        //   System.ServiceProcess.TimeoutException:
        //     시간 제한이 끝나기 전에 작업이 완료되지 않은 경우
        //
        //   System.InvalidOperationException:
        //     지정한 포트가 열려 있지 않은 경우
        public void Write(string text)
        {
            clsSn.Write(text);
        }
        //
        // 요약:
        //     지정한 수의 바이트를 출력 버퍼의 지정한 오프셋에 씁니다.
        //
        // 매개 변수:
        //   offset:
        //     쓰기를 시작할 버퍼 배열의 오프셋입니다.
        //
        //   count:
        //     쓸 바이트 수입니다.
        //
        //   buffer:
        //     출력 내용을 쓸 바이트 배열입니다.
        //
        // 예외:
        //   System.ArgumentException:
        //     offset과 count의 합이 buffer의 길이보다 큰 경우
        //
        //   System.ServiceProcess.TimeoutException:
        //     시간 제한이 끝나기 전에 작업이 완료되지 않은 경우
        //
        //   System.ArgumentNullException:
        //     전달된 buffer가 null인 경우
        //
        //   System.ArgumentOutOfRangeException:
        //     offset 또는 count 매개 변수가 전달된 buffer의 올바른 영역 밖에 있는 경우 offset 또는 count가 0보다 작은
        //     경우
        //
        //   System.InvalidOperationException:
        //     지정한 포트가 열려 있지 않은 경우
        public void Write(byte[] buffer, int offset, int count)
        {
            clsSn.Write(buffer, offset, count);
        }
        //
        // 요약:
        //     지정한 수의 문자를 출력 버퍼의 지정한 오프셋에 씁니다.
        //
        // 매개 변수:
        //   offset:
        //     쓰기를 시작할 버퍼 배열의 오프셋입니다.
        //
        //   count:
        //     쓸 문자 수입니다.
        //
        //   buffer:
        //     출력 내용을 쓸 문자 배열입니다.
        //
        // 예외:
        //   System.ArgumentException:
        //     offset과 count의 합이 buffer의 길이보다 큰 경우
        //
        //   System.ServiceProcess.TimeoutException:
        //     시간 제한이 끝나기 전에 작업이 완료되지 않은 경우
        //
        //   System.ArgumentNullException:
        //     전달된 buffer가 null인 경우
        //
        //   System.ArgumentOutOfRangeException:
        //     offset 또는 count 매개 변수가 전달된 buffer의 올바른 영역 밖에 있는 경우 offset 또는 count가 0보다 작은
        //     경우
        //
        //   System.InvalidOperationException:
        //     지정한 포트가 열려 있지 않은 경우
        public void Write(char[] buffer, int offset, int count)
        {
            clsSn.Write(buffer, offset, count);
        }



        private void Init()
        {
            clsSn.DataReceived += new SerialDataReceivedEventHandler(clsSerial_DataReceived);
        }

        /// <summary>
        /// 수신 후 대기시간 : 신호가 끊어 들어오는 방지
        /// </summary>
        public int intRecieveWatingTime = 100;
        //public byte bytEtx = 0x03;// Encoding.Default.GetString(new byte[] { 0x03 });	//etx, cr, lf
        string strAck = string.Empty;
        string strEtx = Encoding.Default.GetString(new byte[] { 0x03 });    //etx, cr, lf
        private void clsSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Thread.Sleep(intRecieveWatingTime);
                int intLength = clsSn.BytesToRead;

                //if (inStr(strAck, strEtx) < 3)
                //    return;

                //if (intLength < 1) return;

                byte[] bytReceive = new byte[intLength];
                clsSn.Read(bytReceive, 0, intLength);

                //strAck += encoding.GetString(bytData);

                //if (inStr(strAck, strEtx) < 1)
                //    return;

                if (evtReceived != null)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(RaiseEvent), bytReceive);
                }

                if (evtReceivedPort != null)
                    ThreadPool.QueueUserWorkItem(new WaitCallback(RaiseEvent), bytReceive);
            }
            catch(Exception err)
            {
                
            }
        }

        /// <summary>
        /// 문자열에 포함되어 있는 검색 문자 수를 리턴한다.
        /// </summary>
        /// <param name="strData">문자열</param>
        /// <param name="strFind">검색할 문자</param>
        /// <returns></returns>
        public static int inStr(string strData, string strFind)
        {
            int intCnt = 0;
            int intResult = 0;

            while (intResult >= 0 && (intResult + 1) < strData.Length)
            {
                intResult = strData.IndexOf(strFind, intResult + 1);
                if (intResult > 0) intCnt++;
            }

            return intCnt;
        }

        private void RaiseEvent(object obj)
        {
            if (evtReceived != null)
            {
                byte[] bytData = (byte[])obj;
                evtReceived(bytData);
            }
            if(evtReceivedPort !=null)
            {
                byte[] bytData = (byte[])obj;
                evtReceivedPort(bytData, PortName);
            }
        }


        /// <summary>
        /// Parity Enum 형태로 변환
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Parity String2Parity(string value)
        {
            Parity enParity = Parity.None;

            value = value;

            Type type = enParity.GetType();


            foreach (Parity e in Enum.GetValues(type))   //Parity.GetType()))
            {
                if (value == e.ToString())
                {
                    enParity = e;
                    break;
                }
            }


            return enParity;

        }

        /// <summary>
        /// StopBit Enum 형태로 변환
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StopBits String2stopBits(string value)
        {
            StopBits enStopBits = StopBits.One;

            value = value;

            Type type = enStopBits.GetType();


            foreach (StopBits e in Enum.GetValues(type))   //Parity.GetType()))
            {
                if (value == e.ToString())
                {
                    enStopBits = e;
                    break;
                }
            }


            return enStopBits;

        }

        /// <summary>
        /// 클래스를 해제 한다.
        /// </summary>
        public void Dispose()
        {
            Close();
            clsSn.Dispose();
        }
    }
}


