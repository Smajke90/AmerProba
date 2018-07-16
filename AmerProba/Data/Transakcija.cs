using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmerProba.Data
{
    public class Transakcija
    {
        [Key]
        public int TransakcijaId { get; set; }

        public TipTransakcije Tip { get; set; }

        public string Detalji { get; set; }

        public decimal Iznos { get; set; }

        public DateTime Vrijeme { get; set; }

        public User User { get; set; }
        public string KorisnikId { get; set; }

    }
    public enum TipTransakcije
    {
        uplata=0,
        trosak=1
    }
}
