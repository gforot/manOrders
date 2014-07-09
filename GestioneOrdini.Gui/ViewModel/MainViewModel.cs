using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GestioneOrdini.Cl;


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

            //RigheOrdine = TestDataGenerator.CreateTestRigheOrdine();
            using (GestOrdiniDataContext db = new GestOrdiniDataContext())
            {
                UpdateRigheOrdineFromDb(db);
            }
        }

        private void Update()
        {
            
        }

        private void Add()
        {
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