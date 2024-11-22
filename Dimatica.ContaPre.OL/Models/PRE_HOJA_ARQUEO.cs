namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Remoting.Messaging;

    #endregion

    public partial class PRE_HOJA_ARQUEO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_HOJA_ARQUEO()
        {
            PRE_DETALLE_HOJA_ARQUEO = new HashSet<PRE_DETALLE_HOJA_ARQUEO>();
            PRE_TESORERIA = new HashSet<PRE_TESORERIA>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int HOJ_CODIGO { get; set; }

        public short? HOJ_ANO { get; set; }

        public int? HOJ_NUMERO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HOJ_FECHA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HOJ_FECHA_SICAI { get; set; }

        public int? HOJ_NUMERO_SICAI { get; set; }

        public bool? HOJ_ARQUEO50 { get; set; }

        public int? CUE_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HOJ_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public int? HArqueoID { get; set; }

        public virtual PRE_CUENTA_RESTRINGIDA PRE_CUENTA_RESTRINGIDA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DETALLE_HOJA_ARQUEO> PRE_DETALLE_HOJA_ARQUEO { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA> PRE_TESORERIA { get; set; }

        [NotMapped]
        public int? DET_CODIGO { get; set; }

        [NotMapped]
        public int? EXP_EXTRAP_CODIGO { get; set; }

        [NotMapped]
        public DateTime? DET_FECHA_APUNTE { get; set; }

        [NotMapped]
        public int? DET_NUMERO_EXPEDIENTE { get; set; }

        [NotMapped]
        public string DOC_DESCRIPCION { get; set; }

        [NotMapped]
        public decimal? DET_IMPORTE { get; set; }

        [NotMapped]
        public string DET_IMPORTE_LABEL
        {
            get
            {
                return ((decimal)this.DET_IMPORTE).ToString("N");
            }
        }

        [NotMapped]
        public string CUE_DESCRIPCION { get; set; }

        [NotMapped]
        public int EXP_ANO_PRESUPUESTO { get; set; }

        [NotMapped]
        public int? LIN_NUMERO { get; set; }

        [NotMapped]
        public bool MARCADO { get; set; }

        [NotMapped]
        public decimal? AmountLabel
        {
            get
            {
                if ((bool)this.HOJ_ARQUEO50)
                {
                    return null;
                }

                return this.DET_IMPORTE ?? 0;
            }
        }

        [NotMapped]
        public decimal? Amount50Label
        {
            get
            {
                if (!(bool)this.HOJ_ARQUEO50)
                {
                    return null;
                }

                return this.DET_IMPORTE ?? 0;
            }
        }

        [NotMapped]
        public int CUE_ORDINAL_PERCEPTOR { get; set; }

        [NotMapped]
        public string CUE_ORDINAL_LABEL
        {
            get
            {
                return $"{this.CUE_ORDINAL_PERCEPTOR} - {this.CUE_DESCRIPCION}";
            }
        }

        [NotMapped]
        public string Sheet50Image
        {
            get
            {
                return (bool)this.HOJ_ARQUEO50 == false ? "empty" : "notObsolete";
            }
        }

        [NotMapped]
        public string TIPO { get; set; }

        #endregion
    }
}