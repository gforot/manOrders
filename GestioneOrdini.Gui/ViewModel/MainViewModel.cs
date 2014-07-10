using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GestioneOrdini.Cl;
using Microsoft.Practices.ServiceLocation;


namespace GestioneOrdini.Gui.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand UpdateCommand { get; private set; }
        public RelayCommand RitiratoCommand { get; private set; }
        public RelayCommand AvvisatoCommand { get; private set; }
        public RelayCommand AddMarcheCommand { get; private set; }
        public RelayCommand FilterCommand { get; private set; }
        public RelayCommand CancelFilterCommand { get; private set; }
        public RelayCommand RemoveMarcaFilterCommand { get; private set; }
        public RelayCommand RemoveClienteFilterCommand { get; private set; }

        public string AppTitle
        {   
            get
            {
                return "Gestione Ordini";
            }
        }

        public ObservableCollection<RigaOrdine> RigheOrdine { get; set; }

        public ICollectionView RigheOrdineCollectionView { get; private set; }

        public object SelectedItem { get; set; }

        private const string _filterClientePrpName = "FilterCliente";
        private string _filterCliente;
        public string FilterCliente
        {
            get
            {
                return _filterCliente;
            }
            set
            {
                _filterCliente = value;
                RaisePropertyChanged(_filterClientePrpName);
                ApplyFilter();
            }
        }

        private const string _selectedMarcaPrpName = "SelectedMarca";
        private Marca _selectedMarca;
        public Marca SelectedMarca
        {
            get
            {
                return _selectedMarca;
            }
            set
            {
                _selectedMarca = value;
                RaisePropertyChanged(_selectedMarcaPrpName);
                ApplyFilter();
            }
        }

        public ObservableCollection<Marca> Marche { get; set; } 

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            AddCommand = new RelayCommand(Add);
            UpdateCommand = new RelayCommand(Update);
            RitiratoCommand = new RelayCommand(Ritirato);
            AvvisatoCommand = new RelayCommand(Avvisato);
            AddMarcheCommand = new RelayCommand(AddMarche);
            FilterCommand = new RelayCommand(Filter);
            CancelFilterCommand = new RelayCommand(CancelFilter);
            RemoveMarcaFilterCommand = new RelayCommand(RemoveMarcaFilter);
            RemoveClienteFilterCommand = new RelayCommand(RemoveClienteFilter);

            //RigheOrdine = TestDataGenerator.CreateTestRigheOrdine();
            using (GestOrdiniDataContext db = new GestOrdiniDataContext())
            {
                UpdateRigheOrdineFromDb(db);

                //todo: aggiornare Marche quando viene aggiunta una marca
                Marche = new ObservableCollection<Marca>(db.GetMarche());
            }

            RigheOrdineCollectionView = CollectionViewSource.GetDefaultView(RigheOrdine);
        }

        private void Update()
        {
            GestioneOrdini.Gui.App.CurrentRigaOrdine = SelectedItem as RigaOrdine;
            GestioneOrdini.Gui.App.CurrentRigaOrdineId = (SelectedItem as RigaOrdine).Id;

            ServiceLocator.Current.GetInstance<AddRigaOrdineViewModel>().Setup();

            AddRigaOrdineWindow wnd = new AddRigaOrdineWindow();
            wnd.ShowDialog();

            if (wnd.RigaOrdine != null)
            {
                using (GestOrdiniDataContext db = new GestOrdiniDataContext())
                {
                    bool isOk = db.UpdateRigaOrdine(wnd.RigaOrdine);
                    //se la aggiunta va a buon fine aggiorno
                    if (isOk)
                    {
                        UpdateRigheOrdineFromDb(db);
                    }
                }
            }
        }

        private void Ritirato() 
        {
            if (SelectedItem == null) return;
            if (SelectedItem is RigaOrdine)
            {
                using (GestOrdiniDataContext db = new GestOrdiniDataContext())
                {
                    db.SetRitirato((SelectedItem as RigaOrdine).Id, true);
                    UpdateRigheOrdineFromDb(db);
                }
            }
        }


        private void Avvisato()
        {
            if (SelectedItem == null) return;
            if (SelectedItem is RigaOrdine)
            {
                using (GestOrdiniDataContext db = new GestOrdiniDataContext())
                {
                    db.SetAvvisato((SelectedItem as RigaOrdine).Id, true);
                    UpdateRigheOrdineFromDb(db);
                }
            }
        }

        private void Add()
        {
            GestioneOrdini.Gui.App.CurrentRigaOrdine = null;
            GestioneOrdini.Gui.App.CurrentRigaOrdineId = null;

            ServiceLocator.Current.GetInstance<AddRigaOrdineViewModel>().Setup();

            AddRigaOrdineWindow wnd = new AddRigaOrdineWindow();
            wnd.ShowDialog();

            if (wnd.RigaOrdine != null)
            {
                using (GestOrdiniDataContext db = new GestOrdiniDataContext())
                {
                    bool isOk = db.AddRigaOrdine(wnd.RigaOrdine);
                    //se la aggiunta va a buon fine aggiorno
                    if (isOk)
                    {
                        UpdateRigheOrdineFromDb(db);
                    }
                }
            }
        }

        private void AddMarche()
        {
            AddMarcaWindow wnd = new AddMarcaWindow();
            wnd.ShowDialog();

            if (wnd.Marche != null)
            {
                using (GestOrdiniDataContext db = new GestOrdiniDataContext())
                {
                    string[] marche = wnd.Marche.Split(new[] {';'});

                    foreach (var marca in marche)
                    {
                        db.AddMarca(new Marca() {Nome = marca});
                    }

                    Marche.Clear();
                    foreach (Marca marca in db.GetMarche())
                    {
                        Marche.Add(marca);
                    }
                }
            }
        }

        private void UpdateRigheOrdineFromDb(GestOrdiniDataContext db)
        {
            if (RigheOrdine == null)
            {
                RigheOrdine = new ObservableCollection<RigaOrdine>();
            }
            else
            {
                RigheOrdine.Clear();
            }

            foreach (var ro in db.GetRigheOrdine())
            {
                RigheOrdine.Add(ro);
            }
        }

        private void ApplyFilter()
        {
            RigheOrdineCollectionView.Filter = FilterByMarcaAndCliente;
        }

        private bool FilterByMarcaAndCliente(object obj)
        {
            if (!(obj is RigaOrdine))
            {
                return true;
            }
            RigaOrdine ro = obj as RigaOrdine;
            return FilterByCliente(ro) && FilterByMarca(ro);
        }

        private bool FilterByCliente(RigaOrdine ro)
        {
            if (!string.IsNullOrEmpty(FilterCliente))
            {
                if (!ro.Cliente.Contains(FilterCliente))
                {
                    return false;
                }
            }
            return true;
        }

        private bool FilterByMarca(RigaOrdine ro)
        {
            if (SelectedMarca != null)
            {
                if (SelectedMarca.Nome != ro.Marca)
                {
                    return false;
                }
            }
            return true;
        }

        private void RemoveMarcaFilter()
        {
            SelectedMarca = null;
        }

        private void RemoveClienteFilter()
        {
            FilterCliente = null;
        }

        private void Filter() { }
        private void CancelFilter() { }
    }
}