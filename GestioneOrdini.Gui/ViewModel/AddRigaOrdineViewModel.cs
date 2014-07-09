﻿using System;
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
            AddCommand = new RelayCommand(Add);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void Setup()
        {
            if (GestioneOrdini.Gui.App.CurrentRigaOrdine == null)
            {
                Cliente = string.Empty;
                Marca = string.Empty;
                Descrizione = string.Empty;
                DataOrdine = DateTime.Now;
                Stato = 1;
                Telefono = string.Empty;
                _id = -1;
            }
            else
            {
                Cliente = App.CurrentRigaOrdine.Cliente;
                Marca = App.CurrentRigaOrdine.Marca;
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
                                                                    Marca = Marca,
                                                                    Telefono = Telefono,
                                                                  }));



        }

    }
}
