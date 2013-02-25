using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceProcess;
using System.Diagnostics;
using System.IO;
using CommonTool;
using System.Threading;

namespace ParseDataTestUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UDPListener m_udpListener;
        private delegate void MessageHandle(DREventArgs e);
        private delegate void MsgEventHandler(object sender, DREventArgs e);
        
        private bool m_bQuitThread;
        private ManualResetEvent m_eQuitThreadEvent;
        
        private string m_strRec;

        public MainWindow()
        {
            InitializeComponent();

            //Initial UDP listener
            m_udpListener = UDPListener.GetInstance(GShare.cnPortForUI);
            m_udpListener.MsgRecved += new UDPListener.MessageReceivedHandler(UdpMessageReceived);
            m_udpListener.InitialListener();

            m_strRec = "";
            
            //Watching Thread
            m_bQuitThread = false;
            m_eQuitThreadEvent = new ManualResetEvent(false);

        }
        private void ClickInstallParseDataServiceButton(object sender, RoutedEventArgs e)
        {
            //Backup system parameter
            string CurrentDirectory = System.Environment.CurrentDirectory;

            //Create a process to install service
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Services";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "ParseDataInstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            //Restore parameter
            System.Environment.CurrentDirectory = CurrentDirectory;
        }
        private void ClickUninstallParseDataServiceButton(object sender, RoutedEventArgs e)
        {
            //Backup system parameter
            string CurrentDirectory = System.Environment.CurrentDirectory;

            //Create a process to install service
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Services";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "ParseDataUnInstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            //Restore parameter
            System.Environment.CurrentDirectory = CurrentDirectory;
        }
        private void ClickStartParseDataServiceButton(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("ParseData");
            if(IsSvrExist("ParseData") && sc.Status == ServiceControllerStatus.Stopped)
                sc.Start();
        }
        private void ClickStopParseDataServiceButton(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("ParseData");
            if( IsSvrExist("ParseData") && sc.Status == ServiceControllerStatus.Running )
                sc.Stop();
        }
        private void ClickPauseResumeParseDataServiceButton(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("ParseData");
            if (IsSvrExist("ParseData") && sc.CanPauseAndContinue)
            {
                if (sc.Status == ServiceControllerStatus.Running)
                    sc.Pause();
                else if (sc.Status == ServiceControllerStatus.Paused)
                    sc.Continue();
            }
        }
        private void ClickCheckParseDataServiceStatusButton(object sender, RoutedEventArgs e)
        {
            string Status;
            if (IsSvrExist("ParseData"))
            {
                ServiceController sc = new ServiceController("ParseData");
                Status = sc.Status.ToString();
            }
            
        }
        private void ClickInstallTaskManagerServiceButton(object sender, RoutedEventArgs e)
        {
            //Backup system parameter
            string CurrentDirectory = System.Environment.CurrentDirectory;

            //Create a process to install service
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Services";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "TaskManagerInstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            //Restore parameter
            System.Environment.CurrentDirectory = CurrentDirectory;
        }
        private void ClickUninstallTaskManagerServiceButton(object sender, RoutedEventArgs e)
        {
            //Backup system parameter
            string CurrentDirectory = System.Environment.CurrentDirectory;

            //Create a process to install service
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Services";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "TaskManagerUnInstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            //Restore parameter
            System.Environment.CurrentDirectory = CurrentDirectory;
        }
        private void ClickStartTaskManagerServiceButton(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("TaskManager");
            if (IsSvrExist("TaskManager") && sc.Status == ServiceControllerStatus.Stopped)
                sc.Start();
        }
        private void ClickStopTaskManagerServiceButton(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("TaskManager");
            if (IsSvrExist("TaskManager") && sc.Status == ServiceControllerStatus.Running)
                sc.Stop();
        }
        private void ClickPauseResumeTaskManagerServiceButton(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("TaskManager");
            if (IsSvrExist("TaskManager") && sc.CanPauseAndContinue)
            {
                if (sc.Status == ServiceControllerStatus.Running)
                    sc.Pause();
                else if (sc.Status == ServiceControllerStatus.Paused)
                    sc.Continue();
            }
        }
        private void ClickCheckTaskManagerServiceStatusButton(object sender, RoutedEventArgs e)
        {
            string Status;
            if (IsSvrExist("TaskManager"))
            {
                ServiceController sc = new ServiceController("TaskManager");
                Status = sc.Status.ToString();
            }

        }
        private void ClickInstallRoundingAnalyseServiceButton(object sender, RoutedEventArgs e)
        {
            //Backup system parameter
            string CurrentDirectory = System.Environment.CurrentDirectory;

            //Create a process to install service
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Services";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "RoundingAnalyseInstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            //Restore parameter
            System.Environment.CurrentDirectory = CurrentDirectory;
        }
        private void ClickUninstallRoundingAnalyseServiceButton(object sender, RoutedEventArgs e)
        {
            //Backup system parameter
            string CurrentDirectory = System.Environment.CurrentDirectory;

            //Create a process to install service
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Services";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "RoundingAnalyseUnInstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            //Restore parameter
            System.Environment.CurrentDirectory = CurrentDirectory;
        }
        private void ClickStartRoundingAnalyseServiceButton(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("RoundingAnalyse");
            if (IsSvrExist("RoundingAnalyse") && sc.Status == ServiceControllerStatus.Stopped)
                sc.Start();
        }
        private void ClickStopRoundingAnalyseServiceButton(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("RoundingAnalyse");
            if (IsSvrExist("RoundingAnalyse") && sc.Status == ServiceControllerStatus.Running)
                sc.Stop();
        }
        private void ClickPauseResumeRoundingAnalyseServiceButton(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("RoundingAnalyse");
            if (IsSvrExist("RoundingAnalyse") && sc.CanPauseAndContinue)
            {
                if (sc.Status == ServiceControllerStatus.Running)
                    sc.Pause();
                else if (sc.Status == ServiceControllerStatus.Paused)
                    sc.Continue();
            }
        }
        private void ClickCheckRoundingAnalyseServiceStatusButton(object sender, RoutedEventArgs e)
        {
            string Status;
            if (IsSvrExist("RoundingAnalyse"))
            {
                ServiceController sc = new ServiceController("RoundingAnalyse");
                Status = sc.Status.ToString();
            }
        }
        private bool IsSvrExist(string name)
        {
            ServiceController[] scs;
            scs = ServiceController.GetServices();

            foreach (ServiceController sct in scs)
            {
                if (sct.ServiceName == name)
                    return true;
            }

            return false;
        }
        private void ClickParseKLineDayButton(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(".\\Config\\ImportKLineData_Day.xml");
            string strXml = sr.ReadToEnd();

            WebSvr ws = new WebSvr(GShare.cstIPAddress, GShare.cnPortForTaskManager);
            ws.SendMessage(strXml);

            MsgReceived.Text = "Waiting for response...";
            ThreadPool.QueueUserWorkItem(new WaitCallback(MsgWatchingThread));

            return;
        }
        public void MsgWatchingThread(object state)
        {
            while(!m_bQuitThread)
            {
                if (m_strRec == null || m_strRec.Length == 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                RequestParameter rp = new RequestParameter(m_strRec);
                MsgReceived.Text = rp.m_strMsg;
                if (rp.m_enJobStatus == GShare.JobStatus.eEnd)
                    m_bQuitThread = true;

                Thread.Sleep(1000);
            }

            m_eQuitThreadEvent.Set();
        }
        public void UdpMessageReceived(object sender, DREventArgs e)
        {
            m_strRec = e.msg;
        }
    }
}
