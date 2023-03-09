using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Service.Repository
{
    public interface IRefreshService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }

    public class RefreshService : IRefreshService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
