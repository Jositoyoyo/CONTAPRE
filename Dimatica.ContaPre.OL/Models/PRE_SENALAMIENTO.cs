namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_SENALAMIENTO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_SENALAMIENTO()
        {
            PRE_SENALAMIENTO_DOCUMENTO = new HashSet<PRE_SENALAMIENTO_DOCUMENTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int SEN_CODIGO { get; set; }

        public short? SEN_ANO { get; set; }

        public int? SEN_NUMERO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? SEN_FECHA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? SEN_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_SENALAMIENTO_DOCUMENTO> PRE_SENALAMIENTO_DOCUMENTO { get; set; }

        [NotMapped]
        public decimal SEN_TOTAL_LIQUIDO { get; set; }

        //[NotMapped]
        //public string AmountLabel
        //{
        //    get
        //    {
        //        return this.SEN_TOTAL_LIQUIDO.ToString("N");
        //    }
        //}

        [NotMapped]
        public int? NUMERO_EXPEDIENTE { get; set; }

        [NotMapped]
        public int NUMERO_DOCUMENTOS { get; set; }

        #endregion
    }
}