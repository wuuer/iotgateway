using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WalkingTec.Mvvm.Core.ConfigOptions;

namespace WalkingTec.Mvvm.Core
{
    /// <summary>
    /// Configs
    /// </summary>
    public class Configs
    {
        #region ConnectionStrings

        private List<CS> _connectStrings;

        /// <summary>
        /// ConnectionStrings
        /// </summary>
        public List<CS> Connections
        {
            get
            {
                if (_connectStrings == null)
                {
                    _connectStrings = new List<CS>();
                }
                return _connectStrings;
            }
            set
            {
                _connectStrings = value;
            }
        }

        #endregion ConnectionStrings

        #region Domains

        private Dictionary<string, Domain> _domains;

        /// <summary>
        /// ConnectionStrings
        /// </summary>
        public Dictionary<string, Domain> Domains
        {
            get
            {
                if (_domains == null)
                {
                    _domains = new Dictionary<string, Domain>();
                }
                return _domains;
            }
            set
            {
                _domains = new Dictionary<string, Domain>();
                foreach (var domain in value)
                {
                    _domains.Add(domain.Key.ToLower(), domain.Value);
                }
                foreach (var item in _domains)
                {
                    if (item.Value != null)
                    {
                        item.Value.Name = item.Key;
                    }
                }
            }
        }

        private bool? _hasMainHost;

        public bool HasMainHost
        {
            get
            {
                if (_hasMainHost == null)
                {
                    _mainHost = Domains?.Where(x => x.Key.ToLower() == "mainhost").Select(x => x.Value.Address).FirstOrDefault();
                    _hasMainHost = !string.IsNullOrEmpty(_mainHost);
                }
                return _hasMainHost == true;
            }
        }

        private string _mainHost;

        public string MainHost
        {
            get
            {
                _mainHost = Domains?.Where(x => x.Key.ToLower() == "mainhost").Select(x => x.Value.Address).FirstOrDefault();
                if (_mainHost != null)
                {
                    _mainHost = _mainHost.Trim();
                    if (_mainHost.EndsWith('/') || _mainHost.EndsWith('\\'))
                    {
                        _mainHost = _mainHost[0..^1];
                    }
                }
                return _mainHost;
            }
        }

        #endregion Domains

        #region QuickDebug

        private bool? _isQuickDebug;

        /// <summary>
        /// Is debug mode
        /// </summary>
        public bool IsQuickDebug
        {
            get
            {
                return _isQuickDebug ?? false;
            }
            set
            {
                _isQuickDebug = value;
            }
        }

        #endregion QuickDebug

        #region Tenant

        private bool? _enableTenant;

        /// <summary>
        /// Is debug mode
        /// </summary>
        public bool EnableTenant
        {
            get
            {
                return _enableTenant ?? false;
            }
            set
            {
                _enableTenant = value;
            }
        }

        #endregion Tenant

        public string ErrorHandler { get; set; } = "/_Framework/Error";

        #region Cookie prefix

        private string _cookiePre;

        /// <summary>
        /// Cookie prefix
        /// </summary>
        public string CookiePre
        {
            get
            {
                return _cookiePre ?? string.Empty;
            }
            set
            {
                _cookiePre = value;
            }
        }

        #endregion Cookie prefix

        #region PageMode

        private PageModeEnum? _pageMode;

        /// <summary>
        /// PageMode
        /// </summary>
        public PageModeEnum PageMode
        {
            get
            {
                if (_pageMode == null)
                {
                    _pageMode = PageModeEnum.Single;
                }
                return _pageMode.Value;
            }
            set
            {
                _pageMode = value;
            }
        }

        #endregion PageMode

        #region TabMode

        private TabModeEnum? _tabMode;

        /// <summary>
        /// TabMode
        /// </summary>
        public TabModeEnum TabMode
        {
            get
            {
                if (_tabMode == null)
                {
                    _tabMode = TabModeEnum.Default;
                }
                return _tabMode.Value;
            }
            set
            {
                _tabMode = value;
            }
        }

        #endregion TabMode

        #region BlazorMode

        private BlazorModeEnum? _blazorMode;

        /// <summary>
        /// TabMode
        /// </summary>
        public BlazorModeEnum BlazorMode
        {
            get
            {
                if (_blazorMode == null)
                {
                    _blazorMode = BlazorModeEnum.Server;
                }
                return _blazorMode.Value;
            }
            set
            {
                _blazorMode = value;
            }
        }

        #endregion BlazorMode

        #region Custom settings

        private Dictionary<string, string> _appSettings;

        /// <summary>
        /// Custom settings
        /// </summary>
        public Dictionary<string, string> AppSettings
        {
            get
            {
                if (_appSettings == null)
                {
                    _appSettings = new Dictionary<string, string>();
                }
                return _appSettings;
            }
            set
            {
                _appSettings = value;
            }
        }

        #endregion Custom settings

        #region FileOptions

        private FileUploadOptions _fileUploadOptions;

        /// <summary>
        /// FileOptions
        /// </summary>
        public FileUploadOptions FileUploadOptions
        {
            get
            {
                if (_fileUploadOptions == null)
                {
                    _fileUploadOptions = new FileUploadOptions()
                    {
                        UploadLimit = DefaultConfigConsts.DEFAULT_UPLOAD_LIMIT,
                        SaveFileMode = "database",
                        Settings = new Dictionary<string, List<FileHandlerOptions>>()
                    };
                }
                return _fileUploadOptions;
            }
            set
            {
                _fileUploadOptions = value;
            }
        }

        #endregion FileOptions

        #region UIOptions

        private UIOptions _uiOptions;

        /// <summary>
        /// UIOptions
        /// </summary>
        public UIOptions UIOptions
        {
            get
            {
                if (_uiOptions == null)
                {
                    _uiOptions = new UIOptions();
                    if (_uiOptions.DataTable == null)
                        _uiOptions.DataTable = new UIOptions.DataTableOptions
                        {
                            RPP = DefaultConfigConsts.DEFAULT_RPP
                        };

                    if (_uiOptions.ComboBox == null)
                        _uiOptions.ComboBox = new UIOptions.ComboBoxOptions
                        {
                            DefaultEnableSearch = DefaultConfigConsts.DEFAULT_COMBOBOX_DEFAULT_ENABLE_SEARCH
                        };

                    if (_uiOptions.DateTime == null)
                        _uiOptions.DateTime = new UIOptions.DateTimeOptions
                        {
                            DefaultReadonly = DefaultConfigConsts.DEFAULT_DATETIME_DEFAULT_READONLY
                        };

                    if (_uiOptions.SearchPanel == null)
                        _uiOptions.SearchPanel = new UIOptions.SearchPanelOptions
                        {
                            DefaultExpand = DefaultConfigConsts.DEFAULT_SEARCHPANEL_DEFAULT_EXPAND
                        };
                }
                return _uiOptions;
            }
            set
            {
                _uiOptions = value;
            }
        }

        #endregion UIOptions

        #region Is FileAttachment public

        private bool? _isFilePublic;

        /// <summary>
        /// Is FileAttachment public
        /// </summary>
        public bool IsFilePublic
        {
            get
            {
                return _isFilePublic ?? false;
            }
            set
            {
                _isFilePublic = value;
            }
        }

        #endregion Is FileAttachment public

        #region UEditorOptions

        private UEditorOptions _ueditorOptions;

        /// <summary>
        /// UEditor配置
        /// </summary>
        /// <value></value>
        public UEditorOptions UEditorOptions
        {
            get
            {
                if (_ueditorOptions == null)
                {
                    _ueditorOptions = new UEditorOptions();
                }
                return _ueditorOptions;
            }
            set
            {
                _ueditorOptions = value;
            }
        }

        #endregion UEditorOptions

        #region Cors configs

        private Cors _cors;

        /// <summary>
        ///  Cors configs
        /// </summary>
        public Cors CorsOptions
        {
            get
            {
                if (_cors == null)
                {
                    _cors = new Cors();
                    _cors.Policy = new List<CorsPolicy>();
                }
                return _cors;
            }
            set
            {
                _cors = value;
            }
        }

        #endregion Cors configs

        #region Support Languages

        private string _languages;

        /// <summary>
        /// Support Languages
        /// </summary>
        public string Languages
        {
            get
            {
                if (string.IsNullOrEmpty((_languages)))
                {
                    _languages = "zh";
                }
                return _languages;
            }
            set
            {
                _languages = value;
            }
        }

        private List<CultureInfo> _supportLanguages;

        public List<CultureInfo> SupportLanguages
        {
            get
            {
                if (_supportLanguages == null)
                {
                    _supportLanguages = new List<CultureInfo>();
                    var lans = Languages.Split(",");
                    foreach (var lan in lans)
                    {
                        _supportLanguages.Add(new CultureInfo(lan));
                    }
                }
                return _supportLanguages;
            }
        }

        #endregion Support Languages

        public string HostRoot { get; set; } = "";

        #region CookieOption configs

        private CookieOption _cookieOption;

        /// <summary>
        ///  Cors configs
        /// </summary>
        public CookieOption CookieOptions
        {
            get
            {
                if (_cookieOption == null)
                {
                    _cookieOption = new CookieOption();
                }
                return _cookieOption;
            }
            set
            {
                _cookieOption = value;
            }
        }

        #endregion CookieOption configs

        #region JwtOption configs

        private JwtOption _jwtOption;

        /// <summary>
        ///  Cors configs
        /// </summary>
        public JwtOption JwtOptions
        {
            get
            {
                if (_jwtOption == null)
                {
                    _jwtOption = new JwtOption();
                }
                return _jwtOption;
            }
            set
            {
                _jwtOption = value;
            }
        }

        #endregion JwtOption configs
    }
}