using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;


namespace GestioneOrdini.Cl
{
    public class GestOrdiniDataContext : DataContext
    {
        //private const string _dbPath = @"C:\Users\arota\Documents\Personali\sw\GestioneOrdinis\GestioneOrdini.Cl\db\GestioneOrdini.db3";
        private const string _dbPath = @"db\GestioneOrdini.db3";
        public Table<RigaOrdine> RigheOrdine;

        public GestOrdiniDataContext()
            : this(new SQLiteConnection(string.Format("Data Source={0}", _dbPath)))
        {
            this.Log = Console.Out;
        }

        public GestOrdiniDataContext(SQLiteConnection connection)
            : base(connection)
        {
            
        }

        public bool AddRigaOrdine(RigaOrdine ro)
        {
            try
            {
                ro.Id = GetNextId();
                this.RigheOrdine.InsertOnSubmit(ro);
                this.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int GetNextId()
        {
            List<RigaOrdine> rords = this.GetRigheOrdine();
            if (rords.Count == 0)
            {
                return 1;
            }
            return rords.Max(rord => rord.Id) + 1;
        }

        public RigaOrdine GetRigaOrdine(int id)
        {
            return RigheOrdine.First(r => r.Id == id);
        }

        public List<RigaOrdine> GetRigheOrdine()
        {
            return new List<RigaOrdine>(RigheOrdine);
        }
    }
}
