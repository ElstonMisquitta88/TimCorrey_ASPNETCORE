using Microsoft.Extensions.Configuration;

namespace DataLibrary.Db;

public interface IDataAccess
{
    Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
    Task<int> SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
}