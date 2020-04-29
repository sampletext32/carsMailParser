using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.Collections.Specialized;

namespace carsMailParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        async Task<string> LoadHTML(string address)
        {
            WebClient wc = new WebClient();
            string html = Encoding.UTF8.GetString(await wc.DownloadDataTaskAsync(address));
            wc.Dispose();
            return html;
        }
        public HtmlDocument HTMLDocumentFromString(string html)
        {
            WebBrowser wb = new WebBrowser();
            wb.ScriptErrorsSuppressed = true;
            wb.DocumentText = "text";
            wb.Document.OpenNew(true);
            wb.Document.Write(html);
            HtmlDocument doc = wb.Document;
            wb.Dispose();
            return doc;
        }
        public HtmlElementCollection GetAll_A_Tags(HtmlDocument doc)
        {
            return doc.GetElementsByTagName("a");
        }


        private async void buttonBeginLoad_Click(object sender, EventArgs e)
        {
            this.Text = "0";
            string MainHTML = await LoadHTML("https://cars.mail.ru/catalog/");
            List<CarCompany> companies = new List<CarCompany>();
            //Страница Каталог
            HtmlDocument companiesDoc = HTMLDocumentFromString(MainHTML);
            HtmlElementCollection atagsCompanies = GetAll_A_Tags(companiesDoc);

            MainHTML = "";
            for (int i = 0; i < atagsCompanies.Count; i++)
            {
                if (atagsCompanies[i].GetAttribute("className") == ("p-firm__text link-holder"))
                {
                    string ToCompanyLink = "https://cars.mail.ru" + atagsCompanies[i].GetAttribute("href").Substring(6);

                    string CompanyTitle = atagsCompanies[i].InnerText;

                    CarCompany currentCompany = new CarCompany();
                    currentCompany.Name = CompanyTitle;

                    string CompanyPageHTML = await LoadHTML(ToCompanyLink);
                    //Страница марки машины
                    HtmlDocument CompanyDocument = HTMLDocumentFromString(CompanyPageHTML);
                    HtmlElementCollection atagsToModels = GetAll_A_Tags(CompanyDocument);

                    CompanyPageHTML = "";
                    for (int j = 0; j < atagsToModels.Count; j++)
                    {
                        if (atagsToModels[j].GetAttribute("className") == ("catalog-generation__card__title"))
                        {
                            string ToModelLink = "https://cars.mail.ru" + atagsToModels[j].GetAttribute("href").Substring(6);

                            string ModelName = atagsToModels[j].InnerText;
                            CarModel currentModel = new CarModel();
                            currentModel.Name = ModelName;

                            string ModelPageHTML = await LoadHTML(ToModelLink);
                            //Страница Модификации и комплектации
                            HtmlDocument ModelsDoc = HTMLDocumentFromString(ModelPageHTML);
                            HtmlElementCollection divtagsToModelConfigs = ModelsDoc.GetElementsByTagName("div");

                            ModelPageHTML = "";
                            for (int k = 0; k < divtagsToModelConfigs.Count; k++)
                            {
                                if (divtagsToModelConfigs[k].GetAttribute("data-mp-el") == "CarTypesFilter.item")
                                {
                                    string ToModelConfigLink = "https://cars.mail.ru" + divtagsToModelConfigs[k].GetElementsByTagName("a")[0].GetAttribute("href").Substring(6);

                                    string configName = divtagsToModelConfigs[k].GetElementsByTagName("a")[0].InnerText;

                                    string ModelConfigPageHTML = await LoadHTML(ToModelConfigLink);

                                    CarMod currentMod = new CarMod();
                                    currentMod.Name = configName;

                                    //Страница Модификации и комплектации
                                    HtmlDocument carConfigDoc = HTMLDocumentFromString(ModelConfigPageHTML);
                                    HtmlElementCollection divtagsCarConfig = carConfigDoc.GetElementsByTagName("div");

                                    ModelConfigPageHTML = "";

                                    for (int m = 0; m < divtagsCarConfig.Count; m++)
                                    {
                                        if (divtagsCarConfig[m].GetAttribute("className") == ("catalog-age__feat__item clear"))
                                        {                                      
                                            string title = divtagsCarConfig[m].Children[0].InnerText;
                                            string content = divtagsCarConfig[m].Children[1].InnerText;
                                            currentMod.info.Add(new Tuple<string, string>(title, content));
                                        }
                                    }                                    
                                    currentModel.Configs.Add(currentMod);
                                    //MessageBox.Show("Закончено скачивание модификации " + currentMod.Name);
                                    this.Text = (int.Parse(this.Text)+1).ToString();
                                }
                            }
                            currentCompany.Models.Add(currentModel);
                            //MessageBox.Show("Закончено скачивание модели " + currentModel.Name);
                        }
                    }
                    companies.Add(currentCompany);
                    //MessageBox.Show("Закончено скачивание компании " + currentCompany.Name);
                }
            }


            //Здесь можно работать с данными
            MessageBox.Show("Завершено скачивание машин!");
        }
    }
}
