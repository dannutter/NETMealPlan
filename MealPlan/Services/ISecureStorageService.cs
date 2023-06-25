using System;

namespace MealPlan.Services
{
    public interface ISecureStorageService
    {
        Task Save(string key,string value);
        Task<string> Get(string pin);
        Task LogOut();
        Task<bool> CheckAuth();
    }
}
