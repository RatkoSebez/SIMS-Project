using HospitalApplication.Model;
using System;

namespace Model
{
    public class GeneralNews : News
   {
        private string typeNews;
        private string id;
        private string title;
        private string description;
        private DateTime publicationDate;
        private string durationNews;

      public GeneralNews() { }
      public GeneralNews(string argId, string argTypeNews, string argTitle, string argDescription, DateTime argPublicationDate, string argDurationNews)
      {
            id = argId;
            title = argTitle;
            description = argDescription;
            typeNews = argTypeNews;
            publicationDate = argPublicationDate;
            durationNews = argDurationNews;
      }

        public override string TypeNews
        {
            get { return typeNews; }
            set { typeNews = value; }
        }

        public override string Id 
        {
            get { return id; }
            set { id = value; }
        }

        public override string Title
        {
            get { return title; }
            set { title = value; }
        }

        public override string Description 
        {
            get { return description; }
            set { description = value; }
        }

        public override DateTime PublicationDate
        {
            get { return publicationDate; }
            set { publicationDate = value; }
        }

        public override string DurationNews 
        {
            get { return durationNews; }
            set { durationNews = value; }
        }

        /*
        public string DurationNews
        {
            get { return durationNews; }
            set { durationNews = value; }
        }

        public DateTime PublicationDate
        {
            get { return publicationDate; }
            set { publicationDate = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string TypeNews
        {
            get { return typeNews; }
            set { typeNews = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        */
    }
}