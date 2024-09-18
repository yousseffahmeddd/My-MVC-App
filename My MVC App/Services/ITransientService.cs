using System;

namespace My_MVC_App.Services
{
    public interface ITransientService
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
