﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;

namespace SJ.Core
{
    public class FeedHelper
    {
        public static SyndicationFeedFormatter All(string format)
        {
            SyndicationFeed feed = new SyndicationFeed("کارویس - همه", "نمایش فهرست همه کارهای ثبت شده در سیستم", new Uri("http://afsharm.com/"));
            feed.Authors.Add(new SyndicationPerson(GeneralHelper.GetAppEmail()));
            feed.Categories.Add(new SyndicationCategory("همه"));
            feed.Description = new TextSyndicationContent("نمایش فهرست همه کارهای ثبت شده در سیستم");

            List<SyndicationItem> items = new List<SyndicationItem>();

            foreach (Job job in JobDao.GetAllJobs())
            {
                SyndicationItem item = new SyndicationItem(
                    job.Title,
                    job.Description,
                    new Uri(job.GetJobUrl()),
                    job.ID.ToString(),
                    job.DateAdded.Value);

                items.Add(item);
            }

            feed.Items = items;

            switch (format)
            {
                case "rss":
                    return new Rss20FeedFormatter(feed);
                case "atom":
                    return new Atom10FeedFormatter(feed);
                default:
                    throw new ApplicationException("Unkown feed format: " + format);
            }
        }
    }
}
