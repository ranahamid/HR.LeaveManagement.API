using System;
using System.Collections.Generic;
using Hanssens.Net;
using HR.LeaveManagement.MVC.Contracts;

namespace HR.LeaveManagement.MVC.Services
{
    public class LocalStorageService: ILocalStorageService
    {
        private LocalStorage _storage;

        public LocalStorageService()
        {
            var config = new LocalStorageConfiguration
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "HR.LEAVEMGMT"
            };
            _storage = new LocalStorage(config);
        }

        public void ClearStorage(List<string> keys)
        {
            foreach (var itemKey in keys)
            {
             _storage.Remove(key:itemKey);
            }
        }

        public bool Exist(string key)
        {
            return _storage.Exists(key: key);
        }

        public T GetStorageValue<T>(string key)
        {
           var data = _storage.Get<T>(key: key);
           return data;
        }

        public void SetStorageValue<T>(string key, T value)
        {
            _storage.Store(key:key,value);
            _storage.Persist();
        }
    }
}
