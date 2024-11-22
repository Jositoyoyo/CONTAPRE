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

    public class AdministrativeRecordsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<AdministrativeRecordsDataContext> Context = new Lazy<AdministrativeRecordsDataContext>(() => new AdministrativeRecordsDataContext());

        #endregion

        #region Public Properties

        public static AdministrativeRecordsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_EXPEDIENTE_ADMINISTRATIVO> GetSpends(int? exerciseYear, int? recordNumber, int? provenanceId, string description)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_ADMINISTRATIVO>();

                var cmd = Db.GetStoredProcCommand("USP_AdministrativeRecords_GetSpends");

                if (exerciseYear != null)
                {
                    Db.AddInParameter(cmd, "@exerciseYear", DbType.Int32, exerciseYear);
                }

                if (recordNumber != null)
                {
                    Db.AddInParameter(cmd, "@recordNumber", DbType.Int32, recordNumber);
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    Db.AddInParameter(cmd, "@description", DbType.String, description);
                }

                if (provenanceId != null)
                {
                    Db.AddInParameter(cmd, "@provenanceCode", DbType.Int32, provenanceId);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var spend = new PRE_EXPEDIENTE_ADMINISTRATIVO
                                            {
                                                    EA_CODIGO = this.DbInteger(reader["EA_CODIGO"]),
                                                    EA_NUMERO = this.DbIntegerNullable(reader["EA_NUMERO"]),
                                                    EA_ANO_EJERCICIO = this.DbShortNullable(reader["EA_ANO_EJERCICIO"]),
                                                    PROC_DESCRIPCION = this.DbString(reader["PROC_DESCRIPCION"]),
                                                    EA_DESCRIPCION = this.DbString(reader["EA_DESCRIPCION"]),
                                                    TIPC_DESCRIPCION = this.DbString(reader["TIPC_DESCRIPCION"])
                                            };

                        result.Add(spend);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PRE_EXPEDIENTE_ADMINISTRATIVO GetById(int administrativeRecordId)
        {
            try
            {
                PRE_EXPEDIENTE_ADMINISTRATIVO result = null;

                var cmd = Db.GetStoredProcCommand("USP_AdministrativeRecords_GetById");

                Db.AddInParameter(cmd, "@id", DbType.Int32, administrativeRecordId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_EXPEDIENTE_ADMINISTRATIVO
                                         {
                                                 EA_CODIGO = administrativeRecordId,
                                                 EA_NUMERO = this.DbIntegerNullable(reader["EA_NUMERO"]),
                                                 EA_ANO_EJERCICIO = this.DbShortNullable(reader["EA_ANO_EJERCICIO"]),
                                                 PROC_CODIGO = this.DbIntegerNullable(reader["PROC_CODIGO"]),
                                                 ANU_COD_ANUALIDAD = this.DbDecimalNullable(reader["ANU_COD_ANUALIDAD"]),
                                                 EA_DESCRIPCION = this.DbString(reader["EA_DESCRIPCION"]),
                                                 TIPC_CODIGO = this.DbByteNullable(reader["TIPC_CODIGO"])
                                         };
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AdministrativeRecords_Insert");

                if (administrativeRecord.EA_NUMERO != null)
                {
                    Db.AddInParameter(cmd, "@eaNumber", DbType.Int32, administrativeRecord.EA_NUMERO);
                }

                Db.AddInParameter(cmd, "@eaYear", DbType.Int32, administrativeRecord.EA_ANO_EJERCICIO);

                if (administrativeRecord.PROC_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@provenanceCode", DbType.Int32, administrativeRecord.PROC_CODIGO);
                }

                Db.AddInParameter(cmd, "@eaDescription", DbType.String, administrativeRecord.EA_DESCRIPCION);

                if (administrativeRecord.TIPC_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@contractCode", DbType.Int32, administrativeRecord.TIPC_CODIGO);
                }

                Db.AddInParameter(cmd, "@userId", DbType.Int32, administrativeRecord.USU_CODIGO);

                var code = ResponseCode.Invalid;
                var id = 0;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        code = ResponseCode.Ok;
                        id = result;
                    }
                }

                return new Response
                               {
                                       ResponseCode = code,
                                       ResponseMethod = id
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AdministrativeRecords_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, administrativeRecord.EA_CODIGO);

                if (administrativeRecord.EA_NUMERO != null)
                {
                    Db.AddInParameter(cmd, "@eaNumber", DbType.Int32, administrativeRecord.EA_NUMERO);
                }

                Db.AddInParameter(cmd, "@eaYear", DbType.Int32, administrativeRecord.EA_ANO_EJERCICIO);

                if (administrativeRecord.PROC_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@provenanceCode", DbType.Int32, administrativeRecord.PROC_CODIGO);
                }

                Db.AddInParameter(cmd, "@eaDescription", DbType.String, administrativeRecord.EA_DESCRIPCION);

                if (administrativeRecord.TIPC_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@contractCode", DbType.Int32, administrativeRecord.TIPC_CODIGO);
                }

                Db.AddInParameter(cmd, "@userId", DbType.Int32, administrativeRecord.USU_CODIGO);

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

        public Response DeleteAdministrativeRecord(int id, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AdministrativeRecords_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, userId);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.NotFound;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                        }
                    }
                }

                return new Response
                               {
                                       ResponseCode = code,
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNextOrder(int year, int provenanceId)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AdministrativeRecords_GetNextNumber");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@provenanceId", DbType.Int32, provenanceId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbInteger(reader["Result"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}