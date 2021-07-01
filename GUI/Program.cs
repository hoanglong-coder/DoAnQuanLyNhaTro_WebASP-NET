using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
namespace GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmDangNhap());
        }
    }
}
