using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data;
using mshtml;
using hulilab.Models.DAL;

namespace hulilab.Models.BLL
{
    public class PublicationService : BaseService<Publication>, IService<Publication>
    {
        private const string GOOGLESCHOLARURL = "http://scholar.google.com/citations?sortby=pubdate&hl=en&user=gQ63orgAAAAJ&pagesize=100&view_op=list_works";
        public bool UpdatePublicationDatabase()
        {
            bool isSuccess = false;
            try
            {
                WebClient wclient = new WebClient();
                string htmlPage = wclient.DownloadString(GOOGLESCHOLARURL);
                IHTMLDocument2 hd = new HTMLDocumentClass() as IHTMLDocument2;
                hd.write(htmlPage);
                hd.close();

                foreach (HTMLAnchorElement elink in hd.links)
                {
                    if (!elink.href.StartsWith("http://") && !elink.href.StartsWith("https://") && !elink.href.StartsWith("ftp://") && elink.href.StartsWith("about:"))
                    {
                        elink.setAttribute("href", "http://scholar.google.com" + elink.href.Substring(6));
                    }
                }

                IHTMLDocument3 hd3 = hd as IHTMLDocument3;
                HTMLTable paperTable = null;

                foreach (IHTMLElement ht in hd3.getElementsByTagName("table"))
                {
                    if (ht.className != null && ht.className.Equals("cit-table"))
                    {
                        paperTable = (HTMLTable)ht;
                        break;
                    }
                }
                PublicationService ps = new PublicationService();
                foreach (HTMLTableRow row in paperTable.rows)
                {
                    if (row.className.Contains("item"))
                    {
                        Publication publication = new Publication();

                        foreach (HTMLTableCell cell in row.cells)
                        {
                            if (cell.id == "col-title")
                            {
                                foreach (IHTMLDOMNode node in cell.childNodes as IHTMLDOMChildrenCollection)
                                {
                                    if (node is HTMLAnchorElement)
                                    {
                                        HTMLAnchorElement href = node as HTMLAnchorElement;
                                        publication.Title = href.innerText;
                                        publication.Link = href.href;
                                    }
                                    else if (node is HTMLSpanElement)
                                    {
                                        if (string.IsNullOrEmpty(publication.Authors))
                                        {
                                            publication.Authors = (node as HTMLSpanElement).innerText;
                                        }
                                        else if (string.IsNullOrEmpty(publication.Magazine))
                                        {
                                            publication.Magazine = (node as HTMLSpanElement).innerText;
                                        }
                                    }
                                }
                            }
                            else if (cell.id == "col-citedby")
                            {
                                string temp = cell.innerText;
                                int cited;
                                if (!String.IsNullOrEmpty(temp) && int.TryParse(temp, out cited))
                                {
                                    publication.CitedBy = cited;
                                }
                            }
                            else if (cell.id == "col-year")
                            {
                                publication.PublishYear = cell.innerText;
                            }
                        }


                        if (!ps.Add(publication))
                        {
                            errorMsg += ps.ErrorMsg + "\r\n";
                        }
                    }
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return isSuccess;
        }
    }
}