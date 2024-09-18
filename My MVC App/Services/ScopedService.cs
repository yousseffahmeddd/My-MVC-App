using System;

namespace My_MVC_App.Services
{
    public class ScopedService : IScopedService
    {
        public Guid Guid { get; set; }

        public ScopedService()
        {
            Guid = Guid.NewGuid(); //Generate new universal ID
        }

        public string GetGuid()
        {
            return Guid.ToString();
        }

        public override string ToString()
        {
            return Guid.ToString();
        }
    }
}
