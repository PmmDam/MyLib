using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Models.ConfigModels
{
    public class EnvironmentModel
    {
        public string EnvironmentName { get; set; } 
        public DataLayerModel DataLayer { get; set; }
        public EnvironmentModel()
        {
            DataLayer = new DataLayerModel();
        }
        
    }
}
