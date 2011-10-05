using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fardis;
using System.Text.RegularExpressions;

namespace Karvis.Core
{
    public class ExtractorHelper : IExtractorHelper
    {
        /// <summary>
        /// preloaded job urls that are saved in database previously. 
        /// Loading them in first step helps to increase performance
        /// </summary>
        IList<string> preUrls;
        IList<string> preIgnoredUrlJobs;

        IJobModel _jobModel;
        IIgnoredJobModel _ignoredJobModel;
        IKarvisCrawler _crawler;

        public ExtractorHelper(IJobModel jobModel, IIgnoredJobModel ignoredJobModel, IKarvisCrawler karvisCrawler)
        {
            _jobModel = jobModel;
            _ignoredJobModel = ignoredJobModel;
            _crawler = karvisCrawler;
        }

        public void GetReady(AdSource siteSource)
        {
            //TO PREVENT DUPLICATE URLS
            preUrls = _jobModel.GetJobUrlsByAdSource(siteSource);
            preIgnoredUrlJobs = _ignoredJobModel.GetIgnoredJobs(siteSource);
        }

        /// <summary>
        /// Gets urls that are used for given advertise source, a typical ad source may have more than one url.
        /// </summary>
        public string[] GetSiteSourceUrl(AdSource siteSource)
        {
            switch (siteSource)
            {
                case AdSource.rahnama_com:
                    return new string[] 
                        {
                            "http://www.rahnama.com/component/mtree/%DA%AF%D8%B1%D9%88%D9%87/35179/%D8%A8%D8%B1%D9%86%D8%A7%D9%85%D9%87-%D9%86%D9%88%D9%8A%D8%B3.html",//برنامه نویس
                            "http://www.rahnama.com/component/mtree/%DA%AF%D8%B1%D9%88%D9%87/35310/%D9%85%D9%87%D9%86%D8%AF%D8%B3-%D9%83%D8%A7%D9%85%D9%BE%D9%8A%D9%88%D8%AA%D8%B1.html" //مهندس کامپیوتر
                        };
                case AdSource.agahi_ir:
                    return new string[] 
                        { 
                            "http://www.agahi.ir/category/43"  //برنامه نویس
                        };
                case AdSource.nofa_ir:
                    return new string[]
                    {
                        "http://www.nofa.ir/JobSR-t12.aspx", //برنامه نویس سیستم
                        "http://www.nofa.ir/JobSR-t13.aspx", //برنامه نویس تحت وب
                        "http://www.nofa.ir/JobSR-t14.aspx", //طراح وب
                        "http://www.nofa.ir/JobSR-t18.aspx" //تکنسین کامپیوتر
                    };
                case AdSource.unp_ir:
                    return new string[]
                    {
                        "http://www.unp.ir/education_82.htm", //استخدام مهندس برق و کامپیوتر
                        "http://www.unp.ir/education_83.htm", //استخدام برنامه نویس
                        "http://www.unp.ir/education_119.htm" //کار پاره وقت - نیمه وقت
                    };
                case AdSource.itjobs_ir:
                    return new string[]
                    {
                        "http://itjobs.ir/Search.aspx?Keyword=&JobTitle=-1&State=-1&ContractType=-1&Mode=0"
                    };
                case AdSource.istgah_com:
                    return new string[]
                    {
                        "http://www.istgah.com/firekeys/key_19132/", //برنامه نویس PHP
                        "http://www.istgah.com/firekeys/key_4967/", //استخدام برنامه نویس
                        "http://www.istgah.com/firekeys/key_883/", //php
                        "http://www.istgah.com/firekeys/key_39667/", //استخدام کامپیوتر
                        "http://www.istgah.com/firekeys/key_264/" //برنامه نویس
                    };

                case AdSource.developercenter_ir:
                    return new string[]
                    {
                        "http://www.developercenter.ir/forum/external.php?type=RSS2&forumids=59"
                    };

                case AdSource.banki_ir:
                    return new string[]
                    {
                        "http://banki.ir/akhbar?format=feed&type=rss"
                    };

                case AdSource.estekhtam_com:
                    return new string[]
                    {
                        "http://www.estekhtam.com/?feed=rss2"
                    };

                case AdSource.barnamenevis_org:
                    return new string[]
                    {
                        "http://barnamenevis.org/external.php?type=RSS2&forumids=147"
                    };

                case AdSource.irantalent_com:
                    throw new ApplicationException("This site source has not been implemented yet");

                case AdSource.Misc:
                case AdSource.All:
                case AdSource.karvis_ir:
                case AdSource.Email:
                    throw new ApplicationException("This site source can not be extrcted");

                default:
                    throw new ArgumentException("Unknown site source");
            }

        }

        public bool JobUrlExists(string jobUrl)
        {
            return preUrls.Contains(jobUrl) || preIgnoredUrlJobs.Contains(jobUrl);
        }

        /// <summary>
        /// Process title of a job 
        /// </summary>
        public string ProcessTitle(string source)
        {
            return FConvert.ToPersianTotal(FConvert.ReplaceControlCharacters(source, " "));
        }

        /// <summary>
        /// process description text
        /// </summary>
        public string ProcessDescription(string plainDescription)
        {
            return
                FConvert.ToPersianYehKeh(FConvert.ReplaceControlCharacters(
                plainDescription
                .Replace("<br>", " ")
                .Replace("</br>", " ")
                .Replace("<span>", " ")
                .Replace("</span>", " "), " "));
        }

        public void ProcessJob(Job job)
        {
            job.Description = ProcessDescription(job.Description);
            job.Emails = _crawler.ExtractEmailsByText(job.Description);
            job.Tag = ExtractTags(job.Description);
            job.Title = ProcessTitle(job.Title);
        }

        /// <summary>
        /// get absolute url from given relative url and given root url
        /// </summary>
        public string GetAbsoluteUrl(string relativeUrl, string rootUrl)
        {
            return string.Format("{0}{1}", rootUrl, relativeUrl);
        }

        /// <summary>
        /// extract tags from given text
        /// </summary>
        public string ExtractTags(string text)
        {
            string retval = string.Empty;
            string tagPattern = @"[a-zA-Z.#+_@]+";
            string source = text;

            source = FConvert.ToPersianYehKeh(source);
            source = source.ToLower();
            source = source.Replace(",", " ");

            //todo: remove spaces more than one

            //correcting mispelling
            source = source
                .Replace("#c", "C#")
                .Replace("net.", ".Net")
                .Replace("++c", "C++")
                .Replace("++vc", "VC++");

            //persian translation
            source = source
                .Replace("وب", "Web")
                .Replace("و ب", "Web")
                .Replace("آقا", "Male")
                .Replace("خانم", "Female")
                .Replace("طراح قالب", "Template Designer")
                .Replace("دلفی", "Delphi")
                .Replace("تحلیل گر", "Analyst")
                .Replace("تحلیل‌گر", "Analyst")
                .Replace("طراح", "Software Designer")
                .Replace("جاوا", "Java")
                .Replace("بانک اطلاعاتی", "database")
                .Replace("اوراکل", "Oracle")
                .Replace("فلش", "Flash")
                .Replace("جوملا", "Joomla")
                .Replace("وردپرس", "Wordpress")
                .Replace("ورد پرس", "WordPress")
                .Replace("اتوران", "Autoran")
                .Replace("اتو ران", "Autoran")
                .Replace("مدیر پروژه", "Project Manager")
                .Replace("پاره وقت", "Part Time")
                .Replace("شبکه", "Network")
                .Replace("پروژه‌ای", "Project Based")
                .Replace("پروژه ای", "Project Based")
                .Replace("موبایل", "mobile")
                .Replace("آژاکس", "Ajax")
                .Replace("ویندوز", "Windows")
                .Replace("لینوکس", "Linux")
                .Replace("تست", "Test")
                .Replace("آزمون", "Test")
                .Replace("آزمونگر", "Test")
                .Replace("آزمون گر", "Test")
                .Replace("میکرو کنترلر", "Microcontroller")
                .Replace("میکروکنترلر", "Microcontroller")
                .Replace("دات نت", ".Net")
                .Replace("دات‌نت", ".Net");

            //remove email from tags
            foreach (string email in _crawler.ExtractEmailsByText(source).Trim().Split(','))
                if (!string.IsNullOrEmpty(email))
                    source = source.Replace(email.Trim(), " ");

            if (string.IsNullOrEmpty(source.Trim()))
                return retval;

            //find tags with regular expressions
            List<string> tags = new List<string>();
            MatchCollection matches = Regex.Matches(source, tagPattern);
            foreach (Match match in matches)
            {
                string rawTag = match.Value;

                //ignore empty results
                if (string.IsNullOrEmpty(rawTag))
                    continue;

                //ignore 1 length tags
                if (rawTag.Length < 2)
                    continue;

                //ignore duplicate tags
                if (tags.Contains(rawTag))
                    continue;

                tags.Add(rawTag);
            }

            //concatening
            foreach (var item in tags)
                retval += item + ", ";

            return retval;
        }

        public string ExtractRootUrl(string url)
        {
            Regex regex = new Regex(@"http([s]*)://[\w.]*");
            MatchCollection matches = regex.Matches(url);

            if (matches.Count > 0)
                return matches[0].Value;
            else
                return null;
        }

    }
}
