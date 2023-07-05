using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice1.View
{
    public partial class FMainView : Form, MainView
    {
        public FMainView()
        {
            InitializeComponent();
            btnPetsView.Click += delegate { ShowPetView?.Invoke(this, EventArgs.Empty); };

        }

        public event EventHandler ShowPetView;
        public event EventHandler ShowOwnerView;
        public event EventHandler ShowVetsView;
    }
}
