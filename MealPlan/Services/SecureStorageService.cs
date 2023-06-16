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
                        return "true";
                    }
                    return "Login Failed";
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
            
    }
}
