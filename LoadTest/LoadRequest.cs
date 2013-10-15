using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;

namespace LoadTest {
    public class LoadRequest {
        private readonly NameValueCollection headers;
        private readonly HttpVerbs httpVerb;
        private readonly NameValueCollection requestParams;
        private readonly string baseUrl;

        public LoadRequest(string baseUrl, HttpVerbs httpVerb, NameValueCollection headers, NameValueCollection requestParams) {
            this.baseUrl = baseUrl;
            this.httpVerb = httpVerb;
            this.headers = headers;
            this.requestParams = requestParams;
        }

        public TimeSpan TimeTaken { get; private set; }
        public bool IsFinished { get; private set; }
        public string Url { get { return GetRequestUri(); } }
        public long RequestContentLength { get; private set; }
        public long ResponseContentLength { get; private set; }

        public void Run() {
            var t = new Thread(ThreadProc);
            t.Start();
        }

        private void ThreadProc() {
            var requestUri = GetRequestUri();
            var request = WebRequest.Create(requestUri);
            request.Method = httpVerb.ToString();
            AddRequestParams(request);
            AddHeaders(request);
            RequestContentLength = request.ContentLength;
            
            var timer = new Stopwatch();
            timer.Start();
            GetResponse(request);
            timer.Stop();
            TimeTaken = timer.Elapsed;
            IsFinished = true;
        }

        private void GetResponse(WebRequest request) {
            var response = request.GetResponse();
            ResponseContentLength = response.ContentLength;
            response.Close();
        }

        private string GetRequestUri() {
            if (httpVerb != HttpVerbs.Get) {
                return baseUrl;
            }

            var formattedParams = GetFormattedRequestParams();
            return string.Format("{0}?{1}", baseUrl, formattedParams);
        }

        private void AddRequestParams(WebRequest request) {
            if (httpVerb == HttpVerbs.Get) {
                return;
            }

            AddPostRequestParams(request);
        }
        
        private void AddPostRequestParams(WebRequest request) {
            var s = GetFormattedRequestParams();
            var byteParameters = Encoding.UTF8.GetBytes(s);
            var requestStream = request.GetRequestStream();
            requestStream.Write(byteParameters, 0, byteParameters.Length);
            requestStream.Close();
        }

        private string GetFormattedRequestParams() {
            var sb = new StringBuilder();
            foreach (var requestParamKey in requestParams.AllKeys) {
                sb.Append(string.Format("&{0}={1}", requestParamKey, requestParams[requestParamKey]));
            }
            var s = sb.ToString();
            return s;
        }

        private void AddHeaders(WebRequest request) {
            request.Headers.Add(headers);
        }
    }
}