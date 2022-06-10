using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static TestLib.Models.ConfigModels.GeneralSettingsModel;

namespace TestLib.Models.ConfigModels
{
    public class ConfigModel
    {
        public ConfigModel()
        {
            this.GeneralSettings = new GeneralSettingsModel();
            this.UserData = new UserModel();

            this._developmentEnvironment = new EnvironmentModel();
            this._stagingEnvironment = new EnvironmentModel();
            this._productionEnvironment = new EnvironmentModel();
        }
        public GeneralSettingsModel GeneralSettings { get; set; }
        public UserModel UserData { get; set; }
        private EnvironmentModel _developmentEnvironment { get; set; }
        [JsonIgnore]
        public EnvironmentModel DevelopmentEnvironment
        {
            get
            {
                if (!string.Equals(GeneralSettings.ActualEnvironment, Enum.GetName(Environments.Development)))
                {
                    this._developmentEnvironment = null;
                }
                return this._developmentEnvironment;
            }

            set
            {
                this._developmentEnvironment = value;
            }
        }

        private EnvironmentModel _stagingEnvironment { get; set; }
        [JsonIgnore]
        public EnvironmentModel StagingEnvironment
        {
            get
            {
                if (!string.Equals(GeneralSettings.ActualEnvironment, Enum.GetName(Environments.Staging)))
                {
                    this._stagingEnvironment = null;
                }
                return this._stagingEnvironment;
            }
            set
            {
                this._stagingEnvironment = value;
            }
        }


        private EnvironmentModel _productionEnvironment { get; set; }
        [JsonIgnore]
        public EnvironmentModel ProductionEnvironment
        {
            get
            {
                if (!string.Equals(GeneralSettings.ActualEnvironment, Enum.GetName(Environments.Production)))
                {
                    this._productionEnvironment = null;
                }
                return this._productionEnvironment;
            }
            set
            {
                this._productionEnvironment = value;
            }
        }


        private EnvironmentModel _actualEnvironment { get; set; }
        [JsonIgnore]
        public EnvironmentModel ActualEnvironment
        {
            get
            {
                switch (GeneralSettings.RawActualEnvironemt)
                {
                    case Environments.Development:
                        this._actualEnvironment = this.DevelopmentEnvironment;
                        break;

                    case Environments.Staging:
                        this._actualEnvironment = this.StagingEnvironment;
                        break;

                    case Environments.Production:
                        this._actualEnvironment = this.ProductionEnvironment;
                        break;

                    default:
                        this._actualEnvironment = null;
                        break;
                }
                return this._actualEnvironment;

            }
            set
            {
                this._actualEnvironment = value;
            }
        }

       
     

    }
}
