using System;

namespace My_MVC_App.Services
{
    public interface IScopedService
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
