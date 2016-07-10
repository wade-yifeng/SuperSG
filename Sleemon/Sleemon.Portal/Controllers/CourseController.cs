﻿using System.Collections.Generic;
using Sleemon.Common;
using Sleemon.Core;
using Sleemon.Portal.Common;

namespace Sleemon.Portal.Controllers
{
    using System;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Sleemon.Data;
    using Sleemon.Portal.Core;

    public class CourseController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int pageIndex, int pageSize, string courseTitle)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.CurrentUserId = User.Identity.AsClaimsIdentity().GetUserUniqueId();

            var courseList = ServiceClient.Request<ILearningFileService, IList<LearningCourseListModel>>(
                service => service.GetCourseList(pageIndex, pageSize, courseTitle));

            return PartialView("CourseList", courseList);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var course =
                ServiceClient.Request<ILearningFileService, LearningCourseDetailModel>(
                    service => service.GetCourseDetailById(id));

            return PartialView("CourseDetail", course);
        }

        [HttpGet]
        public ActionResult CourseCreate(int id = 0)
        {
            var course = id == 0
                ? new LearningCourseDetailModel()
                : ServiceClient.Request<ILearningFileService, LearningCourseDetailModel>(
                    service => service.GetCourseDetailById(id));

            return PartialView("CourseCreate", course);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateCourse(LearningCourseDetailModel course)
        {
            var msg = "Save successful.";
            var validation = ValidateModelForCourse(course);
            if (string.IsNullOrEmpty(validation))
            {
                EnrichCourseDetailModel(course);
                var result =
                    ServiceClient.Request<ILearningFileService, ResultBase>(service => service.SaveCourseDetail(course));
                if (!result.IsSuccess)
                {
                    msg = result.Message;
                }
            }
            else
            {
                msg = validation;
            }

            return new JsonResult() { Data = msg };
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteCourse(int id)
        {
            var msg = "Delete successful.";
            var course =
                ServiceClient.Request<ILearningFileService, LearningCourseDetailModel>(
                    service => service.GetCourseDetailById(id));
            if (course != null && course.LastUpdateUser == UserUniqueId)
            {
                var result =
                    ServiceClient.Request<ILearningFileService, ResultBase>(
                        service => service.DeleteCourseById(id));
                if (!result.IsSuccess)
                {
                    msg = result.Message;
                }
            }
            else
            {
                msg = @"Course info is not found.";
            }

            return new JsonResult() { Data = msg };
        }
        
        private void EnrichCourseDetailModel(LearningCourseDetailModel course)
        {
            course.LastUpdateUser = UserUniqueId;

            if (course.Status == (byte)ActionCategory.Publish)
            {
                for (int i = 0; i < course.Chapters.Count; i++)
                {
                    var chapter = course.Chapters[i];
                    chapter.No = i + 1;
                    chapter.LastUpdateUser = UserUniqueId;

                    for (int j = 0; j < chapter.Files.Count; j++)
                    {
                        var file = chapter.Files[j];
                        file.No = j + 1;
                        file.LastUpdateUser = UserUniqueId;
                    }
                }
            }
        }

        private string ValidateModelForCourse(LearningCourseDetailModel course)
        {
            return string.Empty;
        }
    }
}