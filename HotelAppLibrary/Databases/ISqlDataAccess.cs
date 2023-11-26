using System.Collections.Generic;

namespace HotelAppLibrary.Databases
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatment,
                               U parameters,
                               string connectionStringName,
                               bool isStoreProcedure = false);
        void SaveData<T>(string sqlStatment,
                         T parameters,
                         string connectionStringName,
                         bool isStoreProcedure = false);
    }
}