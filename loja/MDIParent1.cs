using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace loja
{
    public partial class MDIParent1 : Form
    {

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void UsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childform = new Cadastro_usuario();
            childform.MdiParent = this;
            childform.Show();
        }
    }
}
