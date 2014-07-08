﻿using System;
using System.Data.Linq.Mapping;

namespace GestioneOrdini.Cl
{
    [Table(Name="RigheOrdine")]
    public class RigaOrdine
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name = "Cliente")]
        public string Cliente { get; set; }

        [Column(Name = "Telefono")]
        public string Telefono { get; set; }

        [Column(Name = "Descrizione")]
        public string Descrizione { get; set; }

        [Column(Name = "Marca")]
        public string Marca { get; set; }

        [Column(Name = "DataOrdine")]
        public DateTime? DataOrdine { get; set; }

        [Column(Name = "DataArrivoPezzo")]
        public DateTime? DataArrivoPezzo { get; set; }

        [Column(Name = "Stato")]
        public int Stato { get; set; }

        [Column(Name = "Avvisato")]
        public int Avvisato { get; set; }

        [Column(Name = "DataAvvisato")]
        public DateTime? DataAvvisato { get; set; }
    }

}