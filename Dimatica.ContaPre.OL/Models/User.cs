namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    [Table("USUARIO")]
    public partial class User : BaseModel
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            PRE_ARTICULO = new HashSet<PRE_ARTICULO>();
            PRE_CAPITULO = new HashSet<PRE_CAPITULO>();
            PRE_CONCEPTO = new HashSet<PRE_CONCEPTO>();
            PRE_CUENTA_PGCP = new HashSet<PRE_CUENTA_PGCP>();
            PRE_CUENTA_RESTRINGIDA = new HashSet<PRE_CUENTA_RESTRINGIDA>();
            PRE_DETALLE_HOJA_ARQUEO = new HashSet<PRE_DETALLE_HOJA_ARQUEO>();
            PRE_DOCUMENTO_APLICACION = new HashSet<PRE_DOCUMENTO_APLICACION>();
            PRE_DOCUMENTO_CONTABLE = new HashSet<PRE_DOCUMENTO_CONTABLE>();
            PRE_EXP_CENTRO_COSTE = new HashSet<PRE_EXP_CENTRO_COSTE>();
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_EXPEDIENTE_ADMINISTRATIVO = new HashSet<PRE_EXPEDIENTE_ADMINISTRATIVO>();
            PRE_EXPEDIENTE_CONTABLE = new HashSet<PRE_EXPEDIENTE_CONTABLE>();
            PRE_HOJA_ARQUEO = new HashSet<PRE_HOJA_ARQUEO>();
            PRE_LINEA_TESORERIA = new HashSet<PRE_LINEA_TESORERIA>();
            PRE_MODIF_CREDITO_PRESUPUESTO = new HashSet<PRE_MODIF_CREDITO_PRESUPUESTO>();
            PRE_MODIFICACION_CREDITO = new HashSet<PRE_MODIFICACION_CREDITO>();
            PRE_PARAMETROS = new HashSet<PRE_PARAMETROS>();
            PRE_PRESUPUESTO = new HashSet<PRE_PRESUPUESTO>();
            PRE_PROGRAMA = new HashSet<PRE_PROGRAMA>();
            PRE_PROVEEDOR = new HashSet<PRE_PROVEEDOR>();
            PRE_SENALAMIENTO = new HashSet<PRE_SENALAMIENTO>();
            PRE_SENALAMIENTO_DOCUMENTO = new HashSet<PRE_SENALAMIENTO_DOCUMENTO>();
            PRE_SUBCONCEPTO = new HashSet<PRE_SUBCONCEPTO>();
            PRE_TESORERIA = new HashSet<PRE_TESORERIA>();
            PRE_TESORERIA_DOCUMENTO = new HashSet<PRE_TESORERIA_DOCUMENTO>();
            PRE_TIPO_CONTRATO = new HashSet<PRE_TIPO_CONTRATO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int USU_CODIGO { get; set; }

        [StringLength(20)]
        public string USU_NOMBRE { get; set; }

        [StringLength(60)]
        public string USU_APELLIDOS { get; set; }

        [StringLength(8)]
        public string USU_LOGIN { get; set; }

        [StringLength(28)]
        public string USU_PASSWORD { get; set; }

        [StringLength(1)]
        public string USU_I_G { get; set; }

        public byte? USU_NIVEL { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? USU_FECHA_CONEXION { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? USU_FECHA_DESCONEXION { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? USU_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO_MODIFICACION { get; set; }

        [StringLength(100)]
        public string USU_EMAIL { get; set; } 


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_ARTICULO> PRE_ARTICULO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_CAPITULO> PRE_CAPITULO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_CONCEPTO> PRE_CONCEPTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_CUENTA_PGCP> PRE_CUENTA_PGCP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_CUENTA_RESTRINGIDA> PRE_CUENTA_RESTRINGIDA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DETALLE_HOJA_ARQUEO> PRE_DETALLE_HOJA_ARQUEO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_APLICACION> PRE_DOCUMENTO_APLICACION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_CONTABLE> PRE_DOCUMENTO_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_CENTRO_COSTE> PRE_EXP_CENTRO_COSTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXPEDIENTE_ADMINISTRATIVO> PRE_EXPEDIENTE_ADMINISTRATIVO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXPEDIENTE_CONTABLE> PRE_EXPEDIENTE_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_HOJA_ARQUEO> PRE_HOJA_ARQUEO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_LINEA_TESORERIA> PRE_LINEA_TESORERIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_MODIF_CREDITO_PRESUPUESTO> PRE_MODIF_CREDITO_PRESUPUESTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_MODIFICACION_CREDITO> PRE_MODIFICACION_CREDITO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PARAMETROS> PRE_PARAMETROS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PRESUPUESTO> PRE_PRESUPUESTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PROGRAMA> PRE_PROGRAMA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PROVEEDOR> PRE_PROVEEDOR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_SENALAMIENTO> PRE_SENALAMIENTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_SENALAMIENTO_DOCUMENTO> PRE_SENALAMIENTO_DOCUMENTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_SUBCONCEPTO> PRE_SUBCONCEPTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA> PRE_TESORERIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA_DOCUMENTO> PRE_TESORERIA_DOCUMENTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TIPO_CONTRATO> PRE_TIPO_CONTRATO { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{this.USU_NOMBRE} {this.USU_APELLIDOS}";
            }
        }

        [NotMapped]
        public string OLD_PASSWORD { get; set; }

        #endregion
    }
}