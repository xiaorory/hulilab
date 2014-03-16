using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using mshtml;

namespace hulilab.Models.BLL
{
    public class PaperService
    {
        private const string GOOGLESCHOLARURL ="http://scholar.google.com/citations?sortby=pubdate&hl=en&user=gQ63orgAAAAJ&pagesize=100&view_op=list_works";
        public static bool DoGetPapers(out string result, out string error)
        {
            string papersForm = "citationsForm";
            error = string.Empty;
            result = string.Empty;
            bool isSuccess = false;
            try
            {
                WebClient wclient = new WebClient();
                string htmlPage = wclient.DownloadString(GOOGLESCHOLARURL);
                IHTMLDocument2 hd = new HTMLDocumentClass() as IHTMLDocument2;
                hd.write(htmlPage);
                hd.close();
                foreach (HTMLAnchorElement elink in hd.anchors)
                {
                    if (!elink.href.StartsWith("http://") &&!elink.href.StartsWith("https://")&&!elink.href.StartsWith("ftp://")&&elink.href.StartsWith("about:"))
                    {
                        elink.setAttribute("href","http://scholar.google.com" + elink.href.Substring(6));
                    }
                }
                IHTMLElement el = hd.all.item(papersForm, 0) as IHTMLElement;
                result = el.innerHTML;
                isSuccess = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return isSuccess;
        }


    }
}