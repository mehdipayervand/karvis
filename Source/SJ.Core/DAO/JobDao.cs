﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SJ.Core
{
    public class JobDao
    {
        public static int AddNewJob(string title, string description, string url, string tag)
        {
            url = url.ToLower();

            if (!url.StartsWith("http://"))
                url = "http://" + url;

            Job job = new Job()
            {
                Title = title,
                Description = description,
                URL = url,
                Tag = tag,
                DateAdded = DateTime.UtcNow,
                VisitCount = 0
            };

            ISession session = NHHelper.Instance.GetSession();

            ITransaction tx = session.BeginTransaction();
            session.SaveOrUpdate(job);
            session.Flush();
            tx.Commit();

            return job.ID;
        }

        public IList<Job> FindAll(string title, string sortOrder, int maximumRows, int startRowIndex)
        {
            IQueryOver<Job, Job> q = CreateQuery(title);

            switch (sortOrder)
            {
                case "ID":
                    q = q.OrderBy(j => j.ID).Asc;
                    break;
                case "ID DESC":
                    q = q.OrderBy(j => j.ID).Desc;
                    break;
                case "VisitCount":
                    q = q.OrderBy(j => j.VisitCount).Asc;
                    break;
                case "VisitCount DESC":
                    q = q.OrderBy(j => j.VisitCount).Desc;
                    break;
                case "Title":
                    q = q.OrderBy(j => j.Title).Asc;
                    break;
                case "Title DESC":
                    q = q.OrderBy(j => j.Title).Desc;
                    break;
                case "DateAdded":
                    q = q.OrderBy(j => j.DateAdded).Asc;
                    break;
                case "DateAdded DESC":
                default:
                    q = q.OrderBy(j => j.DateAdded).Desc;
                    break;
            }

            IList<Job> retval = q
                .Skip(startRowIndex)
                .Take(maximumRows)
                .List<Job>();

            return retval;
        }

        private static IQueryOver<Job, Job> CreateQuery(string title)
        {
            ISession session = NHHelper.Instance.GetSession();

            var q = session.QueryOver<Job>();

            if (!string.IsNullOrEmpty(title))
                q = q.WhereRestrictionOn(j => j.Title).IsInsensitiveLike(title, MatchMode.Anywhere);

            return q;
        }

        public int FindAllCount(string title)
        {
            return CreateQuery(title).RowCount();
        }

        public static Job GetJob(string jobId)
        {
            ISession session = NHHelper.Instance.GetSession();

            return session.Get<Job>(Convert.ToInt32(jobId));
        }

        public static void UpdateJob(string title, string description, string URL, string tag, int ID)
        {
            ISession session = NHHelper.Instance.GetSession();

            Job job = session.Load<Job>(ID);

            job.Title = title;
            job.Description = description;
            job.URL = URL;
            job.Tag = tag;

            ITransaction tx = session.BeginTransaction();
            session.SaveOrUpdate(job);
            session.Flush();
            tx.Commit();
        }

        public static void IncreaseVisitCount(string ID)
        {
            ISession session = NHHelper.Instance.GetSession();

            Job job = session.Load<Job>(Convert.ToInt32(ID));

            job.VisitCount++;

            ITransaction tx = session.BeginTransaction();
            session.SaveOrUpdate(job);
            session.Flush();
            tx.Commit();
        }
    }
}