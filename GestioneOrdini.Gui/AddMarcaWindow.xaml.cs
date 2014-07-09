using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using GestioneOrdini.Gui.Messages;


namespace GestioneOrdini.Gui
{
    /// <summary>
    /// Interaction logic for AddMarcaWindow.xaml
    /// </summary>
    public partial class AddMarcaWindow : Window
    {
        public string Marche { get; set; }

        public AddMarcaWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<AddMarcheMessage>(this, HandleAddMarcheMessage);
        }

        private void HandleAddMarcheMessage(AddMarcheMessage m)
        {
            switch (m.Key)
            {
                case AddMarcheMessage.CancelMessage:
                    Cancel();
                    break;
                case AddMarcheMessage.AddMessage:
                    Add((string)m.Parameter);
                    break;
            }
        }

        private void Cancel()
        {
            Marche = null;
            this.Close();
        }

        private void Add(string marche)
        {
            Marche = marche;
            this.Close();
        }


    }
}
