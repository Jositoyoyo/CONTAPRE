namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_PARAMETROS
    {
        #region Public Properties

        [Key]
        public int PAR_CODIGO { get; set; }

        [StringLength(60)]
        public string PAR_DESCRIPCION { get; set; }

        [StringLength(60)]
        public string PAR_VALOR { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PAR_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public string CODIGO_MINISTERIO { get; set; }

        [NotMapped]
        public string CODIGO_ORGANISMO { get; set; }

        [NotMapped]
        public string NOMBRE_ORGANISMO { get; set; }

        [NotMapped]
        public string MINISTERIO { get; set; }

        #endregion
    }
}