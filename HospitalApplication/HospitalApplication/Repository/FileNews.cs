using HospitalApplication.Model;
using HospitalApplication.Repository;
using Model;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WorkWithFiles
{
    class FileNews : IFile
   {
        private static string path = "../../../Data/news.json";
        private static List<News> news;

        private static FileNews instance;

        public static FileNews Instance
        {
            get
            {
                if (null == instance)
                    instance = new FileNews();
                return instance;
            }
        }

        private FileNews()
        {
            Read();
        }

        public void Read()
        {
            string json = File.ReadAllText(path);
            news = new JavaScriptSerializer().Deserialize<List<News>>(json);
        }

        public void Write()
        {
            string json = new JavaScriptSerializer().Serialize(news);
            File.WriteAllText(path, json);
        }

        public List<News> GetAllNews()
        {
            return news;
        }
    }
}