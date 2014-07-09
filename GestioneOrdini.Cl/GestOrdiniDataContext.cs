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
        public Table<Marca> Marche;

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
                ro.Id = GetRigheOrdineNextId();
                this.RigheOrdine.InsertOnSubmit(ro);
                this.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int GetRigheOrdineNextId()
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

        public bool UpdateRigaOrdine(RigaOrdine rigaOrdine)
        {
            try
            {
                //recupero la riga da aggiornare
                RigaOrdine original = RigheOrdine.Single(ro => ro.Id == rigaOrdine.Id);
                if (original == null)
                {
                    return false;
                }
                original.CopyAllProperties(rigaOrdine);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Marca> GetMarche()
        {
            return new List<Marca>(Marche);
        }

        public bool AddMarca(Marca m)
        {
            try
            {
                m.Id = GetMarcheNextId();
                this.Marche.InsertOnSubmit(m);
                this.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int GetMarcheNextId()
        {
            List<Marca> rords = this.GetMarche();
            if (rords.Count == 0)
            {
                return 1;
            }
            return rords.Max(rord => rord.Id) + 1;
        }
    }
}
