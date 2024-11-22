namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Data;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class TreasuryLinesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<TreasuryLinesDataContext> Context = new Lazy<TreasuryLinesDataContext>(() => new TreasuryLinesDataContext());

        #endregion

        #region Public Properties

        public static TreasuryLinesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_LINEA_TESORERIA> GetTreasuryLines()
        {
            try
            {
                var result = new List<PRE_LINEA_TESORERIA>();

                var cmd = Db.GetStoredProcCommand("USP_TreasuryLines_GetAll");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var treasury = new PRE_LINEA_TESORERIA
                                               {
                                                       LIN_NUMERO = this.DbInteger(reader["LIN_NUMERO"]),
                                                       LIN_DESCRIPCION = this.DbString(reader["LIN_DESCRIPCION"]),
                                                       LIN_ORIGEN_DESCRIPCION = this.DbString(reader["LIN_ORIGEN_DESCRIPCION"]),
                                                       LIN_ORIGEN_CODIGO = this.DbInteger(reader["LIN_ORIGEN_CODIGO"])
                                               };

                        result.Add(treasury);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertTreasuryLine(PRE_LINEA_TESORERIA treasuryLine)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_TreasuryLines_Insert");

                Db.AddInParameter(cmd, "@id", DbType.Int32, treasuryLine.LIN_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, treasuryLine.LIN_DESCRIPCION);
                Db.AddInParameter(cmd, "@originDescription", DbType.String, treasuryLine.LIN_ORIGEN_DESCRIPCION);
                Db.AddInParameter(cmd, "@originCode", DbType.Int32, treasuryLine.LIN_ORIGEN_CODIGO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, treasuryLine.USU_CODIGO);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.Found;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                        }
                    }
                }

                return new Response
                               {
                                       ResponseCode = code
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateTreasuryLine(PRE_LINEA_TESORERIA treasuryLine)
        {
            try
            {
                if (treasuryLine?.LIN_NUMERO == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_TreasuryLines_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, treasuryLine.LIN_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, treasuryLine.LIN_DESCRIPCION);
                Db.AddInParameter(cmd, "@originDescription", DbType.String, treasuryLine.LIN_ORIGEN_DESCRIPCION);
                Db.AddInParameter(cmd, "@originCode", DbType.Int32, treasuryLine.LIN_ORIGEN_CODIGO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, treasuryLine.USU_CODIGO);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.Found;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
                        }
                    }
                }

                return new Response
                               {
                                       ResponseCode = code
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response DeleteTreasuryLine(int treasuryLineId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_TreasuryLines_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, treasuryLineId);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
                            case 3:
                                code = ResponseCode.Found;

                                break;
                        }
                    }
                }

                return new Response
                               {
                                       ResponseCode = code
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}