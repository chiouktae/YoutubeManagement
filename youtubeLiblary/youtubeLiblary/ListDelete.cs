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
    public partial class ListDelete : Form
    {
        public delegate void FormSendDataHandler(object sendobject);
        public event FormSendDataHandler FormSendEvent;

        public ListDelete()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSendEvent(textBox1.Text);
            Close();
        }
    }
}
