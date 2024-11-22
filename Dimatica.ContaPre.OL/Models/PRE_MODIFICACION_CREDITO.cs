namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_MODIFICACION_CREDITO
    {
        #region Fields

        private string tipmDescripcionI;

        private string tipmDescripcionG;

        #endregion

        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_MODIFICACION_CREDITO()
        {
            PRE_MODIF_CREDITO_PRESUPUESTO = new HashSet<PRE_MODIF_CREDITO_PRESUPUESTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int MOD_CODIGO { get; set; }

        public short? MOD_ANO_PRESUPUESTO { get; set; }

        public int? MOD_NUMERO_ORDEN { get; set; }

        public int? EA_CODIGO { get; set; }

        public int? EXP_CODIGO { get; set; }

        public byte? MON_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? MOD_FECHA_PROPUESTA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? MOD_FECHA_ASIENTO_DIARIO { get; set; }

        [StringLength(120)]
        public string MOD_DESCRIPCION { get; set; }

        public bool? MOD_EJECUTADA { get; set; }

        public bool? MOD_CREADO_EXPEDIENTE { get; set; }

        public int? TIPM_CODIGO_I { get; set; }

        public int? TIPM_CODIGO_G { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? MOD_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual PRE_EXPEDIENTE_ADMINISTRATIVO PRE_EXPEDIENTE_ADMINISTRATIVO { get; set; }

        public virtual PRE_EXPEDIENTE_CONTABLE PRE_EXPEDIENTE_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_MODIF_CREDITO_PRESUPUESTO> PRE_MODIF_CREDITO_PRESUPUESTO { get; set; }

        public virtual PRE_MONEDA PRE_MONEDA { get; set; }

        public virtual PRE_TIPO_MODIFICACION_CREDITO PRE_TIPO_MODIFICACION_CREDITO { get; set; }

        public virtual PRE_TIPO_MODIFICACION_CREDITO PRE_TIPO_MODIFICACION_CREDITO1 { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public string TIPM_DESCRIPCION_I
        {
            get
            {
                return this.tipmDescripcionI ?? (this.tipmDescripcionI = string.Empty);
            }
            set
            {
                this.tipmDescripcionI = value;
            }
        }

        [NotMapped]
        public string TIPM_DESCRIPCION_G
        {
            get
            {
                return this.tipmDescripcionG ?? (this.tipmDescripcionG = string.Empty);
            }
            set
            {
                this.tipmDescripcionG = value;
            }
        }

        [NotMapped]
        public string DescriptionLabelIncome
        {
            get
            {
                return $"{this.TIPM_CODIGO_I} - {this.TIPM_DESCRIPCION_I}";
            }
        }

        [NotMapped]
        public string DescriptionLabelSpend
        {
            get
            {
                return $"{this.TIPM_CODIGO_G} - {this.TIPM_DESCRIPCION_G}";
            }
        }

        [NotMapped]
        public string ExecuteImage
        {
            get
            {
                if (this.MOD_EJECUTADA == null)
                {
                    return "empty";
                }

                return (bool)this.MOD_EJECUTADA ? "notObsolete" : "obsolete";
            }
        }

        #endregion
    }
}