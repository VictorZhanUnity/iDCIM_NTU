using System;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.Common;

namespace VictorDev.Net.WebAPI
{
    /// <summary>
    /// 呼叫WebAPI的Request參數設定
    /// </summary>
    [CreateAssetMenu(fileName = "WebAPI Request設定", menuName = ">>VictorDev<</Net/WebAPI/WebAPI Request設定")]
    public class WebAPI_Request : ScriptableObject
    {
        [SerializeField] private enumRequestMethod method;
        public enumRequestMethod requestMethod => method;

        [SerializeField] private Authorization authorization;
        public enumAuthorization authorizationType => authorization.authorizationType;
        public string token
        {
            get => authorization.token.Trim();
            set => authorization.token = value;
        }

        [Header(">>> 回應傳資料的型態")]
        [SerializeField] private enumResponseDataType responseDataType;
        private Dictionary<enumResponseDataType, string> contentTypeTable { get; set; }
        /// <summary>
        /// 回應資料的型態
        /// </summary>
        public string contentType
        {
            get
            {
                contentTypeTable ??= new Dictionary<enumResponseDataType, string>()
                    {
                        { enumResponseDataType.Default, "application/x-www-form-urlencoded" },
                        { enumResponseDataType.JSON, "application/json" },
                        { enumResponseDataType.Text, "text/plain" },
                    };
                return contentTypeTable[responseDataType];
            }
        }

        [Header(">>> 設定WebAPI的IP與PORT (選填)")]
        [SerializeField] private WebAPI_IPConfig ipConfig;

        /// <summary>
        /// 設定WebAPI的IP與PORT (可選)
        /// </summary>
        [TextArea(1, 3)]
        [Header(">>> WebAP完整路徑 / WebAPI IP之後的路徑")]
        [SerializeField] private string apiURL;
        [Header(">>> Params設定")]
        [SerializeField] private QueryParams queryParams;

        [Header(">>> Body設定")]
        [SerializeField] private Body body;
        public enumBody bodyType => body.bodyType;
        public WWWForm formData => body.formData;
        public void SetFormData(Dictionary<string, string> dataSet) => body.SetFormData(dataSet);

        public string BodyJSON => body.rawString;

        public WebAPI_Request(string url) => this.apiURL = url;

        private UriBuilder uriBuilder { get; set; }

        /// <summary>
        /// 完整WebAPI網址
        /// </summary>
        public string url
        {
            get
            {
                if (ipConfig != null)
                {
                    uriBuilder = new UriBuilder(ipConfig.WebIP_Port);
                    uriBuilder.Path += apiURL.Trim();
                }
                else uriBuilder = new UriBuilder(apiURL.Trim());

                string resultString = uriBuilder.Uri.ToString();
                //檢查Params設定
                if (queryParams.IsActivated)
                {
                    resultString = StringHandler.StringBuilderAppend(resultString, queryParams.queryString);
                }
                return resultString;
            }
        }

        [ContextMenu("- 列出Request參數資料")]
        private void LogRequestInfo()
        {
            Debug.Log($">>> [{requestMethod}] URL: {url}");

            if (queryParams.IsActivated)
            {
                Debug.Log($"\t\tqueryString: {queryParams.queryString}");
            }

            if (body.IsActivated)
            {
                switch (body.bodyType)
                {
                    case enumBody.formData:
                        Debug.Log($"\t\tWWWForm binary長度: {body.formData.data.Length}");
                        break;
                }
            }
        }

        #region [ >>> Authorization參數設定 ]
        [Serializable]
        private class Authorization
        {
            public enumAuthorization authorizationType = enumAuthorization.Bearer;
            [TextArea(1, 5)]
            public string token;
        }
        #endregion

        #region [ >>> Params參數設定 ]
        [Serializable]
        public class QueryParams
        {
            [SerializeField] private bool isActivated = false;
            [SerializeField] private List<KeyValueItem> fieldList;

            public bool IsActivated => isActivated;

            public string queryString
            {
                get
                {
                    string resultString = "";
                    if (fieldList.Count > 0 && isActivated)
                    {
                        resultString = "?";
                        fieldList.ForEach(item =>
                        {
                            resultString = StringHandler.StringBuilderAppend(resultString, item.key.Trim(), "=", item.value.Trim());
                            if (fieldList.IndexOf(item) != fieldList.Count - 1) resultString += "&";
                        });
                    }
                    return resultString;
                }
            }

            /// <summary>
            /// 設定Filed資料集
            /// </summary>
            public void SetFiledValue(Dictionary<string, string> dataSet)
            {
                fieldList.Clear();
                foreach (string keyName in dataSet.Keys)
                {
                    fieldList.Add(new KeyValueItem(keyName, dataSet[keyName]));
                }
            }
        }
        #endregion

        #region [ >>> Body參數設定 ]
        [Serializable]
        private class Body
        {
            [SerializeField] private bool isActivated = false;
            [SerializeField] private enumBody _bodyType;
            [Header(">>>供form-data型態使用")]
            [SerializeField] private List<KeyValueItem> fieldList;
            [Header(">>>供raw型態: JSON、TEXT使用")]
            [SerializeField] private string _rawString;

            public bool IsActivated => isActivated;

            public enumBody bodyType => (isActivated) ? _bodyType : enumBody.none;

            public void SetFormData(Dictionary<string, string> dataSet)
            {
                fieldList.Clear();
                foreach (string key in dataSet.Keys)
                {
                    fieldList.Add(new KeyValueItem(key, dataSet[key]));
                }
            }

            /// <summary>
            /// form-data型態的參數值
            /// </summary>
            public WWWForm formData
            {
                get
                {
                    if (IsActivated == false || bodyType != enumBody.formData) return null;

                    WWWForm result = new WWWForm();
                    fieldList.ForEach(item =>
                    {
                        Debug.Log($"\t[form-data] {item.key.Trim()} = {item.value.Trim()}");
                        result.AddField(item.key.Trim(), item.value.Trim());
                    });
                    return result;
                }
            }

            public string rawString
            {
                set => _rawString = value;
                get
                {
                    //待優化處理
                    return (bodyType == enumBody.rawJSON) ? _rawString : "empty"; ;
                }
            }

            /// <summary>
            /// 設定Filed資料集
            /// </summary>
            public void SetFiledValue(Dictionary<string, string> dataSet)
            {
                fieldList.Clear();
                foreach (string keyName in dataSet.Keys)
                {
                    fieldList.Add(new KeyValueItem(keyName, dataSet[keyName]));
                }
            }
        }
        #endregion

        [Serializable]
        public class KeyValueItem
        {
            public string key;
            [TextArea(1, 5)]
            public string value;

            public KeyValueItem(string key, string value)
            {
                this.key = key;
                this.value = value;
            }
        }
    }

    public enum enumRequestMethod { GET, HEAD, POST, PUT, CREATE, DELETE }
    public enum enumAuthorization { NoAuth, Bearer }
    public enum enumBody { none, formData, rawJSON, rawText, binary }
    /// <summary>
    /// 預設值為application/x-www-form-urlencoded
    /// </summary>
    public enum enumResponseDataType { Default, JSON, Text }

}