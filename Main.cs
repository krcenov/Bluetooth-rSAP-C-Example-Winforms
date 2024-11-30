using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using static Bluetooth_rSAP.Packeter;
using static Bluetooth_rSAP.Packets;
using static Bluetooth_rSAP.Dict;
using static Bluetooth_rSAP.HelperFuncs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;
using System.Linq;
using System.Text;

namespace Bluetooth_rSAP
{
    public partial class Main : Form
    {
        BluetoothDeviceInfo selectedDevice;
        Guid rsapUuid = new Guid("0000112d-0000-1000-8000-00805f9b34fb");
        NetworkStream stream;
        BluetoothClient client;

        public Main()
        {
            InitializeComponent();
        }

        // Button click event to connect to a paired Bluetooth device
        private void button1_Click(object sender, EventArgs e)
        {
            client = new BluetoothClient();
            var devices = client.PairedDevices;

            foreach (var device in devices)
            {
                Debug.WriteLine($"Device: {device.DeviceName}, Address: {device.DeviceAddress}");
                if (device.DeviceName.Contains(Phone_Name_TB.Text))
                {
                    selectedDevice = device;
                    break;
                }
            }

            if (selectedDevice == null)
            {
                Debug.WriteLine("Device not found.");
                return;
            }

            try
            {
                Debug.WriteLine("Connecting to device...");
                client.Connect(selectedDevice.DeviceAddress, rsapUuid);
                if (client.Connected)
                {
                    Debug.WriteLine("Connected to rSAP service!");
                    Debug.WriteLine("connectionRequest");
                    sendCommand(Pack(connectionReq));
                    readResponse();
                    readResponse();
                    Debug.WriteLine("answerToReset");
                    sendCommand(Pack(answerToReset));
                    readResponse();
                }
                else
                {
                    Debug.WriteLine("Failed to connect to rSAP service.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
            changestate();
        }
        void GetStream()
        {
            stream = client.GetStream();
        }
        // Button click event to get contact from SIM phonebook
        byte[]? readResponse(bool ShowDebug = true)
        {
            byte[] response = new byte[256];
            try
            {
                int bytesRead = stream.Read(response, 0, response.Length);
                if (bytesRead <= 0)
                {
                    Debug.WriteLine("No response received from SIM.");
                    return null;
                }
                if (ShowDebug)
                {
                    Communication_TB.AppendText("Response : " + BitConverter.ToString(response, 0, bytesRead) + Environment.NewLine);
                    Debug.WriteLine("Response : " + BitConverter.ToString(response, 0, bytesRead));
                }
                byte[] toReturn = new byte[bytesRead];
                Buffer.BlockCopy(response, 0, toReturn, 0, bytesRead);
                return toReturn;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No Reponse");
            }
            return null;
        }
        void sendCommand(byte[] command, bool debug = true)
        {
            if (client == null) return;
            if (!client.Connected)
            {
                Debug.WriteLine("Failed to connect or device is not connected.");
                return;
            }
            GetStream();
            if (stream != null)
            {
                stream.ReadTimeout = 5000;
                if (debug)
                {
                    Debug.WriteLine("Sending command...");
                    Debug.WriteLine("Sending : " + BitConverter.ToString(command, 0, command.Length));
                    Communication_TB.AppendText("Sending : " + BitConverter.ToString(command, 0, command.Length) + Environment.NewLine);
                }
                stream.Write(command, 0, command.Length);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Packet apduRequestIMSI = new Packet()
            {
                header = new Header()
                {
                    MsgId = MessageID.TRANSFER_APDU_REQ
                },
                Parameters = new List<Parameter>
                {
                    new Parameter()
                    {
                        ParId = ParameterID.CommandAPDU,
                        ParValue = Convert.FromHexString(Custom_APDU_Command_TB.Text.Replace(" ",""))
                    }
                }
            };
            Debug.WriteLine("apduRequestIMSI");
            sendCommand(Pack(apduRequestIMSI));
            readResponse();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Communication_TB.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            client.Close();
            changestate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Select MF");
            sendCommand(Pack(adpuSelectMF));
            readResponse();
            Debug.WriteLine("Select ICCID");
            sendCommand(Pack(adpuSelectICCID));
            readResponse();
            sendCommand(readBinary(10));
            byte[] res = readResponse();
            List<Parameter> parameters = GetParameters(res);
            Parameter responseAPDU = GetResponseAPDUParameter(parameters);
            byte[] APDU = GetValueOfAPDUParameter(responseAPDU);
            ReverseBitsInBytes(APDU);
            ICCID_TB.Text = ByteArrToHexString(APDU); ;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            int maxContacts = 250;
            Get_Contacts_PB.Maximum = maxContacts;
            Get_Contacts_PB.Value = 0;
            Contacts_LV.Items.Clear();
            Debug.WriteLine("Select MF");
            sendCommand(Pack(adpuSelectMF));
            readResponse();
            Debug.WriteLine("Select TELECOM");
            sendCommand(Pack(adpuSelectTELECOM));
            readResponse();
            Debug.WriteLine("Select EFADN");
            sendCommand(Pack(adpuSelectEFADN));
            readResponse();
            sendCommand(verifyPIN("0000"));
            readResponse();
            List<Contact> contacts = new List<Contact>();
            byte len = 0x00;
            for (int i = 1; i <= maxContacts; i++)
            {
                while (true)
                {
                    sendCommand(readRecord(i, len), false);
                    byte[] res = readResponse(false);
                    List<Parameter> parameters = GetParameters(res);
                    Parameter responseAPDU = GetResponseAPDUParameter(parameters);
                    if (responseAPDU == null) break;
                    string resCode = GetResponseCodeFromAPDUParameter(responseAPDU);
                    if (resCode != "9000" && resCode.Contains("67"))
                    {
                        len = Convert.ToByte(resCode.Replace("67", ""), 16);
                        continue;
                    }
                    responseAPDU.ParValue = RemoveLastBytes(responseAPDU.ParValue, 2);
                    contacts.Add(EFADN.Decode(responseAPDU.ParValue));
                    break;
                }
                Get_Contacts_PB.Increment(1);
            }
            Contacts_LV.View = View.Details;
            Contacts_LV.Columns.Add("Name", 200);
            Contacts_LV.Columns.Add("Phone", 100);
            foreach (var contact in contacts)
            {
                if (contact.Name == "") continue;
                ListViewItem item = new ListViewItem(contact.Name);
                item.SubItems.Add(contact.Phone);  // Add the Phone as a sub-item
                Contacts_LV.Items.Add(item);
            }
            Get_Contacts_PB.Value = 0;
        }
        void changestate()
        {
            bool state = false;
            if(client != null)
                if (client.Connected) state = true;
            Disconnect_BTN.Enabled = state;
            Custom_APDU_Command_BTN.Enabled = state;
            Custom_APDU_Command_TB.Enabled = state;
            Get_ICCID_BTN.Enabled = state;
            ICCID_TB.Enabled = state;
            Get_Contacts_BTN.Enabled = state;
            Contacts_LV.Enabled = state;
            Phone_Name_TB.Enabled = !state;
            Connect_BTN.Enabled = !state;
        }
    }
}
