using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoScrapping_sinhvienhufi
{
    class CrawlHelper
    {
        public bool checkId { get; set; }
        public string LinkSchedule{ get; set; }
        public string LinkTimeline { get; set; }
        public string LinkId { get; set; }
        public List<Subject> ListSubject { get; set; }

        private void checkMS(string id)
        {
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["UserName"] = id;
                data["LoaiThu"] = "2";

                try
                {
                    var response = wb.UploadValues("https://sinhvien.hufi.edu.vn/SinhVien/LoginAnonymousEx", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);

                    if (responseInString.Contains("01"))
                        checkId = false;
                    else checkId = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private string GetResponseWebsite(string id)
        {
            //string responseInString = string.Empty;
            //using (var wb = new WebClient())
            //{
            //    var data = new NameValueCollection();
            //    data["MaSinhVien"] = id;
            //    data["HoTen"] = "";
            //    data["NgaySinh"] = "";
            //    data["LopHoc"] = "";
            //    data["SoCMND"] = "";
            //    data["Captcha"] = "";
            //    data["X-Requested-With"] = "XMLHttpRequest";

            //    var response = wb.UploadValues("https://sinhvien.hufi.edu.vn/tra-cuu-thong-tin.html", "POST", data);
            //    responseInString = Encoding.UTF8.GetString(response);
            //}

            //return responseInString;

            var data = new NameValueCollection();
            data["MaSinhVien"] = id;
            data["HoTen"] = "";
            data["NgaySinh"] = "";
            data["LopHoc"] = "";
            data["SoCMND"] = "";
            data["Captcha"] = "";
            data["X-Requested-With"] = "XMLHttpRequest";
            data[""] = "2";
            data[""] = "2";

            return PostMethodReturnResponse("https://sinhvien.hufi.edu.vn/tra-cuu-thong-tin.html", data);
        }

        private string PostMethodReturnResponse(string url, NameValueCollection data)
        {
            string responseInString = string.Empty;

            using (var wb = new WebClient())
            {
                var response = wb.UploadValues(url, "POST", data);
                responseInString = Encoding.UTF8.GetString(response);
            }

            return responseInString;
        }

        private void GetIdLink(string id)
        {
            var html = GetResponseWebsite(id);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//a").Attributes["href"].Value;

            string[] tmp = htmlBody.Split('=');
            LinkId = tmp[1];
        }

        void GetTKB(string LinkId)
        {
            string url = "https://sinhvien.hufi.edu.vn/SinhVienTraCuu/GetDanhSachLichTheoTuan";
            var data = new NameValueCollection();

            data["k"] = LinkId;
            DateTime today = DateTime.Today;
            data["pNgayHienTai"] = today.ToString("d");
            data["pLoaiLich"] = "0";

            string html = PostMethodReturnResponse(url, data);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlBody = htmlDoc.DocumentNode.SelectNodes("//div[@class='content color-lichhoc text-left']");
            ListSubject = new List<Subject>();

            for (int i = 0; i < htmlBody.Count; i++)
            {
                Subject sbj = new Subject();
                sbj.NameSubject = htmlBody[i].ChildNodes[1].InnerText;
                sbj.Room = htmlBody[i].ChildNodes[3].InnerText;
                sbj.Time = "T" + htmlBody[i].ChildNodes[5].ChildNodes[0].InnerText.Split('T')[1]
                    + htmlBody[i].ChildNodes[5].ChildNodes[3].InnerText;
                sbj.Teacher = htmlBody[i].ChildNodes[7].InnerText;

                try
                {
                    sbj.LinkZoom = $"https://sinhvien.hufi.edu.vn{htmlBody[i].ChildNodes[9].ChildNodes[1].Attributes["data-joinzoomurl2"].Value}";
                }
                catch (Exception)
                {
                    sbj.LinkZoom = null;
                }

                ListSubject.Add(sbj);
            } 
        }

        public bool Login(string id)
        {
            checkMS(id);

            if (checkId == false)
                return false;
            else return true;
        }

        public void MainRun(string id)
        {
            GetIdLink(id);

            string link = LinkId;

            GetTKB(link);
        }
    }

    class Subject
    {
        public string NameSubject { get; set; }
        public string Room { get; set; }
        public string  Time { get; set; }
        public string Teacher { get; set; }
        public string LinkZoom { get; set; }
    }
}
