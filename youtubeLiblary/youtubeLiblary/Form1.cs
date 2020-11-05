using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youtubeLiblary
{
    public partial class Form1 : Form
    {
        ListManage listManage;
        ListInsert listInsert;
        ListUpdate listUpdate;
        ListDelete listDelete;

        int listCount = 0;
        int listnum = 0;
        List<List<object>> objlist = new List<List<object>>();
        public Form1()
        {
            InitializeComponent();
        }

        private void listManageButton_Click(object sender, EventArgs e)
        {
            listManage = new ListManage();
            listManage.FormSendEvent += new ListManage.FormSendDataHandler(ListManageRegister);
            listManage.Show();
        }

        private void ListInsertWork(object sender)
        {
            string[] result = sender.ToString().Split(new char[] { ',' });
            List<object> panellist = new List<object>();

            Panel panel = new Panel();
            Label mlabel1 = new Label();
            Label mlabel2 = new Label();
            Label mlabel3 = new Label();
            PictureBox pictureBox = new PictureBox();

            panellist.Add(panel);
            panellist.Add(mlabel3);
            panellist.Add(mlabel1);
            panellist.Add(mlabel2);
            panellist.Add(pictureBox);

            objlist.Add(panellist);
            //panel
            panel.SuspendLayout();
            panel1.Controls.Add(panel);
            panel.BackColor = Color.White;
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(mlabel2);
            panel.Controls.Add(mlabel3);
            panel.Controls.Add(mlabel1);
            panel.Location = new Point(0, 40+listCount*64);
            panel.Name = result[0];
            panel.Size = new Size(448, 64);
            panel.TabIndex = 3+ listCount;
            panel.Click += new EventHandler(panel_Click);
            //Category label
            mlabel1.AutoSize = true;
            mlabel1.Location = new Point(56, 24);
            mlabel1.Name = "Mlabel1-" + listCount.ToString();
            mlabel1.Size = new Size(53, 12);
            mlabel1.TabIndex = 1;
            mlabel1.Text = result[1];
            //title label
            mlabel2.AutoSize = true;
            mlabel2.Location = new Point(264, 24);
            mlabel2.Name = "Mlabel2-" + listCount.ToString();
            mlabel2.Size = new Size(29, 12);
            mlabel2.TabIndex = 3;
            mlabel2.Text = result[2];
            //index label
            mlabel3.AutoSize = true;
            mlabel3.Location = new Point(16, 24);
            mlabel3.Name = "Mlabel3-" + listCount.ToString(); ;
            mlabel3.Size = new Size(29, 12);
            mlabel3.TabIndex = 2;
            mlabel3.Text = listnum.ToString();
            //pickturebox
            ((ISupportInitialize)(pictureBox)).BeginInit();
            pictureBox.Image = getthumnnail(result[0]);
            pictureBox.Location = new Point(120, 8);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Name = "MpictureBox-" + listCount.ToString();
            pictureBox.Size = new Size(100, 50);
            pictureBox.TabIndex = 4;
            pictureBox.TabStop = false;
            ((ISupportInitialize)(pictureBox)).EndInit();

            listCount++;
            listnum++;
            label9.Text = listCount.ToString();
        }
        private void ListUpdateWork(object sender)
        {
            string[] result = sender.ToString().Split(new char[] { ',' });
            foreach (var obj in objlist)
            {
                if (((Label)obj[1]).Text == result[0]) 
                {
                    ((Panel)obj[0]).Name = result[1];
                    ((Label)obj[2]).Text = result[3];
                    ((Label)obj[3]).Text = result[2];
                    ((PictureBox)obj[4]).Image = getthumnnail(result[1]);
                }
            }
        }
        private void ListDeleteWork(object sender)
        {
            int i;
            for (i = 0; i < objlist.Count; i++)
            {
                if (((Label)objlist[i][1]).Text == sender.ToString())
                {
                    panel1.Controls.Remove((Panel)objlist[i][0]);
                    objlist.RemoveAt(i);
                    break;
                }
            }
            for (; i < objlist.Count; i++)
            {
                ((Panel)objlist[i][0]).Location = new Point(0,40+64*i);
            }
            listCount--;
            label9.Text = listCount.ToString();
        }
        private void panel_Click(object sender, EventArgs e)
        {
            Process.Start(((Panel)sender).Name);
        }
        private Image getthumnnail(string URL)
        {
            try
            {
                string[] result = URL.Split(new char[] { '=' });
                WebClient Downloader = new WebClient();
                Stream ImageStream = Downloader.OpenRead("https://img.youtube.com/vi/"+result[1]+"/0.jpg");
                Image DownloadImage = Image.FromStream(ImageStream);
                
                return DownloadImage;
            }
            catch(Exception e)
            {
                ErrorForm errorForm = new ErrorForm();
                errorForm.Show();
                errorForm.setMessege(e.Message);
                return null;
            }
        }

        private void ListManageRegister(object sender)
        {
            if (sender.GetType() == typeof(ListInsert))
            {
                listInsert = (ListInsert)sender;
                listInsert.FormSendEvent += new ListInsert.FormSendDataHandler(ListInsertWork);
            }
            else if(sender.GetType() == typeof(ListUpdate))
            {
                listUpdate = (ListUpdate)sender;
                listUpdate.FormSendEvent += new ListUpdate.FormSendDataHandler(ListUpdateWork);
            }
            else if(sender.GetType() == typeof(ListDelete))
            {
                listDelete = (ListDelete)sender;
                listDelete.FormSendEvent += new ListDelete.FormSendDataHandler(ListDeleteWork);
            }
            else
            {
                ErrorForm errorForm = new ErrorForm();
                errorForm.Show();
                errorForm.setMessege("error");
            }
        }
    }
}
