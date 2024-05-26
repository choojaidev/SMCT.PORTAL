using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using SMCTPortal.Model.SMCV;

//using SMCTPortal.DataAccess.DatabaseContext;
using SMCTPortal.Model.SMPeople;
using SMCTPortal.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SMCTPortal.DataAccess
{
    public class database
    {
        private readonly IMongoClient _mongoClient;
        public database(IMongoClient mongoClient)
        {
                _mongoClient = mongoClient;
        }
        public tbPeople SaveData(tbPeople people) {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            var collection = database.GetCollection<tbPeople>("Citizens");
          //  _citizen = database.GetCollection<BsonDocument>("Citizens");

            collection.InsertOne(people);
            return people;
        }
        public async Task<tbPeople> SaveRelotData(tbPeople data)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            var collection = database.GetCollection<tbPeople>("Citizens");
            var filter = Builders<tbPeople>.Filter.Eq("citizenNo", data.citizenNo);
            var update = Builders<tbPeople>.Update
            //.Set("Name", people.Name)
            //.Set("SureName", people.SureName)
            //.Set("DateOfBirth", people.DateOfBirth)
            //.Set("PhoneNo", people.PhoneNo)
            //.Set("Email", people.Email)
            //.Set("Relations", people.Relations);
            .Set("relocationInfos", data.relocationInfos);


            var result = await collection.UpdateOneAsync(filter, update);
            return data;
        }
        public async Task<tbPeople> SaveEduData(tbPeople data)
		{
			var client = new MongoClient("mongodb://localhost:27017");
			var database = client.GetDatabase("SMZT");
			var collection = database.GetCollection<tbPeople>("Citizens");
            var filter = Builders<tbPeople>.Filter.Eq("citizenNo", data.citizenNo );
            var update = Builders<tbPeople>.Update
                //.Set("Name", people.Name)
                //.Set("SureName", people.SureName)
                //.Set("DateOfBirth", people.DateOfBirth)
                //.Set("PhoneNo", people.PhoneNo)
                //.Set("Email", people.Email)
                //.Set("Relations", people.Relations);
            .Set("educationInfos", data.educationInfos)
            .Set("resumeInfos.educationInfos", data.educationInfos);


            var result = await collection.UpdateOneAsync(filter, update);
            return data;
		}

        public async Task<tbPeople> SaveCVData(tbPeople data)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            var collection = database.GetCollection<Resume>("Citizens");
            var filter = Builders<Resume>.Filter.Eq("citizenNo", data.citizenNo);
            var update = Builders<Resume>.Update
           
          
            .Set("resumeInfos", data.resumeInfos);


            var result = await collection.UpdateOneAsync(filter, update);
            return data;
        }
        public async Task<Resume> SaveCVJobHistData(Resume  data)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            var collection = database.GetCollection<Resume>("Citizens");
            var filter = Builders<Resume>.Filter.Eq("citizenNo", data.citizenNo);
            var update = Builders<Resume>.Update


            .Set("resumeInfos.JobHistoryInfos", data.JobHistoryInfos);


            var result = await collection.UpdateOneAsync(filter, update);
            return data;
        }
        public async Task< tbPeople> UpdateData(tbPeople people)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            
            var collection = database.GetCollection<tbPeople>("Citizens");
            var filter = Builders<tbPeople>.Filter.Eq("citizenNo", people.citizenNo);
            var update = Builders<tbPeople>.Update
                .Set("Name", people.Name)
                .Set("SureName", people.SureName)
                .Set("DateOfBirth", people.DateOfBirth)
                .Set("PhoneNo", people.PhoneNo)
                .Set("Email", people.Email)
                .Set("Relations", people.Relations);
                //.Set("family", people .family);
             

            var result = await collection.UpdateOneAsync(filter, update);

            return people;
        }
        public async Task<tbPeople> UpdateFamilyData(tbPeople people)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");

            var collection = database.GetCollection<tbPeople>("Citizens");
            var filter = Builders<tbPeople>.Filter.Eq("citizenNo", people.citizenNo);
            var update = Builders<tbPeople>.Update
                //.Set("Name", people.Name)
                //.Set("SureName", people.SureName)
                //.Set("DateOfBirth", people.DateOfBirth)
                //.Set("PhoneNo", people.PhoneNo)
                //.Set("Email", people.Email)
                //.Set("Relations", people.Relations)
                .Set("family", people.family);


            var result = await collection.UpdateOneAsync(filter, update);

            return people;
        }
        public async Task<tbPeople> UpdateEducateData(tbPeople  data)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");

            var collection = database.GetCollection<tbPeople>("Citizens");
            var filter = Builders<tbPeople>.Filter.Eq("citizenNo", data.educationInfos);
            var update = Builders<tbPeople>.Update
                //.Set("Name", people.Name)
                //.Set("SureName", people.SureName)
                //.Set("DateOfBirth", people.DateOfBirth)
                //.Set("PhoneNo", people.PhoneNo)
                //.Set("Email", people.Email)
                //.Set("Relations", people.Relations)
                .Set("educates", data.educationInfos );


            var result = await collection.UpdateOneAsync(filter, update);

            return data;
        }
        public JArray GetData(string citizenId)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            var collection = database.GetCollection<BsonDocument>("Citizens");
            var citizens = collection.Find(filter).ToList();
            // Convert MongoDB documents to dynamic objects
            var dynamicCitizen = new JArray();
            foreach (var ct in citizens)
            {
                dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
            }
            return dynamicCitizen;
        }
    }
}
