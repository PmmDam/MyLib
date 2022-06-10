using Microsoft.Extensions.Configuration;
using MyLib.Json.Serializer;
using TestLib.Models.ConfigModels;

namespace TestLib
{
    internal class Program
    {   
        // Config json filePaths
        private static readonly string appConfigFilePath = Path.Combine(Environment.CurrentDirectory, "Data", "Config", "AppConfig.json");
        private static readonly string configDevFilePath = Path.Combine(Environment.CurrentDirectory, "Data", "Config", "Environments", "AppConfig_Development_Environment.json");
        private static readonly string configStagFilePath = Path.Combine(Environment.CurrentDirectory, "Data", "Config", "Environments", "AppConfig_Staging_Environment.json");
        private static readonly string configProdFilePath = Path.Combine(Environment.CurrentDirectory, "Data", "Config", "Environments", "AppConfig_Production_Environment.json");

        // Test FilePaths
        private static readonly string _testJsonFilePath001 = Path.Combine(Environment.CurrentDirectory, "Data", "TestFiles", "Json", "TestJsonFile_001.json");


        static async Task Main(string[] args)
        {

            #region Configuration

            Console.WriteLine("Configuration Test");
            Console.WriteLine("******************");

           


            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());

            configBuilder.AddJsonFile(appConfigFilePath, false, true);
            configBuilder.AddJsonFile(configDevFilePath, false, true);
            configBuilder.AddJsonFile(configStagFilePath, false, true);
            configBuilder.AddJsonFile(configProdFilePath, false, true);

            configBuilder.AddCommandLine(args);

            IConfiguration configTree = configBuilder.Build();

            ConfigModel config = configTree.Get<ConfigModel>();

            Console.WriteLine("\nConfig access test 01");
            Console.WriteLine(config.DevelopmentEnvironment.EnvironmentName);

            Console.WriteLine("\nConfig access test 02");
            Console.WriteLine(config.ActualEnvironment.EnvironmentName);

            Console.WriteLine("(This has to be equal)");
            Console.WriteLine("\n---------------------");
            #endregion Configuration

            #region JsonSerializerService
            #region Serializer

            Console.WriteLine("\nJsonSerializerService Test");
            Console.WriteLine("******************");
            GeneralSettingsModel GeneralSettings = new GeneralSettingsModel();
            GeneralSettings.LogEnabled = true;
            GeneralSettings.ActualEnvironment = "Development";


            DataLayerModel DataLayer = new DataLayerModel();
            DataLayer.DbName = "testdb";
            DataLayer.ServerName = "127.0.0.1";
            DataLayer.Password = "123456789/a";
            DataLayer.Login = "LoginTest";
            DataLayer.ConnectionString = "Data Source=127.0.0.1;Initial Catalog=testdb;User ID=LoginTest;Password=123456789/a;Trust Server Certificate=True";

            EnvironmentModel Development = new EnvironmentModel();
            Development.EnvironmentName = "Development";
            Development.DataLayer = DataLayer;

            UserModel User = new UserModel();
            User.IdUser = 0;
            User.Name = "Pablo";
            User.Password = "123456789/a";

            ConfigModel configSerializerTest = new ConfigModel();
            configSerializerTest.UserData = User;
            configSerializerTest.GeneralSettings = GeneralSettings;


            await JsonSerializerService.SerializeIntoJson<ConfigModel>(_testJsonFilePath001, configSerializerTest);

            string[] json = File.ReadAllLines(_testJsonFilePath001);

            foreach(string line in json)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("\n---------------------");


            #endregion Serializer

            #region Deserializer

            Console.WriteLine("\nJsonSerializerService Test");
            Console.WriteLine("******************");

            ConfigModel configDeserializerTest = await JsonSerializerService.DeserializeFromJson<ConfigModel>(_testJsonFilePath001);
            
            Console.WriteLine(configDeserializerTest.GeneralSettings.ActualEnvironment);
            Console.WriteLine("\n---------------------");
            
            #endregion Deserializer

            #endregion JsonSerializerService
        }
    }
}