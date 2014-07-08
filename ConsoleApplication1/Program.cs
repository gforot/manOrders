using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneOrdini.Cl;


namespace ConsoleApplication1
{
    class Program
    {
        


        static void Main(string[] args)
        {
            using (GestOrdiniDataContext db = new GestOrdiniDataContext())
            {
                foreach (RigaOrdine rigaOrdine in db.RigheOrdine)
                {
                    rigaOrdine.Cliente = rigaOrdine.Cliente + "*";
                    //rigaOrdine.DataOrdine = DateTime.Now;
                    Console.WriteLine("ID: {0}", rigaOrdine.Id);
                    Console.WriteLine("Cliente: {0}", rigaOrdine.Cliente);
                    Console.WriteLine("Descrizione: {0}", rigaOrdine.Descrizione);
                    Console.WriteLine("DataOrdine: {0}", rigaOrdine.DataOrdine);
                    Console.WriteLine("Avvisato: {0}", rigaOrdine.Avvisato);
                    Console.WriteLine("Stato: {0}", rigaOrdine.Stato);
                    Console.WriteLine("DataAvvisato: {0}", rigaOrdine.DataAvvisato);
                    Console.WriteLine("DataArrivoPezzo: {0}", rigaOrdine.DataArrivoPezzo);

                    
                }
                db.SubmitChanges();
                //foreach (Prova prova in db.Prove)
                //{
                //    prova.A = prova.A + "*";
                //    Console.WriteLine("a: {0}", prova.A);

                //}
                

            }
            Console.Write("cccccccc");
            Console.ReadKey();
        }

        //static void Main(string[] args)
        //{
        //    var connection = new SQLiteConnection(string.Format("Data Source={0}", _dbPath));

        //    using (
        //        DataContext context = new DataContext(connection))
        //    {
        //        var righeOrdine = context.GetTable<RigaOrdine>();
        //        foreach (var rigaOrdine in righeOrdine)
        //        {
        //            //Console.WriteLine("ID: {0}", rigaOrdine.Id);
        //            //Console.WriteLine("Cliente: {0}", rigaOrdine.Cliente);
        //            //Console.WriteLine("Descrizione: {0}", rigaOrdine.Descrizione);
        //            //Console.WriteLine("DataOrdine: {0}", rigaOrdine.DataOrdine);

        //            //Console.WriteLine("Stato: {0}", rigaOrdine.Stato);
        //            //Console.WriteLine("DataAvvisato: {0}", rigaOrdine.DataAvvisato);
        //            //Console.WriteLine("DataArrivoPezzo: {0}", rigaOrdine.DataArrivoPezzo);

        //            rigaOrdine.Cliente = rigaOrdine.Cliente + "*";
        //            rigaOrdine.Avvisato = 1;

        //        }
        //        context.SubmitChanges();
        //    }

        //    Console.ReadKey();
        //}
    }
}
