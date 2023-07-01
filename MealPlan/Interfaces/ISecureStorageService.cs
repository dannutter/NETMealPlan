using System;

namespace MealPlan.Interfaces
{
    public interface ISecureStorageService
    {
        Task Save(string key,string value);
        Task<string> Get(string pin);
        Task LogOut();
        Task<bool> CheckAuth();
    }
}
