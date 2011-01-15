using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PewClient
{
    public partial class GetPass : Form
    {
        public string Pass = string.Empty;
        public GetPass()
        {
            InitializeComponent();
            Password.Text = Pass;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Pass = Password.Text;
        }
    }
}
