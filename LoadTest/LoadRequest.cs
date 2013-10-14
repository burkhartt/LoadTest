using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace LoadTest {
    public class LoadRequest {
        private readonly NameValueCollection headers;
        private readonly RequestMethod requestMethod;
        private readonly NameValueCollection requestParams;
        private readonly string baseUrl;

        public LoadRequest(string name, string baseUrl, RequestMethod requestMethod, NameValueCollection headers, NameValueCollection requestParams) {
            Name = name;
            this.baseUrl = baseUrl;
            this.requestMethod = requestMethod;
            this.headers = headers;
            this.requestParams = requestParams;
        }

        public string Name { get; private set; }
        public TimeSpan TimeTaken { get; private set; }
        public bool IsFinished { get; private set; }

        public string Url { get { return GetRequestUri(); } }

        public void Run() {
            var t = new Thread(ThreadProc);
            t.Start();
        }

        private void ThreadProc() {
            var requestUri = GetRequestUri();
            var request = WebRequest.Create(requestUri);
            AddRequestParams(request);
            AddHeaders(request);
            request.Method = requestMethod.ToString();

            var timer = new Stopwatch();
            timer.Start();
            request.GetResponse().Close();
            timer.Stop();
            TimeTaken = timer.Elapsed;
            IsFinished = true;
        }

        private string GetRequestUri() {
            if (requestMethod != RequestMethod.Get) {
                return baseUrl;
            }

            var formattedParams = GetFormattedRequestParams();
            return string.Format("{0}?{1}", baseUrl, formattedParams);
        }

        private void AddRequestParams(WebRequest request) {
            if (requestMethod == RequestMethod.Get) {
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