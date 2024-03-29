﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using R5T.Magyar.IO;

using R5T.D0088;
using R5T.D0090;
using R5T.D0096;
using R5T.D0098;
using R5T.D0099;
using R5T.D0099.D002.I001;
using R5T.D0099.D003;
using R5T.D0103.I001;
using R5T.L0017.X001;
using R5T.T0091.T001;


namespace R5T.S0023
{
    class Program : ProgramAsAServiceBase
    {
        #region Static

        static async Task Main()
        {
            //OverridableProcessStartTimeProvider.Override("20211203-231500");

            await Instances.Host.NewBuilder()
                .UseProgramAsAService<Program, T0075.IHostBuilder>()
                .UseHostStartup<HostStartup, T0075.IHostBuilder>(Instances.ServiceAction.AddStartupAction())
                .Build()
                .SerializeConfigurationAudit()
                .SerializeServiceCollectionAudit()
                .RunAsync();
        }

        #endregion


        private ILogger Logger { get; }

        private IHumanOutput HumanOutput { get; }
        private IMachineOutput MachineOutput { get; }
        private Yabbo.TestService TestService { get; }


        public Program(IServiceProvider serviceProvider, ILogger<Program> logger,
            IHumanOutput humanOutput,
            IMachineOutput machineOutput,
            Yabbo.TestService testService)
            : base(serviceProvider)
        {
            this.Logger = logger;

            this.HumanOutput = humanOutput;
            this.MachineOutput = machineOutput;
            this.TestService = testService;
        }

        protected override async Task ServiceMain(CancellationToken stoppingToken)
        {
            //await this.RunMethod();
            await this.RunOperation();
        }

        private async Task RunOperation()
        {
            await this.ServiceProvider.Run<O000_Main>();

            //await this.ServiceProvider.Run<O900_OpenAllProjectRepositoryFiles>();

            //await this.ServiceProvider.Run<O100_UpdateProjectRepositoryWithCurrentProjects>();

            //await this.ServiceProvider.Run<O008_UpdateRepositoryWithSelectedProjects>();
            //await this.ServiceProvider.Run<O007_UpdateRepositoryWithAllProjects>();
            //await this.ServiceProvider.Run<O005_UpdateProjectIntellisense>();
            //await this.ServiceProvider.Run<O004_BackupFileBasedProjectRepositoryFiles>();
            //await this.ServiceProvider.Run<O003_PerformRequiredHumanActions>();
            //await this.ServiceProvider.Run<O002_UpdateFileBasedProjectRepository>();
            //await this.ServiceProvider.Run<O001_AnalyzeAllCurrentProjects>();
        }

        #region Extra

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private async Task RunMethod()
        {
            //await this.TestMachineMessageDeserialization();
            await this.TestOutput();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private async Task TestMachineMessageDeserialization()
        {
            var filePathProvider = this.ServiceProvider.GetRequiredService<IMachineOutputFilePathProvider>();

            var filePath = await filePathProvider.GetMachineOutputFilePath();

            var streamReader = StreamReaderHelper.New(filePath);
            var jsonTextReader = new JsonTextReader(streamReader);

            var jArray = await JArray.LoadAsync(jsonTextReader, CancellationToken.None);

            var machineMessageJsonReserializer = this.ServiceProvider.GetRequiredService<IMachineMessageJsonReserializer>();

            foreach (var jObject in jArray.Children<JObject>())
            {
                var machineMessage = machineMessageJsonReserializer.Deserialize(jObject);

                if(machineMessage.Success && machineMessage.Result is SimpleTextMachineMessage simpleTextMachineMessage)
                {
                    Instances.Console.WriteLine(simpleTextMachineMessage.Text);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
        private Task TestOutput()
        {
            // Test exclusive use console.
            //Console.WriteLine("Hello world!");
            Instances.Console.WriteLine("Hello world!");

            // Test logger.
            this.Logger.TestLogLevelOutput();

            using (var exclusiveUsageContext = Instances.Console.GetExclusiveUsageContext())
            {
                this.Logger.TestLogLevelEnabled(Console.Out);
            }

            this.TestService.Run();

            // Test human output.
            this.HumanOutput.WriteLine("Hello again world!");

            // Test machine output.
            var message = new SimpleTextMachineMessage
            {
                Text = "Hello machine world!",
            };

            this.MachineOutput.Write(message);

            var messages = InMemoryMachineMessageOutputSinkProvider.Messages;

            return Task.CompletedTask;
        }

        #endregion
    }
}
