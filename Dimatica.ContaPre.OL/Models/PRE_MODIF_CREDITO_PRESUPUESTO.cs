namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_MODIF_CREDITO_PRESUPUESTO
    {
        private string cacsNumero;

        private string cacsCodigo;

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PRE_CODIGO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MOD_CODIGO { get; set; }

        [Column(TypeName = "money")]
        public decimal? MODP_IMPORTE { get; set; }

        public bool? MODP_POSITIVO { get; set; }

        [StringLength(1)]
        public string MODP_I_G { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? MODP_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual PRE_MODIFICACION_CREDITO PRE_MODIFICACION_CREDITO { get; set; }

        public virtual PRE_PRESUPUESTO PRE_PRESUPUESTO { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public string CACS_NUMERO
        {
            get
            {
                return this.cacsNumero ?? (this.cacsNumero = string.Empty);
            }
            set
            {
                this.cacsNumero = value;
            }
        }
        [NotMapped]
        public string CACS_CODIGO
        {
            get
            {
                return this.cacsCodigo ?? (this.cacsCodigo = string.Empty);
            }
            set
            {
                this.cacsCodigo = value;
            }
        }

        [NotMapped]
        public string SingLabel
        {
            get
            {
                return (bool)this.MODP_POSITIVO ? "+" : "-";
            }
        }

        //[NotMapped]
        //public string AmountLabel
        //{
        //    get
        //    {
        //        return ((decimal)this.MODP_IMPORTE).ToString("N");
        //    }
        //}
    }
}
