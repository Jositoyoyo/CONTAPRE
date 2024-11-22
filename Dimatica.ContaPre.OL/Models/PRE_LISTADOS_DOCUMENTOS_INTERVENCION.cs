namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_LISTADOS_DOCUMENTOS_INTERVENCION
    {
        [Key]
        public int IdListado { get; set; }

        [StringLength(100)]
        public string Registro { get; set; }

        public DateTime? Fecha_Envio { get; set; }

        public DateTime? Fecha_Desaparicion_EnListado { get; set; }

        [StringLength(500)]
        public string observaciones { get; set; }
    }
}
