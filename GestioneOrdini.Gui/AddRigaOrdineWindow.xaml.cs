using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using GestioneOrdini.Cl;
using GestioneOrdini.Gui.Messages;


namespace GestioneOrdini.Gui
{
    /// <summary>
    /// Interaction logic for AddRigaOrdineWindow.xaml
    /// </summary>
    public partial class AddRigaOrdineWindow : Window
    {
        public RigaOrdine RigaOrdine { get; private set; }

        public AddRigaOrdineWindow()
        {
            InitializeComponent();
            RigaOrdine = null;
            Messenger.Default.Register<AddRigaMessage>(this, HandleAddRigaMessage);
        }

        private void HandleAddRigaMessage(AddRigaMessage m)
        {
            switch (m.Key)
            {
                case AddRigaMessage.CancelMessage:
                    Cancel();
                    break;
                case AddRigaMessage.AddMessage:
                    Add((RigaOrdine)m.Parameter);
                    break;
            }
        }

        private void Cancel()
        {
            this.Close();
        }

        private void Add(RigaOrdine rigaOrdine)
        {
            RigaOrdine = rigaOrdine;
            this.Close();
        }
    }
}
