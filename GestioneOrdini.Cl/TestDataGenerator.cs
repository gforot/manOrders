
using System;
using System.Collections.Generic;


namespace GestioneOrdini.Cl
{
    public static class TestDataGenerator
    {
        public static List<RigaOrdine> CreateTestRigheOrdine()
        {
            int id = 0;
            return new List<RigaOrdine>()
                   {
                       new RigaOrdine()
                       {
                           Cliente="Andrea Rota",
                           Avvisato = 0,
                           DataArrivoPezzo = null,
                           DataAvvisato = null,
                           DataOrdine = DateTime.Now,
                           Descrizione = "staffa per cassone Nissan",
                           Stato = 2,
                           Id = id++
                       },
                       new RigaOrdine()
                       {
                           Cliente="Clara Giudici",
                           Avvisato = 0,
                           DataArrivoPezzo = null,
                           DataAvvisato = null,
                           DataOrdine = DateTime.Now,
                           Descrizione = "Custodia cell",
                           Stato = 3,
                           Id = id++
                       },
                       new RigaOrdine()
                       {
                           Cliente="Angelo Rota",
                           Avvisato = 1,
                           DataArrivoPezzo = null,
                           DataAvvisato = null,
                           DataOrdine = DateTime.Now.Subtract(new TimeSpan(5,0,0,0)),
                           Descrizione = "Custodia cell",
                           Stato = 4,
                           Id = id
                       },
                   };
        }
 
    }
}
