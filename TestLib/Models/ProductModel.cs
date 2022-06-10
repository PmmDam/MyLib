using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestLib.Models
{
    public class ProductModel
    {
        private int _id { get; set; }
        [JsonIgnore]
        public int RawId
        {
            get { return _id; }
            set
            {
                this._id = value;
            }
        }
        public string Id
        {
            get
            {
                return this._id.ToString();
            }
            set
            {
                this._id = int.Parse(value);
            }
        }
        public string Name { get; set; }
        private double _price{ get; set; }
        [JsonIgnore]
        public double RawPrice
        {
            get
            {
                return this._price < 0 ? 0 : this._price;
            }
            set
            {
               
                this._price = value;
            }
        }
        public string Price
        {
            get
            {
                return this._price < 0 ? "Error (Precio negativo) " : this._price.ToString();
            }
            set
            {
                this._price = int.Parse(value);
            }
        }

    }
}
