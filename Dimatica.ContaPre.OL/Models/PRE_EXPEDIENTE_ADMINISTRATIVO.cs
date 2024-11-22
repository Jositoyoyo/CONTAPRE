namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_EXPEDIENTE_ADMINISTRATIVO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_EXPEDIENTE_ADMINISTRATIVO()
        {
            PRE_EXPEDIENTE_CONTABLE = new HashSet<PRE_EXPEDIENTE_CONTABLE>();
            PRE_MODIFICACION_CREDITO = new HashSet<PRE_MODIFICACION_CREDITO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int EA_CODIGO { get; set; }

        public int? EA_NUMERO { get; set; }

        public short? EA_ANO_EJERCICIO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ANU_COD_ANUALIDAD { get; set; }

        public int? PROC_CODIGO { get; set; }

        [StringLength(255)]
        public string EA_DESCRIPCION { get; set; }

        public byte? TIPC_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EA_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public int? ExpAdministrativoID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXPEDIENTE_CONTABLE> PRE_EXPEDIENTE_CONTABLE { get; set; }

        public virtual PRE_PROCEDENCIA PRE_PROCEDENCIA { get; set; }

        public virtual PRE_TIPO_CONTRATO PRE_TIPO_CONTRATO { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_MODIFICACION_CREDITO> PRE_MODIFICACION_CREDITO { get; set; }

        [NotMapped]
        public string PROC_DESCRIPCION { get; set; }

        [NotMapped]
        public string TIPC_DESCRIPCION { get; set; }

        #endregion
    }
}