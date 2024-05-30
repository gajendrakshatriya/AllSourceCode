using Azure;
using Microsoft.Azure.Amqp.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


    /*
  This code sample shows Custom Model operations with the Azure Form Recognizer client library. 

  To learn more, please visit the documentation - Quickstart: Form Recognizer C# client library SDKs
  https://learn.microsoft.com/azure/applied-ai-services/form-recognizer/quickstarts/get-started-v3-sdk-rest-api?view=doc-intel-3.1.0&pivots=programming-language-csharp
*/

using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace AzureOperationsLib
{

    public class DocFields
    {
        public string FieldName { get; set; }
        public string Content { get; set; }
        public float? Confidence { get; set; }
    }
    public class OCROP
    {

        /*
          Remember to remove the key from your code when you're done, and never post it publicly. For production, use
          secure methods to store and access your credentials. For more information, see 
          https://docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-security?tabs=command-line%2Ccsharp#environment-variables-and-application-configuration
        */

        public async void ProcessDocAsync(string blobFileUrl)
        {
            string endpoint = "https://my-form-ocr-test-2024.cognitiveservices.azure.com/";// " < endpoint>";
            string apiKey = "37ea1a19040441b48b6a465ef6b1602b";// " < apiKey>";
            AzureKeyCredential credential = new AzureKeyCredential(apiKey);
            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            string modelId = "Training-3.0";// "<modelId>";
            Uri fileUri = new Uri(blobFileUrl);// new Uri("https://azstoragetest2024.blob.core.windows.net/aidocument/5.pdf");// new Uri("<fileUri>");

            AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, modelId, fileUri);
            AnalyzeResult result = operation.Value;

            Console.WriteLine($"Document was analyzed with model with ID: {result.ModelId}");

            foreach (AnalyzedDocument document in result.Documents)
            {
                Console.WriteLine($"Document of type: {document.DocumentType}");

                foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
                {
                    string fieldName = fieldKvp.Key;
                    DocumentField field = fieldKvp.Value;

                    Console.WriteLine($"Field '{fieldName}': ");

                    Console.WriteLine($"  Content: '{field.Content}'");
                    Console.WriteLine($"  Confidence: '{field.Confidence}'");
                }
            }

            // Iterate over lines and selection marks on each page
            foreach (DocumentPage page in result.Pages)
            {
                Console.WriteLine($"Lines found on page {page.PageNumber}");
                foreach (var line in page.Lines)
                {
                    Console.WriteLine($"  {line.Content}");
                }

                Console.WriteLine($"Selection marks found on page {page.PageNumber}");
                foreach (var selectionMark in page.SelectionMarks)
                {
                    Console.WriteLine($"  Selection mark is '{selectionMark.State}' with confidence {selectionMark.Confidence}");
                }
            }

            // Iterate over the document tables
            for (int i = 0; i < result.Tables.Count; i++)
            {
                Console.WriteLine($"Table {i + 1}");
                foreach (var cell in result.Tables[i].Cells)
                {
                    Console.WriteLine($"  Cell[{cell.RowIndex}][{cell.ColumnIndex}] has content '{cell.Content}' with kind '{cell.Kind}'");
                }
            }
        }

        
        public async Task<List<DocFields>> GetProcessedDocAsync(string blobFileUrl)
        {
            string endpoint = "https://my-form-ocr-test-2024.cognitiveservices.azure.com/";// " < endpoint>";
            string apiKey = "37ea1a19040441b48b6a465ef6b1602b";// " < apiKey>";
            AzureKeyCredential credential = new AzureKeyCredential(apiKey);
            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            string modelId = "Training-3.0";// "<modelId>";
            Uri fileUri = new Uri(blobFileUrl);// new Uri("https://azstoragetest2024.blob.core.windows.net/aidocument/5.pdf");// new Uri("<fileUri>");

            AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, modelId, fileUri);
            AnalyzeResult result = operation.Value;

            Console.WriteLine($"Document was analyzed with model with ID: {result.ModelId}");

            var lstResult = new List<DocFields>();
            foreach (AnalyzedDocument document in result.Documents)
            {
                Console.WriteLine($"Document of type: {document.DocumentType}");

                foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
                {
                    string fieldName = fieldKvp.Key;
                    DocumentField field = fieldKvp.Value;

                    Console.WriteLine($"Field '{fieldName}': ");
                    Console.WriteLine($"  Content: '{field.Content}'");
                    Console.WriteLine($"  Confidence: '{field.Confidence}'");

                    var processedResult = new DocFields { FieldName = fieldName, Content = field.Content, Confidence = field.Confidence };
                    lstResult.Add(processedResult);
                }
            }

            return lstResult;
        }
    }
}
