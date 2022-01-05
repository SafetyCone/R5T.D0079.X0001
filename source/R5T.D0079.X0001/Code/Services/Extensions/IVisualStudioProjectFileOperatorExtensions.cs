using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.Lombardy;

using R5T.D0079;

using Instances = R5T.D0079.X0001.Instances;


namespace System
{
    public static class IVisualStudioProjectFileOperatorExtensions
    {
        public static Task Create(this IVisualStudioProjectFileOperator visualStudioProjectFileOperator,
            string projectType,
            string projectFilePath)
        {
            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

            var projectName = Instances.ProjectPathsOperator.GetProjectName(projectFilePath);

            return visualStudioProjectFileOperator.Create(projectType, projectName, projectDirectoryPath);
        }

        public static async Task<string[]> ListProjectReferencePaths(this IVisualStudioProjectFileOperator visualStudioProjectFileOperator,
            string projectFilePath,
            IStringlyTypedPathOperator stringlyTypedPathOperator)
        {
            var projectDirectoryPath = stringlyTypedPathOperator.GetDirectoryPathForFilePath(projectFilePath);

            var projectReferenceRelativePaths = await visualStudioProjectFileOperator.ListProjectReferenceRelativeFilePaths(projectFilePath);

            var output = projectReferenceRelativePaths
                .Select(xRelativeFilePath => stringlyTypedPathOperator.Combine(projectDirectoryPath, xRelativeFilePath))
                .ToArray();

            return output;
        }
    }
}
