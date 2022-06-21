namespace LsiApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LSI_APP
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string NazwaEksportu { get; set; }

        public DateTime? DataGodzina { get; set; }

        [Required]
        [StringLength(255)]
        public string Uzytkownik { get; set; }

        [Required]
        [StringLength(255)]
        public string Lokal { get; set; }
    }
}
