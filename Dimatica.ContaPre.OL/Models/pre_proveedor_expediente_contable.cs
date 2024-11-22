namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pre_proveedor_expediente_contable
    {
        [Key]
        public int prov_exp_codigo { get; set; }

        public int exp_codigo { get; set; }

        public int prov_codigo { get; set; }

        public int usu_codigo { get; set; }

        public DateTime? fecha_alta { get; set; }

        public DateTime? fecha_modif { get; set; }

        public virtual PRE_EXPEDIENTE_CONTABLE PRE_EXPEDIENTE_CONTABLE { get; set; }

        public virtual PRE_PROVEEDOR PRE_PROVEEDOR { get; set; }
    }
}
