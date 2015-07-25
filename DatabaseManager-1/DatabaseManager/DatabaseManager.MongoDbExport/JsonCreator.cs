namespace DatabaseManager.MongoDbExport
{
    using DatabaseManager.SalesReports;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Bson.IO;
    using MongoDB.Bson.Serialization;

    public class JsonCreator
    {
        private const string FilePath = @"\\psf\Dropbox\Personal\SoftUni\Level 3\DB Apps\team project\DBAppsTeamProject\DatabaseManager-1\DatabaseManager\DatabaseManager.MongoDbExport\JsonReports\";

        public int WriteJsonFiles(List<MongoDB.Document> documents)
        {
            int filesCount = 0;

            foreach (var document in documents)
            {
                string fileName = FilePath + document.ToDictionary()["product-id"] + ".json";
                using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonWriter jw = new JsonWriter(sw))
                {
                    BsonSerializer.Serialize(jw, document);
                    filesCount++;
                }
            }
            return filesCount;
        }
    }
}
