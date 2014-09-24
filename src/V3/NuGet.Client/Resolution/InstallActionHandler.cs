﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Client.Resolution
{
    public class InstallActionHandler : IActionHandler
    {
        public Task Execute(PackageAction action, ExecutionContext context)
        {
            // Get the package from the shared repository
            var package = context.PackageManager.LocalRepository.FindPackage(
                action.PackageName.Id, CoreConverters.SafeToSemVer(action.PackageName.Version));
            Debug.Assert(package != null); // The package had better be in the local repository!!

            // Add the package to the project
            context.ProjectManager.Execute(new PackageOperation(
                package,
                NuGet.PackageAction.Install));

            // Not async yet :)
            return Task.FromResult(0);
        }

        public Task Rollback(PackageAction action, ExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
