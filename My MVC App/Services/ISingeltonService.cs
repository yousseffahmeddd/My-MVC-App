using System;

namespace My_MVC_App.Services
{
    public interface ISingeltonService
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
