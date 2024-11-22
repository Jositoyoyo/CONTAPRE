namespace Dimatica.ContaPre.DAL.Models
{
    #region NameSpaces

    using System;
    using System.Configuration;

    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    #endregion

    public class ContextModel
    {
        #region Static Fields and Constants

        public static Database Db;

        #endregion

        #region Constructors and Desctructors

        public ContextModel()
        {
            Db = new SqlDatabase(ConfigurationManager.ConnectionStrings["ContaPreModel"].ConnectionString);
        }

        #endregion

        #region Public Methods

        public string DbString(object obj)
        {
            return obj == DBNull.Value ? string.Empty : Convert.ToString(obj);
        }

        public byte DbByte(object obj)
        {
            return obj == DBNull.Value ? Convert.ToByte("10") : Convert.ToByte(obj);
        }

        public byte? DbByteNullable(object obj)
        {
            return obj == DBNull.Value ? (byte?)null : Convert.ToByte(obj);
        }

        public DateTime DbDate(object obj)
        {
            return obj == DBNull.Value ? new DateTime() : Convert.ToDateTime(obj);
        }

        public DateTime? DbDateNullable(object obj)
        {
            return obj == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(obj);
        }

        public int DbInteger(object obj)
        {
            return obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public int? DbIntegerNullable(object obj)
        {
            return obj == DBNull.Value ? (int?)null : Convert.ToInt32(obj);
        }

        public short DbShort(object obj)
        {
            return obj == DBNull.Value ? (short)0 : Convert.ToInt16(obj);
        }

        public short? DbShortNullable(object obj)
        {
            return obj == DBNull.Value ? (short?)null : Convert.ToInt16(obj);
        }

        public decimal DbDecimal(object obj)
        {
            return obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
        } 

        public decimal? DbDecimalNullable(object obj)
        {
            return obj == DBNull.Value ? (decimal?)null : Convert.ToDecimal(obj);
        }

        public double DbDouble(object obj)
        {
            return obj == DBNull.Value ? 0 : Convert.ToDouble(obj);
        }

        public Guid DbGuid(object obj)
        {
            return obj == DBNull.Value ? new Guid() : new Guid(obj.ToString());
        }

        public Guid? DbGuidNullable(object obj)
        {
            return obj == DBNull.Value ? (Guid?)null : new Guid(obj.ToString());
        }

        public bool DbBooleanBit(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }

            return Convert.ToInt32(obj) != 0;
        }

        public bool DbBooleanString(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }

            return Convert.ToString(obj).ToLower() == "true";
        }

        #endregion
    }
}