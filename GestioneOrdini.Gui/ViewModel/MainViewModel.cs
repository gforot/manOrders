using System.Collections.Generic;
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

        public string AppTitle
        {   
            get
            {
                return "Gestione Ordini";
            }
        }

        public List<RigaOrdine> RigheOrdine { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            AddCommand = new RelayCommand(Add);

            //RigheOrdine = TestDataGenerator.CreateTestRigheOrdine();
            using (GestOrdiniDataContext db = new GestOrdiniDataContext())
            {
                RigheOrdine = db.GetRigheOrdine();
            }
        }

        private void Add()
        {
            AddRigaOrdineWindow wnd = new AddRigaOrdineWindow();
            wnd.ShowDialog();

            if (wnd.RigaOrdine != null)
            {
                using (GestOrdiniDataContext db = new GestOrdiniDataContext())
                {
                    db.AddRigaOrdine(wnd.RigaOrdine);
                }
            }
        }
    }
}