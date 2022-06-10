using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestLib.Models.ConfigModels
{
    public  class GeneralSettingsModel
    {
        public enum Environments
        {
            None,
            Development,
            Staging,
            Production
        }

        public bool LogEnabled { get; set; }
        private Environments _actualEnvironment { get; set; }
        [JsonIgnore]
        public Environments RawActualEnvironemt
        {
            get
            {
                return this._actualEnvironment;
            }
            set
            {
                this._actualEnvironment = value;
            }
        }
        public string ActualEnvironment
        {
            get
            {
                return Enum.GetName(this._actualEnvironment);
            }

            set
            {
                Environments result;

                if (Enum.TryParse<Environments>(value, out result))
                {
                    this._actualEnvironment = result;
                }
                else
                {
                    this._actualEnvironment = Environments.None;
                }

            }
        }
    }
}
