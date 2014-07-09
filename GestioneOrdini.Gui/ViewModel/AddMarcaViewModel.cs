using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GestioneOrdini.Gui.Messages;


namespace GestioneOrdini.Gui.ViewModel
{
    public class AddMarcaViewModel : ViewModelBase
    {
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private const string _marchePrpName="Marche";
        private string _marche;
        public string Marche
        {
            get
            {
                return _marche;
            }
            set
            {
                _marche = value;
                RaisePropertyChanged(_marchePrpName);
            }
        }

        private const string _helpMessageDefault = "Inserire le marche da aggiungere separate da ';'";

        private const string _helpMessagePrpName = "HelpMessage";
        private string _helpMessage;
        public string HelpMessage
        {
            get
            {
                return _helpMessage;
            }
            set
            {
                _helpMessage = value;
                RaisePropertyChanged(_helpMessagePrpName);
            }
        }

        public AddMarcaViewModel()
        {
            HelpMessage = _helpMessageDefault;

            AddCommand = new RelayCommand(Add);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Add()
        {
            MessengerInstance.Send(new AddMarcheMessage(AddMarcheMessage.AddMessage, Marche));  
        }

        private void Cancel()
        {
            MessengerInstance.Send(new AddMarcheMessage(AddMarcheMessage.CancelMessage));
        }

    }
}
