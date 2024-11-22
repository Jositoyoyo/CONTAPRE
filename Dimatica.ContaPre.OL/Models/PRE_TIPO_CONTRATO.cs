namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_TIPO_CONTRATO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_TIPO_CONTRATO()
        {
            PRE_EXPEDIENTE_ADMINISTRATIVO = new HashSet<PRE_EXPEDIENTE_ADMINISTRATIVO>();
        }

        #endregion

        #region Public Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte TIPC_CODIGO { get; set; }

        [StringLength(60)]
        public string TIPC_DESCRIPCION { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TIPC_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXPEDIENTE_ADMINISTRATIVO> PRE_EXPEDIENTE_ADMINISTRATIVO { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public int TIPC_CODIGO_AUX { get; set; }

        #endregion
    }
}