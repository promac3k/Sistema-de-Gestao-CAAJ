using CAAJ.Utilitarios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CAAJ
{
    public partial class FormLogin : Form
    {
        internal Utilitarios.DB ligacao;
        internal static RichTextBox LOG;
        internal Usuario Usuario;

        // para arrastar o UI
        private bool mouseDown;

        private Point lastLocation;

        public FormLogin()
        {
            InitializeComponent();
            LOG = richTextBox1;
            ligacao = new DB();
            if (ligacao.Ligado())
            {
                LOG.Text += "Connection Done...\n";
            }
            Usuario = new Usuario(ligacao);
            txtUsuario.Select();
        }



        private void BtnLoggin_Click_1(object sender, EventArgs e)
        {
            var admin = -1;
            admin = Usuario.BuscarUsuario(txtUsuario.Text, txtSenha.Text);
            //Console.WriteLine(">>>>>>>> " + admin);
            if (admin == 1)
            {
                Program.usuarioAdmin = true;
                ligacao.Desligado();
                this.Close();
            }
            if (admin == 0)
            {
                Program.usuarioAdmin = false;
                Program.usuario = true;
                ligacao.Desligado();
                this.Close();
            }
            if (admin != 0 || admin != 1)
            {
                MessageBox.Show("O login esta incorreto");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Arrastar a UI
        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void FormLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void FormLogin_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}