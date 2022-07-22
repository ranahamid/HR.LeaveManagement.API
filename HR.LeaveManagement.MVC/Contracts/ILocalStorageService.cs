using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILocalStorageService
    {
        void ClearStorage(List<string> keys);
        bool Exist(string key);
        T GetStorageValue<T>(string key);
        void SetStorageValue<T>(string key, T value);

    }
}
