using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    public class DataBaseException : Exception
    {
        public DataBaseException() { }
        public DataBaseException(string message) : base(message)
        {
        }

        public DataBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    class Util
    {
        public static void HandleSQLDBException(Exception exception)
        {
            if (exception is DbUpdateConcurrencyException concurrencyEx)
            {
                throw new DataBaseException();
            }

            if (exception is DbUpdateException dbUpdateEx)
            {
                if (dbUpdateEx.InnerException != null && dbUpdateEx.InnerException.InnerException != null)
                {
                    if (dbUpdateEx.InnerException.InnerException is SqlException sqlException)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:
                                throw new DataBaseException("O valor inserido está duplicando alguma propriedade única.");
                            case 547:
                                throw new DataBaseException("O valor inserido não respeita alguma restrição específica do modelo.");
                            case 2601:
                                throw new DataBaseException("O valor inserido está duplicando alguma propriedade única, possívelmente a chave primária.");
                            default:
                                throw new DataBaseException(dbUpdateEx.Message, dbUpdateEx.InnerException);
                        }
                    }
                    throw new DataBaseException(dbUpdateEx.Message, dbUpdateEx.InnerException);
                }
            }
        }
    }
}
