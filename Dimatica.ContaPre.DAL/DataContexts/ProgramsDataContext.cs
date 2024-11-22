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

    public class ProgramsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<ProgramsDataContext> Context = new Lazy<ProgramsDataContext>(() => new ProgramsDataContext());

        #endregion

        #region Public Properties

        public static ProgramsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_PROGRAMA> GetPrograms()
        {
            try
            {
                var result = new List<PRE_PROGRAMA>();

                var cmd = Db.GetStoredProcCommand("USP_Programs_GetAll");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var treasury = new PRE_PROGRAMA
                                               {
                                                       PRO_CODIGO = this.DbByte(reader["PRO_CODIGO"]),
                                                       PRO_CODIGO_AUX = this.DbInteger(reader["PRO_CODIGO"]),
                                                       PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                                                       PRO_DESCRIPCION = this.DbString(reader["PRO_DESCRIPCION"]),
                                                       PRO_POR_DEFECTO = this.DbBooleanBit(reader["PRO_POR_DEFECTO"])
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

        public Response InsertProgram(PRE_PROGRAMA program)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Programs_Insert");

                Db.AddInParameter(cmd, "@number", DbType.String, program.PRO_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, program.PRO_DESCRIPCION);
                Db.AddInParameter(cmd, "@isDefault", DbType.Boolean, program.PRO_POR_DEFECTO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, program.USU_CODIGO);

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

        public Response UpdateProgram(PRE_PROGRAMA program)
        {
            try
            {
                if (program?.PRO_CODIGO == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Programs_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, program.PRO_CODIGO);
                Db.AddInParameter(cmd, "@number", DbType.String, program.PRO_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, program.PRO_DESCRIPCION);
                Db.AddInParameter(cmd, "@isDefault", DbType.Boolean, program.PRO_POR_DEFECTO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, program.USU_CODIGO);

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

        public Response DeleteProgram(byte programId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Programs_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, programId);

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