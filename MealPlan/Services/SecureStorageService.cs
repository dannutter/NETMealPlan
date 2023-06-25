using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlan.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        public SecureStorageService() 
        { 
        }
        public async Task<string> Get(string pin)
        {
            try
            {
                var ReturnedPin = await SecureStorage.Default.GetAsync("pin");
                if (ReturnedPin==null)
                {
                    return "null";
                }
                else
                {
                    if (ReturnedPin == pin)
                    {
                        await SecureStorage.SetAsync("Auth", "true");
                        return "true";                       
                    }
                    return "false";
                }
            }
            catch
            {
                return "null";
            }
        }
        
        public async Task Save(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }
        public async Task LogOut()
        {
            await SecureStorage.SetAsync("Auth", "false");
        }

        public async Task<bool> CheckAuth()
        {
            var ReturnedAuth = await SecureStorage.Default.GetAsync("Auth");
            if (ReturnedAuth==null)
            {
                return false;
            }
            else
            {
                if (ReturnedAuth=="true")
                {
                    return true;
                }
                return false;
            }
        }

    }
}
