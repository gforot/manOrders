using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
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

        private int _id;

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

        private Marca _marca;
        public Marca Marca
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

        public List<Marca> Marche { get; set; }


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

        private string _telefono;
        public string Telefono
        {
            get
            {
                return _telefono;
            }
            set
            {
                _telefono = value;
                RaisePropertyChanged("Telefono");
            }
        }

        private DateTime? _dataOrdine;
        public DateTime? DataOrdine
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
            AddCommand = new RelayCommand(Add, () => CanAdd);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void Setup()
        {
            using (GestOrdiniDataContext db = new GestOrdiniDataContext())
            {
                Marche = db.GetMarche();
            }

            if (GestioneOrdini.Gui.App.CurrentRigaOrdine == null)
            {
                Cliente = string.Empty;
                Marca = null;
                Descrizione = string.Empty;
                DataOrdine = DateTime.Now;
                Stato = 1;
                Telefono = string.Empty;
                _id = -1;
            }
            else
            {
                Marca = Marche.First(m => m.Nome.Equals(App.CurrentRigaOrdine.Marca));
                Cliente = App.CurrentRigaOrdine.Cliente;
                Descrizione = App.CurrentRigaOrdine.Descrizione;
                DataOrdine = App.CurrentRigaOrdine.DataOrdine;
                Stato = App.CurrentRigaOrdine.Stato;
                Telefono = App.CurrentRigaOrdine.Telefono;
                _id = App.CurrentRigaOrdine.Id;
            }


        }

        private void Cancel()
        {
            MessengerInstance.Send(new AddRigaMessage(AddRigaMessage.CancelMessage));            
        }

        private void Add()
        {
            MessengerInstance.Send(new AddRigaMessage(AddRigaMessage.AddMessage, new RigaOrdine
                                                                  {
                                                                    Id = _id,
                                                                    Cliente = Cliente,
                                                                    Descrizione = Descrizione,
                                                                    DataOrdine = DataOrdine,
                                                                    Stato = Stato,
                                                                    Marca = Marca.Nome,
                                                                    Telefono = Telefono,
                                                                  }));
        }

        private bool CanAdd
        {
            get
            {
                return (this.Marca != null) &&
                    (!string.IsNullOrEmpty(this.Cliente)) &&
                    (!string.IsNullOrEmpty(this.Descrizione));
            }
        }

    }
}
