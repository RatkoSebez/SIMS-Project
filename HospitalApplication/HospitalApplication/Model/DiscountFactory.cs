using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Model
{
    class DiscountFactory : NewsFactory
    {
        private string _typeNews;
        private string _id;
        private string _title;
        private string _description;
        private DateTime _publicationDate;
        private string _durationNews;

        public DiscountFactory(string argId, string argTypeNews, string argTitle, string argDescription, DateTime argPublicationDate, string argDurationNews)
        {
            _id = argId;
            _title = argTitle;
            _description = argDescription;
            _typeNews = argTypeNews;
            _publicationDate = argPublicationDate;
            _durationNews = argDurationNews;
        }

        public override News GetNews()
        {
            return new DiscountNews(_id, _typeNews, _title, _description, _publicationDate, _durationNews);
        }
    }
}
