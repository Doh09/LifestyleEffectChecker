using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Repository.ServiceGateway;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository
{
    class JournalRepository : IRepository<Journal>
    {
        private readonly SQLiteConnection _connection;

        private static JournalRepository instance;

        //Used to check for connection
        private readonly ICheckNetwork _netWork = DependencyService.Get<ICheckNetwork>();
        private readonly IRepository<Journal> _serviceGateway = ServiceGatewayFacade.GetJournalServiceGateway();

        public static JournalRepository GetInstance()
        {
            if (instance == null)
                instance = new JournalRepository();
            return instance;
        }

        private JournalRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Journal>();
            MakeMockJournals();
        }

        private async void MakeMockJournals()
        {
            if (ReadAll().Result.ToList().Count < 1) {  //if no journals for testing.

                var JournalJ = new Journal() { Name = "123Adventure Journal", JournalChildren = new List<PartInformation>() };
                var PartInformation1 = new PartInformation();
                PartInformation1.ID = 1;
                var PartInformation2 = new PartInformation();
                PartInformation2.ID = 2;
                JournalJ.JournalChildren.Add(PartInformation1);
                JournalJ.JournalChildren.Add(PartInformation2);
                await Create(JournalJ);
                await Create(new Journal() { Name = "123Food Journal", JournalChildren = new List<PartInformation>() });
                await Create(new Journal() { Name = "123Excercise Journal", JournalChildren = new List<PartInformation>() });
            }
        }

        public async Task<Journal> Create(Journal obj)
        {

            //If there is online connection, send signal to the RestAPI
            //if (_netWork.IsOnline())
            //{
            //    await new JournalServiceGateway().Create(obj);
            //}
            //_connection.UpdateWithChildren(obj);

            if (obj.JournalChildren != null && obj.JournalChildren.Count != 0)
                _connection.InsertWithChildren(obj, true);
            else
            {
                _connection.Insert(obj);
            }

            return await Task.FromResult(obj);
        }

        public async Task<Journal> Read(int id, bool goOnline = false)
        {
            //If there is online connection, send signal to the RestAPI
            //if (goOnline)
            //{
            //    if (_netWork.IsOnline())
            //    {
            //        return await _serviceGateway.Read(id);
            //    }
            //}
            return await Task.FromResult(_connection.Table<Journal>().FirstOrDefault(journal => journal.ID == id));
        }

        public async Task<IEnumerable<Journal>> ReadAll(bool goOnline = false)
        {
            //If there is online connection, send signal to the RestAPI
            //if (goOnline)
            //{
            //    if (_netWork.IsOnline())
            //    {
            //        return await _serviceGateway.ReadAll();
            //    }
            //}
            //var journals = (from t in _connection.Table<Journal>() select t).ToList();
            var journals = (from t in _connection.Table<Journal>() select t);
            var toReturn = await Task.FromResult(journals);

            return toReturn;
        }

        public async Task<Journal> Update(Journal obj)
        {
            //If there is online connection, send signal to the RestAPI
            //if (_netWork.IsOnline())
            //{
            //    await _serviceGateway.Update(obj);
            //}

            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {
            //If there is online connection, send signal to the RestAPI
            //if (_netWork.IsOnline())
            //{
            //    await _serviceGateway.Delete(id);
            //}
            _connection.Delete<Journal>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
