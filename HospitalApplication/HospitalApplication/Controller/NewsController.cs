
using HospitalApplication.Model;
using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using WorkWithFiles;

namespace HospitalApplication.Controller
{
     class NewsController
    {
        private NewsService newsService = NewsService.Instance;
        private FileNews fileNews = FileNews.Instance;

        public void CreateNews(News newNews)
        {
            newsService.CreateNews(newNews);
        }

        public void DeleteNews(string iDNews)
        {
            newsService.DeleteNews(iDNews);
        }

        public List<News> GetAllNews()
        {
            return fileNews.GetAllNews();
        }

        public void UpdateNews(News currentNews)
        {
            newsService.UpdateNews(currentNews);
        }
    }
}
