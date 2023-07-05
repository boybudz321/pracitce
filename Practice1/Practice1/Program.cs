using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Practice1.Model;
using Practice1._Repositories;
using Practice1.View;
using Practice1.Presenter;
using System.Configuration;


namespace Practice1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MainView view = new FMainView(); 
            new MainPresenter(view, sqlConnectionString);
            Application.Run((Form)view);
        }
    }
}
