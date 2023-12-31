﻿namespace MyMongoDbAjaxProject.DAL.Settings
{
    public interface IDataBaseSettings
    {
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string EmployeeCollectionName { get; set; }
    }
}
