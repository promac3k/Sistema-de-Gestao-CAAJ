using System;
using System.Windows.Forms;

namespace CAAJ
{
    internal static class Program
    {
        internal static bool usuarioAdmin { get; set; }
        internal static bool usuario { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            usuarioAdmin = false;
            usuario = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
            if (usuarioAdmin || usuario)
            {
                Application.Run(new MainForm());
            }
        }
    }
}