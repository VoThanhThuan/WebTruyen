using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Service
{
    public class TextService
    {
        public string RemoveSpaces(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            while (text.Contains("  "))
                text = text.Replace("  ", " ");
            return text;
        }
    }
}
