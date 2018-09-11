using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace SAM_COMPANION2.Importers
{
    public abstract class ImporterBase
    {
        protected Configuration CsvConfig;

        protected ImporterBase()
        {
            CsvConfig = new Configuration
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };
        }

        public abstract void Import(string filename);

        public Task ImportAsync(string filename)
        {
            return Task.Run(() => Import(filename));
        }
    }
}
