using System;
using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace CmnSoftwareBackend.API.Filters
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            //will add control
            //Allow all authenticated users to see the Dashboard(potentially dangerous)
            //return httpContext.User.Identity.IsAuthenticated;
            return true;
        }
    }
}

