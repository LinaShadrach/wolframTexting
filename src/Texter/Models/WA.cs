using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//the following is for parsing xml // 
using System.Xml;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Texter.Models
{
    public class WA
    {
        //private static RestResponse response;

        public string Question { get; set; }

        public static Queryresult AskWolfram(string question)
        {
            string pattern = "\\s+";
            Regex rgx = new Regex(pattern);
            string questionParsed = rgx.Replace(question, "+");
            RestClient client = new RestClient("http://api.wolframalpha.com/v2/");
            RestRequest request = new RestRequest("query?input="+ questionParsed +"&format=plaintext&output=XML&appid="+ EnvironmentVariables.WolframAPIKey);
            RestResponse response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;

            }).Wait();
            var XmlDS = new RestSharp.Deserializers.XmlDeserializer();
            var responseCSharp = XmlDS.Deserialize<Queryresult>(response);
            Debug.WriteLine("wolfram response:" + responseCSharp);
            return (responseCSharp);
        }
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            TaskCompletionSource<IRestResponse> tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}













            //string wolframResultString = await response.Content.ReadAsStringAsync();

            // Define your URL to call here
            //var uri = new Uri($"http://api.wolframalpha.com/v2/query?input={question}&appid={EnvironmentVariables.WolframAPIKey}");
            // Get and output your response


                //using (XmlReader reader = XmlReader.Create(new StringReader(wolframResultString)))
                //{
                //    XmlWriterSettings ws = new XmlWriterSettings();
                //    ws.Indent = true;
                //    using (XmlWriter writer = XmlWriter.Create(output, ws))
                //    {
                //        // Parse the file and display each of the nodes.
                //        while (reader.Read())
                //        {
                //            switch (reader.NodeType)
                //            {
                //                case XmlNodeType.Element:
                //                    writer.WriteStartElement(reader.Name);
                //                    break;
                //                case XmlNodeType.Text:
                //                    writer.WriteString(reader.Value);
                //                    break;
                //                case XmlNodeType.XmlDeclaration:
                //                case XmlNodeType.ProcessingInstruction:
                //                    writer.WriteProcessingInstruction(reader.Name, reader.Value);
                //                    break;
                //                case XmlNodeType.Comment:
                //                    writer.WriteComment(reader.Value);
                //                    break;
                //                case XmlNodeType.EndElement:
                //                    writer.WriteFullEndElement();
                //                    break;
                //            }
                //        }
                //    }
                //}

                //string stringer = output.ToString();
