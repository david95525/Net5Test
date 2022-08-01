using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Services.DomainModels
{
    public enum Status
    {
        /// <summary>
        /// 正確執行無任何錯誤
        /// </summary>
        [Description("success")]
        Success = 10000,

        /// <summary>
        /// 參數為必填
        /// </summary>
        [Description("parameter is Required")]
        ParameterdoesNotExist = 1001,

        /// <summary>
        /// 參數為空值
        /// </summary>
        [Description("parameter is null")]
        ParameterIsNull = 1002,

        /// <summary>
        /// 參數不為數字
        /// </summary>
        [Description("parameter is not numeric")]
        ParameterIsNotNumeric = 1003,

        /// <summary>
        /// 參數不為整數
        /// </summary>
        [Description("parameter is not integer")]
        ParameterIsNotInteger = 1004,

        /// <summary>
        /// 參數不為正整數
        /// </summary>
        [Description("parameter is not positive integer")]
        ParameterIsNotPositiveInteger = 1005,

        /// <summary>
        /// 參數不為負整數
        /// </summary>
        [Description("parameter is not negative integer")]
        ParameterIsNotNegativeInteger = 1006,

        /// <summary>
        /// 參數不為email格式
        /// </summary>
        [Description("parameter is not email")]
        ParameterIsNotEmail = 1007,

        /// <summary>
        /// 參數格式錯誤
        /// </summary>
        [Description("parameter format error")]
        ParameterFormatError = 1008,

        /// <summary>
        /// 參數錯誤
        /// </summary>
        [Description("parameter error")]
        ParameterError = 1009,

        /// <summary>
        /// 日期格式錯誤
        /// </summary>
        [Description("date format error")]
        DateFormatError = 1010,

        /// <summary>
        /// 帳號不存在
        /// </summary>
        [Description("account does not exist")]
        AccountDoesNotExist = 2001,

        /// <summary>
        /// 帳號重複
        /// </summary>
        [Description("repeat_account")]
        RepeatAccount = 2002,

        /// <summary>
        /// 帳號尚未認證
        /// </summary>
        [Description("account is not registration")]
        AccountIsNotRegistration = 2003,

        /// <summary>
        /// 帳號停用
        /// </summary>
        [Description("account is stop")]
        AccountIsStop = 2004,

        /// <summary>
        /// 密碼錯誤
        /// </summary>
        [Description("password error")]
        PasswordError = 2005,

        /// <summary>
        /// 密碼長度錯誤
        /// </summary>
        [Description("parameter needs to be between 4 and 12 characters")]
        PassworLengthdError = 2006,

        /// <summary>
        /// 註冊方式(忘記密碼)
        /// </summary>
        [Description("reg_type is not email")]
        RegType = 2051,

        /// <summary>
        /// 查詢資料錯誤
        /// </summary>
        [Description("fetch data error")]
        FetchDataError = 3001,

        /// <summary>
        /// 新增錯誤
        /// </summary>
        [Description("add_error")]
        AddError = 3002,

        /// <summary>
        /// 修改錯誤
        /// </summary>
        [Description("update_error")]
        UpdateError = 3003,

        /// <summary>
        /// 刪除錯誤
        /// </summary>
        [Description("delete_error")]
        DeleteError = 3004,

        /// <summary>
        /// 驗證碼
        /// </summary>
        [Description("enc_error")]
        EncError = 3501,

        /// <summary>
        /// api_key錯誤
        /// </summary>
        [Description("api_key_error")]
        ApiKeyError = 3502,

        /// <summary>
        /// 檔案上傳失敗
        /// </summary>
        [Description("file upload failed")]
        FileUploadFailed = 3503,

        /// <summary>
        /// 資料重複
        /// </summary>
        [Description("repeat_data")]
        RepeatData = 3504,

        /// <summary>
        /// 資料處裡中
        /// </summary>
        [Description("data is processing")]
        DataProcessing = 3505,

        /// <summary>
        /// 資料已處理(已完成)，用於只能處理一次時的狀態
        /// </summary>
        [Description("data is processed")]
        DataProcessed = 3506,

        /// <summary>
        /// authorization_code錯誤
        /// </summary>
        [Description("authorization_code_error")]
        AuthorizationCodeError = 5001,

        /// <summary>
        /// access_token錯誤
        /// </summary>
        [Description("access_token_error")]
        AccessTokenError = 5002,

        /// <summary>
        /// refresh_token錯誤
        /// </summary>
        [Description("refresh_token_error")]
        RefreshTokenError = 5003,

        /// <summary>
        /// client_id錯誤
        /// </summary>
        [Description("client_id_error")]
        ClientIdError = 5004,

        /// <summary>
        /// client_secret錯誤
        /// </summary>
        [Description("client_secret_error")]
        ClientSecretError = 5005,

        /// <summary>
        /// redirect_uri錯誤
        /// </summary>
        [Description("client_secret_error")]
        RedirectUriError = 5006,

        /// <summary>
        /// scope錯誤
        /// </summary>
        [Description("client_secret_error")]
        ScopeError = 5007,

        /// <summary>
        /// 無效的請求
        /// </summary>
        [Description("oauth_invalid_request")]
        OauthInvalidRequest = 5101,

        /// <summary>
        /// 無效的授權請求
        /// </summary>
        [Description("oauth_invalid_scope")]
        OauthInvalidScope = 5102,

        /// <summary>
        /// 無效的redirect_uri請求
        /// </summary>
        [Description("oauth_invalid_redirect_uri")]
        OauthInvalidRedirectUri = 5103,

        /// <summary>
        /// 開發者無此授權方式的權限
        /// </summary>
        [Description("oauth_unauthorized_client")]
        OauthUnauthorizedClient = 5201,

        /// <summary>
        /// 開發者被拒絕授權(被停權)
        /// </summary>
        [Description("oauth_access_denied")]
        OauthAccessDenied = 5202,

        /// <summary>
        /// 伺服器不支援此方式獲取code、token...
        /// </summary>
        [Description("oauth_unsupported_response_type")]
        OauthUnsupportedResponseType = 5203,

        /// <summary>
        /// 伺服器不支援此授權方式
        /// </summary>
        [Description("oauth_unsupported_grant_type")]
        OauthUnsupportedGrantType = 5204,

        /// <summary>
        /// authorization_code過期
        /// </summary>
        [Description("oauth_expired_authorization_code")]
        AuthorizationCode過期 = 5205,

        /// <summary>
        /// refresh_token過期
        /// </summary>
        [Description("oauth_expired_refresh_token")]
        OauthExpiredRefreshToken = 5206,

        /// <summary>
        /// access_token過期
        /// </summary>
        [Description("oauth_expired_access_token")]
        OauthExpiredAccessToken = 5207,

        /// <summary>
        /// 伺服器錯誤
        /// </summary>
        [Description("oauth_server_error")]
        OauthServerError = 5231,

        /// <summary>
        /// 伺服器超載或維修中
        /// </summary>
        [Description("oauth_temporarily_unavailable")]
        OauthTemporarilyUnavailable = 5232,

        /// <summary>
        /// 異常錯誤
        /// </summary>
        [Description("an unknown error occurred")]
        AnUnknownErrorOccurred = 9999,

    }
}
