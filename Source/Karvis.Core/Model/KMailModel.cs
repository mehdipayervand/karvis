﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Fardis;
using NHibernate.Linq;
using System.Net.Mail;
using System.Net;

namespace Karvis.Core
{
    public class KMailModel : IKMailModel
    {
        ISessionFactory _sessionFactory;
        NHibernateRepository<KMail> _mailRepository;
        IScheduleInfoModel _scheduleInfoModel;
        IKDispatcher _kdispatcher;

        public KMailModel()
        {
            _sessionFactory = NHHelper.Instance.GetSessionFactory();
            _mailRepository = new NHibernateRepository<KMail>(_sessionFactory);
            _scheduleInfoModel = new ScheduleInfoModel();
            _kdispatcher = new KDispatcher();
        }

        public KMailModel(ISessionFactory sessionFactory, IScheduleInfoModel scheduleInfoModel, IKDispatcher kdispatcher)
        {
            _sessionFactory = sessionFactory;
            _scheduleInfoModel = scheduleInfoModel;
            _mailRepository = new NHibernateRepository<KMail>(_sessionFactory);
            _kdispatcher = kdispatcher;
        }


        public void DoSchedule()
        {
            DateTime start = DateTime.Now;

            //do the real work
            string result = string.Empty;
            //todo

            DateTime end = DateTime.Now;
            _scheduleInfoModel.SaveScheduleInfo(KConstants.MailSchedule, result, start, end);
        }


        public void Qeue(string subject, string description, string fromAddress, string fromDescription, int relatedReferenceId)
        {
            KMail mail = new KMail
            {
                Subject = subject,
                Description = description,
                FromAddress = fromAddress,
                FromDescription = fromDescription,
                RelatedReferenceId = relatedReferenceId,
                AddDate = DateTime.Now,
                IsSent = false,
                TryCounter = 0
            };

            _mailRepository.SaveOrUpdate(mail);
        }

        public IList<KMail> GetSentItems()
        {
            return _mailRepository.QueryOver().Where(item => item.IsSent).List();
        }

        public IList<KMail> GetUnSentItems()
        {
            return _mailRepository.QueryOver().Where(item => !item.IsSent).List();
        }

        public void IncreaseTry(KMail mail)
        {
            var item = _mailRepository.Load(mail.Id);
            item.TryCounter++;
            _mailRepository.SaveOrUpdate(item);
        }

        public void SaveSuccess(KMail mail)
        {
            var item = _mailRepository.Load(mail.Id);
            item.IsSent = true;
            _mailRepository.SaveOrUpdate(item);
        }
    }
}
