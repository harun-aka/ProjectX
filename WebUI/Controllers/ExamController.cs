using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ExamController : Controller
    {
        IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        public IActionResult Index(string? errorMessage)
        {
            if (!AuthHelper.IsAuthenticated())
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.Error = errorMessage;

            var result = _examService.GetAll();
            List<ExamListDto> list = new List<ExamListDto>();
            if (result.Success)
            {
                list = result.Data;
            }

            return View(list);

        }

        [HttpPost]
        public IActionResult GetExamCreateView(string title)
        {
            var result = GetListRss();
            if(!result.Success)
            {
                return null;
            }
            RssModel rss = result.Data.Where(x => x.Title == title).FirstOrDefault();
            ViewBag.Rss = rss;
            ViewBag.RssContent = GetRssContent(rss);
            return PartialView("_PartialExam", new ExamDto());
        }


        public IActionResult Create()
        {
            if (!AuthHelper.IsAuthenticated())
            {
                return RedirectToAction("Login", "Auth");
            }
            var result = GetListRss();
            if(!result.Success)
            {
                ViewBag.Error = "Failed" + result.Message;
                return View();
            }
            ViewBag.RSSFeed = result.Data;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ExamDto exam)
        {
            if (exam == null)
            {
                ViewBag.Error = "Create Exam Failed.";
                return View("Create");
            }

            var result = _examService.SaveExam(exam);
            if (!result.Success)
            {
                ViewBag.Error = "Create Exam Failed. " + result.Message;
                return View("Create");
            }

            return RedirectToAction("Index", "Exam");
        }

        public IDataResult<List<RssModel>> GetListRss()
        {
            List<RssModel> rssList = new List<RssModel>();

            string url = "https://www.wired.com/feed/rss";

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);

            XmlNodeList itemNodes = xmlDoc.SelectNodes("//item");
            var list = itemNodes.Cast<XmlNode>().Take(5);
            foreach (XmlNode itemNode in list)
            {
                RssModel rss = new RssModel();
                XmlNode titleNode = itemNode.SelectSingleNode("title");
                XmlNode descriptionNode = itemNode.SelectSingleNode("description");
                XmlNode urlNode = itemNode.SelectSingleNode("link");
                rss.Title = titleNode.InnerText;
                rss.Description = descriptionNode.InnerText;
                rss.URL = urlNode.InnerText;
                rssList.Add(rss);
            }
            return new SuccessDataResult<List<RssModel>> (rssList);
        }

        public IDataResult<RssModel> GetRssContent(RssModel rssModel)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(rssModel.URL);

            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//p").ToArray();
            for (var i = 0; i < nodes.Count<HtmlNode>(); i++)
            {
                HtmlNode htmlNote = nodes[i];
                if (i < nodes.Count<HtmlNode>() - 5)
                {
                    rssModel.Description += "<br>" + htmlNote.InnerHtml;
                }
            }
            return new SuccessDataResult<RssModel>(rssModel); ;
        }

        [HttpGet]
        public IActionResult Delete(int examId)
        {
            var result = _examService.Delete(examId);
            if (!result.Success)
            {
                return RedirectToAction("Index", "Exam", new { errorMessage = result.Message });
            }
            return RedirectToAction("Index", "Exam");
        }

        [HttpGet]
        public IActionResult StartExam(int examId)
        {
            if (!AuthHelper.IsAuthenticated())
            {
                return RedirectToAction("Login", "Auth");
            }

            var result = _examService.Get(examId);
            if(!result.Success)
            {
                return RedirectToAction("Index", "Exam");
            }

            ExamDto exam = result.Data;
            foreach (var item in exam.Questions)
            {
                foreach (var answer in item.Answers)
                {
                    answer.IsRight = null;
                }
            }

            ViewBag.RssTitle = exam.Article.Title;
            ViewBag.RssContent = exam.Article.Text;
            return View("StartExam", exam);
        }

        [HttpPost]
        public JsonResult CheckAnswer(int answerId, bool IsRight)
        {
            var result = _examService.GetAnswer(answerId);
            if(!result.Success)
            {
                return new JsonResult(new { data = "null" });
            }
            Answer answer = result.Data;
            if (answer.IsRight == IsRight)
            {
                return new JsonResult(new { data = "true" });
            }
            return new JsonResult(new { data = "false" });
        }

    }
}
