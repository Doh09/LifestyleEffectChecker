using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Repository.ServiceGateway;
using SQLite.Net;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository
{
    class PartInformationRepository : IRepository<PartInformation>
    {
        private readonly SQLiteConnection _connection;

        private static PartInformationRepository instance;

        //Used to check for connection
        private readonly ICheckNetwork _netWork = DependencyService.Get<ICheckNetwork>();
        private readonly IRepository<PartInformation> _serviceGateway = ServiceGatewayFacade.GetPartInformationServiceGateway();

        public static PartInformationRepository GetInstance()
        {
            if (instance == null)
                instance = new PartInformationRepository();
            return instance;
        }

        private PartInformationRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<PartInformation>();
        }

        public async Task<PartInformation> Create(PartInformation obj)
        {
            //If there is online connection, send signal to the RestAPI
            //if (_netWork.IsOnline())
            //{
            //    await _serviceGateway.Create(obj);
            //}
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<PartInformation> Read(int id, bool goOnline = false)
        {   //If there is online connection, send signal to the RestAPI
            //if (goOnline)
            //{
            //    if (_netWork.IsOnline())
            //    {
            //        return await _serviceGateway.Read(id);
            //    }
            //}
            return await Task.FromResult(_connection.Table<PartInformation>().FirstOrDefault(action => action.ID == id));
        }

        public async Task<IEnumerable<PartInformation>> ReadAll(bool goOnline = false)
        { //If there is online connection, send signal to the RestAPI
            //if (goOnline)
            //{
            //    if (_netWork.IsOnline())
            //    {
            //        return await _serviceGateway.ReadAll();
            //    }
            //}
            return await Task.FromResult((from t in _connection.Table<PartInformation>() select t).ToList());
        }

        public async Task<PartInformation> Update(PartInformation obj)
        {    //If there is online connection, send signal to the RestAPI
            //if (_netWork.IsOnline())
            //{
            //    await _serviceGateway.Update(obj);
            //}
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        { //If there is online connection, send signal to the RestAPI
            //if (_netWork.IsOnline())
            //{
            //    await _serviceGateway.Delete(id);
            //}
            _connection.Delete<PartInformation>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
