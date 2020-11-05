using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youtubeLiblary
{
    public partial class ListManage : Form
    {
        public delegate void FormSendDataHandler(object sendobject);
        public event FormSendDataHandler FormSendEvent;

        public ListManage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListInsert listInsert = new ListInsert();
            FormSendEvent(listInsert);
            listInsert.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListUpdate listUpdate = new ListUpdate();
            FormSendEvent(listUpdate);
            listUpdate.Show();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListDelete listDelete = new ListDelete();
            FormSendEvent(listDelete);
            listDelete.Show();
            Close();
        }
    }
}
