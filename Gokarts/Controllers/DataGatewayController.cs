using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using Gokarts.Models;

namespace Gokarts.Controllers;

public static class DataGatewayController
{
    public static async Task<DataModel> GetAsync()
    {
        DataModel returnModel;

        string htmlCode = await DownloadHtmlAsync(AppConfig.GATEWAY_HOST);
        returnModel = ExtractValues(htmlCode);
        
        return returnModel;
    }

    static async Task<string> DownloadHtmlAsync(string url)
    {
        HttpClient client = new();
        return await client.GetStringAsync(url);
    }

    static DataModel ExtractValues(string htmlCode)
    {
        DataModel returnModel = new();
        HtmlDocument htmlDocument = new();
        htmlDocument.LoadHtml(htmlCode);

        returnModel.IsStart = Convert.ToBoolean(Convert.ToInt16(htmlDocument.DocumentNode.SelectNodes("//p[@class='lap_start']")[0].InnerText.Trim()));

        returnModel.Foto_1 = Convert.ToBoolean(Convert.ToInt16(htmlDocument.DocumentNode.SelectNodes("//p[@class='foto_0']")[0].InnerText.Trim()));
        returnModel.Foto_2 = Convert.ToBoolean(Convert.ToInt16(htmlDocument.DocumentNode.SelectNodes("//p[@class='foto_1']")[0].InnerText.Trim()));
        returnModel.Foto_3 = Convert.ToBoolean(Convert.ToInt16(htmlDocument.DocumentNode.SelectNodes("//p[@class='foto_2']")[0].InnerText.Trim()));

        returnModel.ConfiguarationType = Convert.ToBoolean(Convert.ToInt16(htmlDocument.DocumentNode.SelectNodes("//p[@class='race_configuration']")[0].InnerText.Trim()));

        returnModel.Time_1 = htmlDocument.DocumentNode.SelectNodes("//p[@class='time_0']")[0].InnerText.Trim();
        returnModel.Time_2 = htmlDocument.DocumentNode.SelectNodes("//p[@class='time_1']")[0].InnerText.Trim();
        returnModel.Time_3 = htmlDocument.DocumentNode.SelectNodes("//p[@class='time_3']")[0].InnerText.Trim();

        returnModel.LapTime = htmlDocument.DocumentNode.SelectNodes("//p[@class='lap_time']")[0].InnerText.Trim();

        returnModel.FullLap = Convert.ToInt16(htmlDocument.DocumentNode.SelectNodes("//p[@class='main_lap_counter']")[0].InnerText.Trim());



        #region lapCounterDL
        var lapCounterElement = htmlDocument.DocumentNode.SelectNodes("//p[@class='lap_counter']");
        if (lapCounterElement != null)
        {
            foreach (var node in lapCounterElement)
            {
                if (node.InnerText.Trim() != String.Empty && node.InnerText.Trim() != "0")
                {
                    returnModel.LapCounter = Convert.ToInt16(node.InnerText.Trim());
                }
            }
        }
        #endregion

        return returnModel;
    }
}
