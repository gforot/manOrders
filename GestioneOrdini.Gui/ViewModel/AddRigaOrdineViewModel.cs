using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GestioneOrdini.Cl;
using GestioneOrdini.Gui.Messages;


namespace GestioneOrdini.Gui.ViewModel
{
    public class AddRigaOrdineViewModel : ViewModelBase
    {
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private string _cliente;
        public string Cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                _cliente = value;
                RaisePropertyChanged("Cliente");
            }
        }

        private string _marca;
        public string Marca
        {
            get
            {
                return _marca;
            }
            set
            {
                _marca = value;
                RaisePropertyChanged("Marca");
            }
        }


        private string _descrizione;
        public string Descrizione
        {
            get
            {
                return _descrizione;
            }
            set
            {
                _descrizione = value;
                RaisePropertyChanged("Descrizione");
            }
        }

        private DateTime _dataOrdine;
        public DateTime DataOrdine
        {
            get
            {
                return _dataOrdine;
            }
            set
            {
                _dataOrdine = value;
                RaisePropertyChanged("DataOrdine");
            }
        }

        private int _stato;
        public int Stato
        {
            get
            {
                return _stato;
            }
            set
            {
                _stato = value;
                RaisePropertyChanged("Stato");
            }
        }

        public AddRigaOrdineViewModel()
        {
            AddCommand = new RelayCommand(Add);
            CancelCommand = new RelayCommand(Cancel);
            Cliente = string.Empty;
            Marca = string.Empty;
            Descrizione = string.Empty;
            DataOrdine = DateTime.Now;
            Stato = 1;
        }

        private void Cancel()
        {
            MessengerInstance.Send(new AddRigaMessage(AddRigaMessage.CancelMessage));            
        }

        private void Add()
        {
            MessengerInstance.Send(new AddRigaMessage(AddRigaMessage.AddMessage, new RigaOrdine()
                                                                  {
                                                                    Id = -1,
                                                                    Cliente = Cliente,
                                                                    Descrizione = Descrizione,
                                                                    DataOrdine = DataOrdine,
                                                                    Stato = Stato,
                                                                    Marca = Marca
                                                                  }));
        }

    }
}
