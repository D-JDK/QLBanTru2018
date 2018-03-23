﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnect.DAO.HungTD
{
    public class LessonDAO
    {
        QLHSSmartKidsDataContext db;
        Table<Lesson> lessons;
        public LessonDAO()
        {
            db = new QLHSSmartKidsDataContext();
            lessons = db.GetTable<Lesson>();
        }
        public List<Lesson> ListAll()
        {
            return lessons.Where(x => x.Status.Equals(true)).ToList();
        }
        public List<Lesson> FilterByTopicType(int topicTypeID)
        {
            return lessons.Where(x => x.Status.Equals(true) && x.Topic.TopicTypeID.Equals(topicTypeID)).ToList();
        }
        public List<Lesson> FilterByTopic(int topicID)
        {
            return lessons.Where(x => x.Status.Equals(true) && x.TopicID.Equals(topicID)).ToList();
        }
        public List<Lesson> ListDeleted()
        {
            return lessons.Where(x => x.Status.Equals(false)).ToList();
        }
        public int Insert(Lesson entity)
        {
            try
            {
                lessons.InsertOnSubmit(entity);
                db.SubmitChanges();
                return entity.LessonID;
            }
            catch
            {
                return 0;
            }
        }
        public bool Edit(Lesson entity)
        {
            try
            {
                Lesson obj = lessons.Single(x => x.LessonID == entity.LessonID);
                obj.Name = entity.Name;
                obj.TopicID = entity.TopicID;
                obj.Status = entity.Status;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int lessonID)
        {
            try
            {
                Lesson obj = lessons.Single(x => x.LessonID == lessonID);
                obj.Status = false;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteByTopic(int topicID)
        {
            try
            {
                var listDeleteLesson = lessons.Where(x => x.TopicID.Equals(topicID));
                foreach(var item in listDeleteLesson)
                {
                    Delete(item.LessonID);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Restore(int lessonID)
        {
            try
            {
                Lesson obj = lessons.Single(x => x.LessonID == lessonID && x.Status == false);
                obj.Status = true;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RestoreByTopic(int topicID)
        {
            try
            {
                var listRestoreLesson = lessons.Where(x => x.TopicID.Equals(topicID) && x.Status == false);
                foreach(var item in listRestoreLesson)
                {
                    Restore(item.LessonID);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
