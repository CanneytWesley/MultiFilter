using Filter.Filter_Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiFilter.Data
{
    public class FilterDataService
    {
        private readonly DataLocation dataLocation;

        public FilterDataService(DataLocation dataLocation)
        {
            this.dataLocation = dataLocation;
        }

        public void Save(List<IResult> filters)
        {
            List<DataModel> Models = new List<DataModel>();

            foreach (var filter in filters)
            {
                Models.Add(new DataModel(filter.Filter.ShortCut,filter.Filter.Title,filter.Model.Item));
            }

            FileService.WriteToXmlFile(dataLocation.GetFilePath(), Models, false);
        }

        public List<DataModel> Read()
        {
            if (dataLocation.NotValid()) return new List<DataModel>();

            return FileService.ReadFromXmlFile<List<DataModel>>(dataLocation.GetFilePath());
        }
    }
}
