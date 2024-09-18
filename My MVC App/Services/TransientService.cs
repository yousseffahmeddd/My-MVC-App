using System;

namespace My_MVC_App.Services
{
    public class TransientService : ITransientService
    {
        public Guid Guid { get; set; }

        public TransientService()
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
