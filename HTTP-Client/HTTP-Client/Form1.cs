using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;

namespace HTTP_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            methodsComBox.SelectedIndex = 0;
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            string url = textBox2.Text;
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(url, 80);
            NetworkStream stream = tcpClient.GetStream();
            string request =
                $"{methodsComBox.SelectedItem} / HTTP/1.0\r\n" +
                $"Host: {url}\r\n" +
                "\r\n";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(request);
            stream.Write(data, 0, data.Length);
            textBox1.Text += request;
            data = new byte[1024];
            int bytes;
            string response = "";
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                response += Encoding.UTF8.GetString(data, 0, bytes);
            } while (stream.DataAvailable);
            tcpClient.Close();
            textBox1.Text += response;

        }
    }
}
