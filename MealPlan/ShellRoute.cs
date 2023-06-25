using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlan
{
    public class ShellRoute
    {
        public string Route { get; }
        public Func<bool> AuthorizationCheck { get; }

        public ShellRoute(string route, Func<bool> authorizationCheck)
        {
            Route = route;
            AuthorizationCheck = authorizationCheck;
        }
    }
}
