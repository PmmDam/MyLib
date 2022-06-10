using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Models.ConfigModels
{
    public class DataLayerModel
    {
        public string ServerName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string DbName { get; set; }
        public string ConnectionString { get; set; }
    }
}
