using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serial_port_Control
{
    public partial class frmSerial : Form
    {
        public delegate void myDelegate();
        public frmSerial()
        {
            InitializeComponent();
        }//frmSerial
        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e) {
            richTextBox1.BeginInvoke(new myDelegate(updateTextBox));
        }//DataReceived
        private void frmSerial_Load(object sender, EventArgs e)
        {
            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(DataReceived);
            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            cmboPorts.Items.AddRange(portNames);
            btnDisconnect.Enabled = false;
        }//frmSerial_Load
        public void updateTextBox() {
            richTextBox1.AppendText(serialPort1.ReadExisting());
            richTextBox1.ScrollToCaret();
        }//updateTextBox

        private void btnConncect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) {
                serialPort1.Close();
            }//if it is open
            try
            {
                serialPort1.PortName = cmboPorts.Text;
                serialPort1.BaudRate = 9600;
                serialPort1.Parity = System.IO.Ports.Parity.None;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = System.IO.Ports.StopBits.One;
                serialPort1.Open();
                richTextBox1.Text = cmboPorts.Text + "Connected";
                btnConncect.Enabled = false;
                btnDisconnect.Enabled = true;
            }//try
            catch (Exception ex)
            {
                string strmessage, strtitle;
                strtitle = "Sender & Receiver";
                strmessage = "An exception with the following message has occured \n\t" + ex.Message;
                MessageBox.Show(strmessage, strtitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//catch
        }//btnConncect_Click

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                richTextBox1.Text = serialPort1.PortName + " disconnected";
                btnConncect.Enabled = true;
                btnDisconnect.Enabled = false;           
            }//try
            catch (Exception ex)
            {
                string strmessage, strtitle;
                strtitle = "Sender & Receiver";
                strmessage = "An exception with the following message has occured \n\t" + ex.Message;
                MessageBox.Show(strmessage, strtitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//catch
        }//btnDisconnect_Click

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write(txtSend.Text + Environment.NewLine);
                richTextBox1.AppendText(">" + txtSend.Text + Environment.NewLine);
                richTextBox1.ScrollToCaret();
                txtSend.Clear();
            }//try
            catch (Exception ex)
            {
                string strmessage, strtitle;
                strtitle = "Sender & Receiver";
                strmessage = "An exception with the following message has occured \n\t" + ex.Message;
                MessageBox.Show(strmessage, strtitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//catch
        }//btnSend_Click
    }//public partial class frmSerial : Form
}//Serial_port_Control
