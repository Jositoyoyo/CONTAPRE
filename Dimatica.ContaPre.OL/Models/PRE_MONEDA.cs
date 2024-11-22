namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_MONEDA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_MONEDA()
        {
            PRE_DETALLE_HOJA_ARQUEO = new HashSet<PRE_DETALLE_HOJA_ARQUEO>();
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_EXPEDIENTE_CONTABLE = new HashSet<PRE_EXPEDIENTE_CONTABLE>();
            PRE_MODIFICACION_CREDITO = new HashSet<PRE_MODIFICACION_CREDITO>();
            PRE_PRESUPUESTO = new HashSet<PRE_PRESUPUESTO>();
            PRE_TESORERIA = new HashSet<PRE_TESORERIA>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte MON_CODIGO { get; set; }

        [StringLength(20)]
        public string MON_DESCRIPCION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DETALLE_HOJA_ARQUEO> PRE_DETALLE_HOJA_ARQUEO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXPEDIENTE_CONTABLE> PRE_EXPEDIENTE_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_MODIFICACION_CREDITO> PRE_MODIFICACION_CREDITO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PRESUPUESTO> PRE_PRESUPUESTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA> PRE_TESORERIA { get; set; }
    }
}
