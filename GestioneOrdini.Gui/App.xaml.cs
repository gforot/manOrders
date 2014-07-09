using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GestioneOrdini.Cl;


namespace GestioneOrdini.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RigaOrdine CurrentRigaOrdine { get; set; }
        public static int? CurrentRigaOrdineId { get; set; }
    }
}
