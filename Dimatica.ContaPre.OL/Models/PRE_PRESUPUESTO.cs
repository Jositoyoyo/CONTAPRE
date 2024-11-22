namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_PRESUPUESTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_PRESUPUESTO()
        {
            PRE_DOCUMENTO_APLICACION = new HashSet<PRE_DOCUMENTO_APLICACION>();
            PRE_MODIF_CREDITO_PRESUPUESTO = new HashSet<PRE_MODIF_CREDITO_PRESUPUESTO>();
        }

        [Key]
        public int PRE_CODIGO { get; set; }

        public byte? CAP_CODIGO { get; set; }

        public byte? ART_CODIGO { get; set; }

        public int? CON_CODIGO { get; set; }

        public byte? SUB_CODIGO { get; set; }

        public short? PRE_ANO { get; set; }

        [StringLength(1)]
        public string PRE_I_G { get; set; }

        public bool PRE_NO_VINCULANTE { get; set; }

        public bool PRE_CERRADO { get; set; }

        [Column(TypeName = "money")]
        public decimal? PRE_IMPORTE { get; set; }

        [Column(TypeName = "money")]
        public decimal? PRE_IMPORTE_MODIFICACIONES { get; set; }

        public byte? PRO_CODIGO { get; set; }

        public byte? MON_CODIGO { get; set; }

        public bool? PRE_OCULTO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PRE_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual PRE_ARTICULO PRE_ARTICULO { get; set; }

        public virtual PRE_CAPITULO PRE_CAPITULO { get; set; }

        public virtual PRE_CONCEPTO PRE_CONCEPTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_APLICACION> PRE_DOCUMENTO_APLICACION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_MODIF_CREDITO_PRESUPUESTO> PRE_MODIF_CREDITO_PRESUPUESTO { get; set; }

        public virtual PRE_MONEDA PRE_MONEDA { get; set; }

        public virtual PRE_PROGRAMA PRE_PROGRAMA { get; set; }

        public virtual PRE_SUBCONCEPTO PRE_SUBCONCEPTO { get; set; }

        public virtual User USUARIO { get; set; }
    }
}
