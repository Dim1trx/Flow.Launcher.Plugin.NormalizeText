using System;
using System.Collections.Generic;
using System.Globalization;

namespace Flow.Launcher.Plugin.NormalizeText
{
    public class NormalizeText : IPlugin
    {
        private PluginInitContext _context;
        private TextInfo _textInfo;

        public void Init(PluginInitContext context)
        {
            _context = context;
            _textInfo = CultureInfo.CurrentCulture.TextInfo;
        }

        public List<Result> Query(Query query)
        {
            try
            {
                var input = query.Search;
                var titleResult = _textInfo.ToTitleCase(query.Search.ToLowerInvariant());

                var result = new Result
                {
                    Title = "Normalize text",
                    SubTitle = "Result: " + titleResult,
                    Action = c =>
                    {
                        _context.API.CopyToClipboard(titleResult, showDefaultNotification: false);
                        return true;
                    },
                    IcoPath = "./Images/icon.png",
                };

                return new List<Result> { result };
            }
            catch (Exception ex)
            {
                _context.API.ShowMsgError("Error", ex.Message);
                return new List<Result> { };
            }
        }
    }
}