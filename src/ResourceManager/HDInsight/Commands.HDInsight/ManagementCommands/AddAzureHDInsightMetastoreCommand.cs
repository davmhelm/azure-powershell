﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Management.Automation;
using Microsoft.Azure.Commands.HDInsight.Commands;
using Microsoft.Azure.Commands.HDInsight.Models;

namespace Microsoft.Azure.Commands.HDInsight
{
    [Cmdlet(
        VerbsCommon.Add,
        Constants.CommandNames.AzureHDInsightMetastore),
    OutputType(
        typeof(AzureHDInsightConfig))]
    public class AddAzureHDInsightMetastoreCommand : HDInsightCmdletBase
    {
        private AzureHDInsightMetastore _metastore;

        #region Input Parameter Definitions

        [Parameter(Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            HelpMessage = "The HDInsight cluster configuration to use when creating the new cluster")]
        public AzureHDInsightConfig Config { get; set; }
        
        [Parameter(Position = 1,
            Mandatory = true,
            HelpMessage = "The type of metastore.")]
        public AzureHDInsightMetastoreType MetastoreType { get; set; }

        [Parameter(Position = 2, 
            Mandatory = true,
            HelpMessage = "The Azure SQL Server instance to use for this metastore.")]
        public string SqlAzureServerName
        {
            get { return this._metastore.SqlAzureServerName; }
            set { this._metastore.SqlAzureServerName = value; }
        }
        
        [Parameter(Position = 3,
            Mandatory = true, 
            HelpMessage = "The database on the Azure SQL Server instance to use for this metastore.")]
        public string DatabaseName
        {
            get { return this._metastore.DatabaseName; }
            set { this._metastore.DatabaseName = value; }
        }
        
        [Parameter(Position = 4,
            Mandatory = true, 
            HelpMessage = "The user credentials to use for the Azure SQL Server database.")]
        public PSCredential Credential
        {
            get { return this._metastore.Credential; }
            set { this._metastore.Credential = value; }
        }

        #endregion

        public AddAzureHDInsightMetastoreCommand()
        {
            _metastore = new AzureHDInsightMetastore();
        }
        
        public override void ExecuteCmdlet()
        {
            switch (this.MetastoreType)
            {
                case AzureHDInsightMetastoreType.HiveMetastore:
                    this.Config.HiveMetastore = this._metastore;
                    break;
                case AzureHDInsightMetastoreType.OozieMetastore:
                    this.Config.OozieMetastore = this._metastore;
                    break;
            }

            WriteObject(this.Config);
        }
    }
}
