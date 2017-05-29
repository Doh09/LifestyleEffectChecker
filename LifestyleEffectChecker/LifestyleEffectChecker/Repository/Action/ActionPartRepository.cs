using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models.Action;
using LifestyleEffectChecker.Repository.ServiceGateway;
using LifestyleEffectChecker.Repository.ServiceGateway.Action;
using SQLite.Net;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository.Action
{
    class ActionPartRepository : IRepository<ActionPart>
    {
        private readonly SQLiteConnection _connection;

        private static ActionPartRepository instance;

        //Used to check for connection
        private readonly ICheckNetwork _netWork = DependencyService.Get<ICheckNetwork>();
        private readonly IRepository<ActionPart> _serviceGateway = ServiceGatewayFacade.GetActionPartServiceGateway();


        public static ActionPartRepository GetInstance()
        {
            if (instance == null)
                instance = new ActionPartRepository();
            return instance;
        }

        private ActionPartRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<ActionPart>();
        }

        public async Task<ActionPart> Create(ActionPart obj)
        {
            //If there is online connection, send signal to the RestAPI
            if (_netWork.IsOnline())
            {
                await _serviceGateway.Create(obj);
            }
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<ActionPart> Read(int id, bool goOnline = false)
        {    //If there is online connection, send signal to the RestAPI
            if (goOnline)
            {
                if (_netWork.IsOnline())
                {
                    return await _serviceGateway.Read(id);
                }
            }
            return await Task.FromResult(_connection.Table<ActionPart>().FirstOrDefault(actionpart => actionpart.ID == id));
        }

        public async Task<IEnumerable<ActionPart>> ReadAll(bool goOnline = false)
        {
            //If there is online connection, send signal to the RestAPI
            if (goOnline)
            {
                if (_netWork.IsOnline())
                {
                    return await _serviceGateway.ReadAll();
                }
            }
            return await Task.FromResult((from t in _connection.Table<ActionPart>() select t).ToList());
        }

        public async Task<ActionPart> Update(ActionPart obj)
        {
            //If there is online connection, send signal to the RestAPI
            if (_netWork.IsOnline())
            {
                await _serviceGateway.Update(obj);
            }
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {
            //If there is online connection, send signal to the RestAPI
            if (_netWork.IsOnline())
            {
                await _serviceGateway.Delete(id);
            }
            _connection.Delete<ActionPart>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
