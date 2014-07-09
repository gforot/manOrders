using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public string AppTitle
        {   
            get
            {
                return "Gestione Ordini";
            }
        }

        public ObservableCollection<RigaOrdine> RigheOrdine { get; set; }

        public object SelectedItem { get; set; }

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

            //RigheOrdine = TestDataGenerator.CreateTestRigheOrdine();
            using (GestOrdiniDataContext db = new GestOrdiniDataContext())
            {
                UpdateRigheOrdineFromDb(db);
            }
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
                RigaOrdine ro = SelectedItem as RigaOrdine;

                using (GestOrdiniDataContext db = new GestOrdiniDataContext())
                {
                    RigaOrdine roInDb = db.GetRigaOrdine(ro.Id);
                    if (roInDb != null)
                    {
                        //ho trovato la riga nel db. La aggiorno.
                        roInDb.Ritirato = 1;
                        roInDb.DataRitirato = System.DateTime.Now;

                        bool res = db.UpdateRigaOrdine(roInDb);
                        if (res)
                        {
                            db.SubmitChanges();
                            UpdateRigheOrdineFromDb(db);
                        }
                        else
                        {

                        }
                    }


                }

            }


        }


        private void Avvisato()
        {
            if (SelectedItem == null) return;
            if (SelectedItem is RigaOrdine)
            {
                RigaOrdine ro = SelectedItem as RigaOrdine;

                using (GestOrdiniDataContext db = new GestOrdiniDataContext())
                {
                    RigaOrdine roInDb = db.GetRigaOrdine(ro.Id);
                    if (roInDb != null)
                    {
                        //ho trovato la riga nel db. La aggiorno.
                        roInDb.Avvisato = 1;
                        roInDb.DataAvvisato = System.DateTime.Now;

                        bool res = db.UpdateRigaOrdine(roInDb);
                        if (res)
                        {
                            db.SubmitChanges();
                            UpdateRigheOrdineFromDb(db);
                        }
                        else
                        {

                        }
                    }


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
    }
}