namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_PROVEEDOR
    {
        #region Fields

        private string provName;

        #endregion

        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_PROVEEDOR()
        {
            PRE_DOCUMENTO_CONTABLE = new HashSet<PRE_DOCUMENTO_CONTABLE>();
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_EXP_EXTRAPRE1 = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_EXPEDIENTE_CONTABLE = new HashSet<PRE_EXPEDIENTE_CONTABLE>();
            pre_proveedor_expediente_contable = new HashSet<pre_proveedor_expediente_contable>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int PROV_CODIGO { get; set; }

        [StringLength(60)]
        public string PROV_NOMBRE { get; set; }

        [StringLength(15)]
        public string PROV_NIF { get; set; }

        [StringLength(100)]
        public string PROV_DIRECCION { get; set; }

        [StringLength(50)]
        public string PROV_POBLACION { get; set; }

        [StringLength(5)]
        public string PROV_CODIGO_POSTAL { get; set; }

        public byte? ProvID { get; set; }

        public int? CodPaisID { get; set; }

        [StringLength(20)]
        public string PROV_TELEFONO { get; set; }

        [StringLength(75)]
        public string PROV_PERSONA_CONTACTO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PROV_COD_PROVEEDOR { get; set; }

        public bool? PROV_INTER_JUDICIAL { get; set; }

        [StringLength(4)]
        public string PROV_CC_CE { get; set; }

        [StringLength(4)]
        public string PROV_CC_CO { get; set; }

        [StringLength(2)]
        public string PROV_CC_DC { get; set; }

        [StringLength(10)]
        public string PROV_CC_NC { get; set; }

        [StringLength(50)]
        public string PROV_NOMBRE_SUCURSAL { get; set; }

        [StringLength(100)]
        public string PROV_DIR_SUCURSAL { get; set; }

        [StringLength(5)]
        public string PROV_CP_SUCURSAL { get; set; }

        [StringLength(50)]
        public string PROV_POBLACION_SUCURSAL { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PROV_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        [StringLength(50)]
        public string prov_iban { get; set; }

        public virtual Pais Pais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_CONTABLE> PRE_DOCUMENTO_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXPEDIENTE_CONTABLE> PRE_EXPEDIENTE_CONTABLE { get; set; }

        public virtual Provincia Provincia { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pre_proveedor_expediente_contable> pre_proveedor_expediente_contable { get; set; }

        [NotMapped]
        public string ProvName
        {
            get
            {
                return this.provName ?? (this.provName = string.Empty);
            }
            set
            {
                this.provName = value;
            }
        }

        #endregion
    }
}