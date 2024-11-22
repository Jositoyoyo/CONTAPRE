namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContaPreModel : DbContext
    {
        public ContaPreModel()
            : base("name=ContaPreModel")
        {
        }

        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<Pais> Paises { get; set; }
        public virtual DbSet<PRE_ARTICULO> PRE_ARTICULO { get; set; }
        public virtual DbSet<PRE_CAPITULO> PRE_CAPITULO { get; set; }
        public virtual DbSet<PRE_CENTRO_COSTE> PRE_CENTRO_COSTE { get; set; }
        public virtual DbSet<PRE_CONCEPTO> PRE_CONCEPTO { get; set; }
        public virtual DbSet<PRE_CUENTA_PGCP> PRE_CUENTA_PGCP { get; set; }
        public virtual DbSet<PRE_CUENTA_RESTRINGIDA> PRE_CUENTA_RESTRINGIDA { get; set; }
        public virtual DbSet<PRE_DETALLE_HOJA_ARQUEO> PRE_DETALLE_HOJA_ARQUEO { get; set; }
        public virtual DbSet<PRE_DOCUMENTO_APLICACION> PRE_DOCUMENTO_APLICACION { get; set; }
        public virtual DbSet<PRE_DOCUMENTO_CONTABLE> PRE_DOCUMENTO_CONTABLE { get; set; }
        public virtual DbSet<PRE_EXP_CENTRO_COSTE> PRE_EXP_CENTRO_COSTE { get; set; }
        public virtual DbSet<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }
        public virtual DbSet<PRE_EXPEDIENTE_ADMINISTRATIVO> PRE_EXPEDIENTE_ADMINISTRATIVO { get; set; }
        public virtual DbSet<PRE_EXPEDIENTE_CONTABLE> PRE_EXPEDIENTE_CONTABLE { get; set; }
        public virtual DbSet<PRE_EXTRAPRESUPUESTARIA> PRE_EXTRAPRESUPUESTARIA { get; set; }
        public virtual DbSet<PRE_FACTURA_COMPRA> PRE_FACTURA_COMPRA { get; set; }
        public virtual DbSet<PRE_FORMA_PAGO> PRE_FORMA_PAGO { get; set; }
        public virtual DbSet<PRE_HOJA_ARQUEO> PRE_HOJA_ARQUEO { get; set; }
        public virtual DbSet<PRE_LINEA_TESORERIA> PRE_LINEA_TESORERIA { get; set; }
        public virtual DbSet<PRE_MODIF_CREDITO_PRESUPUESTO> PRE_MODIF_CREDITO_PRESUPUESTO { get; set; }
        public virtual DbSet<PRE_MODIFICACION_CREDITO> PRE_MODIFICACION_CREDITO { get; set; }
        public virtual DbSet<PRE_MONEDA> PRE_MONEDA { get; set; }
        public virtual DbSet<PRE_ORIGEN> PRE_ORIGEN { get; set; }
        public virtual DbSet<PRE_PARAMETROS> PRE_PARAMETROS { get; set; }
        public virtual DbSet<PRE_PRESUPUESTO> PRE_PRESUPUESTO { get; set; }
        public virtual DbSet<PRE_PROCEDENCIA> PRE_PROCEDENCIA { get; set; }
        public virtual DbSet<PRE_PROGRAMA> PRE_PROGRAMA { get; set; }
        public virtual DbSet<PRE_PROVEEDOR> PRE_PROVEEDOR { get; set; }
        public virtual DbSet<pre_proveedor_expediente_contable> pre_proveedor_expediente_contable { get; set; }
        public virtual DbSet<PRE_SENALAMIENTO> PRE_SENALAMIENTO { get; set; }
        public virtual DbSet<PRE_SENALAMIENTO_DOCUMENTO> PRE_SENALAMIENTO_DOCUMENTO { get; set; }
        public virtual DbSet<PRE_SUBCONCEPTO> PRE_SUBCONCEPTO { get; set; }
        public virtual DbSet<PRE_TESORERIA> PRE_TESORERIA { get; set; }
        public virtual DbSet<PRE_TESORERIA_DOCUMENTO> PRE_TESORERIA_DOCUMENTO { get; set; }
        public virtual DbSet<PRE_TIPO_CONTRATO> PRE_TIPO_CONTRATO { get; set; }
        public virtual DbSet<PRE_TIPO_DOCUMENTO> PRE_TIPO_DOCUMENTO { get; set; }
        public virtual DbSet<PRE_TIPO_EXTRAP> PRE_TIPO_EXTRAP { get; set; }
        public virtual DbSet<PRE_TIPO_MODIFICACION_CREDITO> PRE_TIPO_MODIFICACION_CREDITO { get; set; }
        public virtual DbSet<PRE_TIPO_PAGO> PRE_TIPO_PAGO { get; set; }
        public virtual DbSet<PRE_TIPO_REGISTRO> PRE_TIPO_REGISTRO { get; set; }
        public virtual DbSet<Proveedor_UIMPPRESUP> Proveedor_UIMPPRESUP { get; set; }
        public virtual DbSet<Provincia> Provincias { get; set; }
        public virtual DbSet<Tercero_UIMPPRESUP> Tercero_UIMPPRESUP { get; set; }
        public virtual DbSet<User> USUARIOs { get; set; }
        public virtual DbSet<PRE_BACKUP_DETALLE_HOJA_ARQUEO> PRE_BACKUP_DETALLE_HOJA_ARQUEO { get; set; }
        public virtual DbSet<PRE_BACKUP_DOCUMENTO_APLICACION> PRE_BACKUP_DOCUMENTO_APLICACION { get; set; }
        public virtual DbSet<PRE_BACKUP_HOJA_ARQUEO> PRE_BACKUP_HOJA_ARQUEO { get; set; }
        public virtual DbSet<PRE_BACKUP_MODIF_CREDITO_PRESUPUESTO> PRE_BACKUP_MODIF_CREDITO_PRESUPUESTO { get; set; }
        public virtual DbSet<PRE_BACKUP_MODIFICACION_CREDITO> PRE_BACKUP_MODIFICACION_CREDITO { get; set; }
        public virtual DbSet<PRE_BACKUP_PRESUPUESTO> PRE_BACKUP_PRESUPUESTO { get; set; }
        public virtual DbSet<PRE_BACKUP_SENALAMIENTO> PRE_BACKUP_SENALAMIENTO { get; set; }
        public virtual DbSet<PRE_BACKUP_SENALAMIENTO_DOCUMENTO> PRE_BACKUP_SENALAMIENTO_DOCUMENTO { get; set; }
        public virtual DbSet<PRE_DOCUMENTOS_LISTADOS_DOCUMENTOS_INTERVENCION> PRE_DOCUMENTOS_LISTADOS_DOCUMENTOS_INTERVENCION { get; set; }
        public virtual DbSet<PRE_LISTADOS_DOCUMENTOS_INTERVENCION> PRE_LISTADOS_DOCUMENTOS_INTERVENCION { get; set; }
        public virtual DbSet<PRE_SENALAMIENTOS_LISTADOS_DOCUMENTOS_INTERVENCION> PRE_SENALAMIENTOS_LISTADOS_DOCUMENTOS_INTERVENCION { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<dtproperty>()
                .Property(e => e.property)
                .IsUnicode(false);

            modelBuilder.Entity<dtproperty>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<Pais>()
                .Property(e => e.Pais1)
                .IsUnicode(false);

            modelBuilder.Entity<Pais>()
                .HasMany(e => e.Provincias)
                .WithRequired(e => e.Pais)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRE_ARTICULO>()
                .Property(e => e.ART_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_ARTICULO>()
                .Property(e => e.ART_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CAPITULO>()
                .Property(e => e.CAP_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CAPITULO>()
                .Property(e => e.CAP_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CENTRO_COSTE>()
                .Property(e => e.CEN_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CENTRO_COSTE>()
                .HasMany(e => e.PRE_EXP_CENTRO_COSTE)
                .WithRequired(e => e.PRE_CENTRO_COSTE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRE_CONCEPTO>()
                .Property(e => e.CON_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CONCEPTO>()
                .Property(e => e.CON_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CUENTA_PGCP>()
                .Property(e => e.CUEP_NUMERO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CUENTA_PGCP>()
                .Property(e => e.CUEP_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CUENTA_RESTRINGIDA>()
                .Property(e => e.CUE_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CUENTA_RESTRINGIDA>()
                .Property(e => e.CUE_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CUENTA_RESTRINGIDA>()
                .Property(e => e.CUE_ENTIDAD)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CUENTA_RESTRINGIDA>()
                .Property(e => e.CUE_CC)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CUENTA_RESTRINGIDA>()
                .Property(e => e.CUE_DIRECCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_CUENTA_RESTRINGIDA>()
                .HasMany(e => e.PRE_EXP_EXTRAPRE)
                .WithOptional(e => e.PRE_CUENTA_RESTRINGIDA)
                .HasForeignKey(e => e.CUE_CODIGO);

            modelBuilder.Entity<PRE_CUENTA_RESTRINGIDA>()
                .HasMany(e => e.PRE_EXP_EXTRAPRE1)
                .WithOptional(e => e.PRE_CUENTA_RESTRINGIDA1)
                .HasForeignKey(e => e.CUE_CODIGO_PAGADOR);

            modelBuilder.Entity<PRE_DETALLE_HOJA_ARQUEO>()
                .Property(e => e.DET_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_DOCUMENTO_APLICACION>()
                .Property(e => e.DOCA_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_DOCUMENTO_APLICACION>()
                .Property(e => e.DOCA_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_DOCUMENTO_CONTABLE>()
                .Property(e => e.DOC_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_DOCUMENTO_CONTABLE>()
                .Property(e => e.DOC_NUMERO_CHEQUE)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_DOCUMENTO_CONTABLE>()
                .Property(e => e.DOC_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_DOCUMENTO_CONTABLE>()
                .Property(e => e.DOC_FACTURA)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_EXP_CENTRO_COSTE>()
                .Property(e => e.EXP_CEN_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_EXP_EXTRAPRE>()
                .Property(e => e.EXP_EXTRAP_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_EXP_EXTRAPRE>()
                .Property(e => e.EXP_EXTRAP_TEXTO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_EXP_EXTRAPRE>()
                .Property(e => e.EXP_EXTRAP_NUMERO_CHEQUE)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_EXP_EXTRAPRE>()
                .Property(e => e.CUEP_NUMERO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_EXP_EXTRAPRE>()
                .HasMany(e => e.PRE_EXP_EXTRAPRE1)
                .WithOptional(e => e.PRE_EXP_EXTRAPRE2)
                .HasForeignKey(e => e.EXP_EXTRAP_CODIGO_ENLAZADO);

            modelBuilder.Entity<PRE_EXPEDIENTE_ADMINISTRATIVO>()
                .Property(e => e.ANU_COD_ANUALIDAD)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PRE_EXPEDIENTE_ADMINISTRATIVO>()
                .Property(e => e.EA_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_EXPEDIENTE_CONTABLE>()
                .Property(e => e.EXP_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_EXPEDIENTE_CONTABLE>()
                .Property(e => e.EXP_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_EXPEDIENTE_CONTABLE>()
                .HasMany(e => e.PRE_EXP_CENTRO_COSTE)
                .WithRequired(e => e.PRE_EXPEDIENTE_CONTABLE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRE_EXTRAPRESUPUESTARIA>()
                .Property(e => e.EXTRAPRE_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_EXTRAPRESUPUESTARIA>()
                .HasMany(e => e.PRE_EXP_EXTRAPRE)
                .WithRequired(e => e.PRE_EXTRAPRESUPUESTARIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.EJERCICIO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.PROV_NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.PROV_NIF)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.NCERTIFICADO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.FA_NUM_FACTURA)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.FA_IMPORTE_INTEGRO)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.FA_IMPORTE_BOE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.FA_IMPORTE_GARANTIA)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.FA_BASE_IMPONIBLE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.FA_IVA)
                .HasPrecision(5, 2);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.FA_RETENCION)
                .HasPrecision(5, 2);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.FA_IMPORTE_IVA)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.FA_IMPORTE_RETENCION)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_FACTURA_COMPRA>()
                .Property(e => e.APP_PRESUP)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_FORMA_PAGO>()
                .Property(e => e.FOR_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_LINEA_TESORERIA>()
                .Property(e => e.LIN_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_LINEA_TESORERIA>()
                .Property(e => e.LIN_ORIGEN_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_MODIF_CREDITO_PRESUPUESTO>()
                .Property(e => e.MODP_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_MODIF_CREDITO_PRESUPUESTO>()
                .Property(e => e.MODP_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_MODIFICACION_CREDITO>()
                .Property(e => e.MOD_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_MODIFICACION_CREDITO>()
                .HasMany(e => e.PRE_MODIF_CREDITO_PRESUPUESTO)
                .WithRequired(e => e.PRE_MODIFICACION_CREDITO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRE_MONEDA>()
                .Property(e => e.MON_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_ORIGEN>()
                .Property(e => e.ORI_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PARAMETROS>()
                .Property(e => e.PAR_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PARAMETROS>()
                .Property(e => e.PAR_VALOR)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PRESUPUESTO>()
                .Property(e => e.PRE_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PRESUPUESTO>()
                .Property(e => e.PRE_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_PRESUPUESTO>()
                .Property(e => e.PRE_IMPORTE_MODIFICACIONES)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_PRESUPUESTO>()
                .HasMany(e => e.PRE_MODIF_CREDITO_PRESUPUESTO)
                .WithRequired(e => e.PRE_PRESUPUESTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRE_PROCEDENCIA>()
                .Property(e => e.PROC_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROCEDENCIA>()
                .Property(e => e.PROC_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROGRAMA>()
                .Property(e => e.PRO_NUMERO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROGRAMA>()
                .Property(e => e.PRO_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_NIF)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_DIRECCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_POBLACION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_CODIGO_POSTAL)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_TELEFONO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_PERSONA_CONTACTO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_COD_PROVEEDOR)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_CC_CE)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_CC_CO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_CC_DC)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_CC_NC)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_NOMBRE_SUCURSAL)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_DIR_SUCURSAL)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_CP_SUCURSAL)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.PROV_POBLACION_SUCURSAL)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .Property(e => e.prov_iban)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .HasMany(e => e.PRE_EXP_EXTRAPRE)
                .WithOptional(e => e.PRE_PROVEEDOR)
                .HasForeignKey(e => e.PROV_CODIGO_PROVEEDOR);

            modelBuilder.Entity<PRE_PROVEEDOR>()
                .HasMany(e => e.PRE_EXP_EXTRAPRE1)
                .WithOptional(e => e.PRE_PROVEEDOR1)
                .HasForeignKey(e => e.PROV_CODIGO_TERCERO);

            modelBuilder.Entity<PRE_SENALAMIENTO>()
                .HasMany(e => e.PRE_SENALAMIENTO_DOCUMENTO)
                .WithRequired(e => e.PRE_SENALAMIENTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRE_SUBCONCEPTO>()
                .Property(e => e.SUB_NUMERO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_SUBCONCEPTO>()
                .Property(e => e.SUB_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_SUBCONCEPTO>()
                .Property(e => e.SUB_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TESORERIA>()
                .Property(e => e.TES_TOTAL_IMPORTE_LIQUIDO)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_TESORERIA>()
                .Property(e => e.TES_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TESORERIA>()
                .Property(e => e.TES_NUMERO_CHEQUE)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TESORERIA>()
                .Property(e => e.TES_APLICACION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TESORERIA>()
                .Property(e => e.TesoreriaID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TESORERIA>()
                .HasMany(e => e.PRE_TESORERIA_DOCUMENTO)
                .WithRequired(e => e.PRE_TESORERIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRE_TESORERIA_DOCUMENTO>()
                .Property(e => e.TESD_IMPORTE_LIQUIDO)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_TESORERIA_DOCUMENTO>()
                .Property(e => e.TESD_NUMERO_CHEQUE)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TESORERIA_DOCUMENTO>()
                .Property(e => e.TESD_DOCUMENTO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TESORERIA_DOCUMENTO>()
                .Property(e => e.TESD_APLICACION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TESORERIA_DOCUMENTO>()
                .Property(e => e.TESD_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TIPO_CONTRATO>()
                .Property(e => e.TIPC_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TIPO_DOCUMENTO>()
                .Property(e => e.TIPD_NOMBRE_CORTO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TIPO_DOCUMENTO>()
                .Property(e => e.TIPD_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TIPO_DOCUMENTO>()
                .Property(e => e.TIPD_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TIPO_EXTRAP>()
                .Property(e => e.TIP_EXTRAP_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TIPO_MODIFICACION_CREDITO>()
                .Property(e => e.TIPM_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TIPO_MODIFICACION_CREDITO>()
                .Property(e => e.TIPM_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TIPO_MODIFICACION_CREDITO>()
                .HasMany(e => e.PRE_MODIFICACION_CREDITO)
                .WithOptional(e => e.PRE_TIPO_MODIFICACION_CREDITO)
                .HasForeignKey(e => e.TIPM_CODIGO_I);

            modelBuilder.Entity<PRE_TIPO_MODIFICACION_CREDITO>()
                .HasMany(e => e.PRE_MODIFICACION_CREDITO1)
                .WithOptional(e => e.PRE_TIPO_MODIFICACION_CREDITO1)
                .HasForeignKey(e => e.TIPM_CODIGO_G);

            modelBuilder.Entity<PRE_TIPO_PAGO>()
                .Property(e => e.TIPP_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_TIPO_REGISTRO>()
                .Property(e => e.TIPR_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<Provincia>()
                .Property(e => e.Provincia1)
                .IsUnicode(false);

            modelBuilder.Entity<Provincia>()
                .Property(e => e.ComAutonoma)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.USU_NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.USU_APELLIDOS)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.USU_LOGIN)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.USU_PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.USU_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_BACKUP_DETALLE_HOJA_ARQUEO>()
                .Property(e => e.DET_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_BACKUP_DOCUMENTO_APLICACION>()
                .Property(e => e.DOCA_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_BACKUP_DOCUMENTO_APLICACION>()
                .Property(e => e.DOCA_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_BACKUP_MODIF_CREDITO_PRESUPUESTO>()
                .Property(e => e.MODP_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_BACKUP_MODIF_CREDITO_PRESUPUESTO>()
                .Property(e => e.MODP_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_BACKUP_MODIFICACION_CREDITO>()
                .Property(e => e.MOD_DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_BACKUP_PRESUPUESTO>()
                .Property(e => e.PRE_I_G)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_BACKUP_PRESUPUESTO>()
                .Property(e => e.PRE_IMPORTE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_BACKUP_PRESUPUESTO>()
                .Property(e => e.PRE_IMPORTE_MODIFICACIONES)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRE_LISTADOS_DOCUMENTOS_INTERVENCION>()
                .Property(e => e.Registro)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_LISTADOS_DOCUMENTOS_INTERVENCION>()
                .Property(e => e.observaciones)
                .IsUnicode(false);
        }
    }
}
