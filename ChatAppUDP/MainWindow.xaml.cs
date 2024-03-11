using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;
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


namespace ChatAppUDP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string ipAddress;
        static int port;
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
             ipAddress = IpTextBox.Text;
            
            if (int.TryParse(PortTextBox.Text, out port))
            {
                using (var db = new AppContext())
                {
                    var connectionInfo = new ConnectionInfo
                    {
                        IpAddress = ipAddress,
                        Port = port
                    };

                    db.ConnectionInfos.Add(connectionInfo);
                    db.SaveChanges();
                }

                MessageBox.Show("Success");
             Stackpanel.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Incorrect data.");
            }


        }


        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var ip = IPAddress.Parse(ipAddress);

            var connectEP = new IPEndPoint(ip, port);
            var str = MessageTextBox.Text;
            var bytes = Encoding.Default.GetBytes(str);
            clientSocket.SendTo(bytes, connectEP);
            
            ChatListBox.Items.Add(str);
            MessageTextBox.Text = "";
        }
        }
    }
