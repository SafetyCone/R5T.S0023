using System;
using System.Threading.Tasks;

using R5T.D0084.D001;
using R5T.D0101;
using R5T.D0101.I001;


namespace R5T.S0023
{
    public class O003_PerformRequiredHumanActions : T0020.IOperation
    {
        private IAllProjectFilePathsProvider AllProjectFilePathsProvider { get; }
        private IProjectRepository ProjectRepository { get; }

        private O003a_DetermineIfHumanActionsAreRequired O003A_DetermineIfHumanActionsAreRequired { get; }
        private O003b_PromptForRequiredHumanActionsCore O003B_PromptForRequiredHumanActionsCore { get; }


        public O003_PerformRequiredHumanActions(
            IAllProjectFilePathsProvider allProjectFilePathsProvider,
            IProjectRepository projectRepository,
            O003a_DetermineIfHumanActionsAreRequired o003A_DetermineIfHumanActionsAreRequired,
            O003b_PromptForRequiredHumanActionsCore o003B_PromptForRequiredHumanActionsCore)
        {
            this.AllProjectFilePathsProvider = allProjectFilePathsProvider;
            this.ProjectRepository = projectRepository;

            this.O003A_DetermineIfHumanActionsAreRequired = o003A_DetermineIfHumanActionsAreRequired;
            this.O003B_PromptForRequiredHumanActionsCore = o003B_PromptForRequiredHumanActionsCore;
        }

        public async Task Run()
        {
            var currentProjects = await Instances.Operation.GetCurrentProjects(this.AllProjectFilePathsProvider);
            var repositoryProjects = await this.ProjectRepository.GetAllProjects();

            var repositoryDuplicateProjectNames = await this.ProjectRepository.GetDuplicateProjectNames();
            var repositoryIgnoredProjectNames = await this.ProjectRepository.GetAllIgnoredProjectNames();

            var humanActionsAreRequired = await this.O003A_DetermineIfHumanActionsAreRequired.Run(
                currentProjects,
                repositoryProjects,
                repositoryDuplicateProjectNames,
                repositoryIgnoredProjectNames);

            var anyHumanActionsAreRequired = humanActionsAreRequired.Any();
            if (anyHumanActionsAreRequired)
            {
                Console.WriteLine("Human actions are required before updating the project repository.\n");

                await this.O003B_PromptForRequiredHumanActionsCore.Run(humanActionsAreRequired);
            }
            else
            {
                Console.WriteLine("No human actions are required before updating the project repository.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
