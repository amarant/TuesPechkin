using System;
using System.Collections.Generic;
using System.Text;

namespace TuesPechkin
{
    internal interface IHtmlToSomethingDocument
    {
        List<ObjectSettings> Objects { get; }
        GlobalSettings GlobalSettings { get; set; }
        void ApplyToConverter(out IntPtr converter);
    }
}
