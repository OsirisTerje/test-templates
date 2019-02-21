// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.TestTemplates.AcceptanceTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.IO;

    [TestClass]
    public class DotnetCoreTemplateTests : AcceptanceTestBase
    {
        /// <summary>
        /// The net core versions for which templates are present
        /// </summary>
        private static string[] netCoreVersions = { "1.x", "2.0", "2.1", "2.2", "3.0" };

        /// <summary>
        /// The type of the test template, combination of the test framework and language
        /// </summary>
        private static string[] templateTypes = { "MSTest-CSharp", "MSTest-FSharp", "MSTest-VisualBasic", "XUnit-CSharp", "XUnit-FSharp", "XUnit-VisualBasic" };

        [DataTestMethod]
        [DynamicData(nameof(GetTestTemplatesPath), DynamicDataSourceType.Method)]
        public void TemplateTest(string path)
        {
            // Invokes dotnet test <path>
            InvokeDotnetTest(path);

            // Verfiy the tests run as expected.
            ValidateSummaryStatus(1, 0, 0);
        }

        /// <summary>
        /// Dynamic data source for the template test 
        /// </summary>
        /// <returns>Paths to all possible the template projects</returns>
        private static IEnumerable<object[]> GetTestTemplatesPath()
        {
            var list = new List<string[]>();

            foreach (var netcoreVersion in netCoreVersions)
            {
                foreach (var templateType in templateTypes)
                {
                    list.Add(new string[] { Path.Combine("template_feed", "Microsoft.DotNet.Test.ProjectTemplates." + netcoreVersion, "content", templateType) });
                }
            }

            return list;
        }
    }
}
