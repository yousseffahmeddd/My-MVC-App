using System;

namespace My_MVC_App.Services
{
    public class SingeltonService : ISingeltonService
    {
        public Guid Guid { get; set; }

        public SingeltonService()
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
