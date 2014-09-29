using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuesPechkin
{
    [Serializable]
    public class HtmlToImageDocument : IHtmlToSomethingDocument
    {
        public HtmlToImageDocument()
        {
            this.Objects = new List<ObjectSettings>();
        }

        private GlobalSettings global = new GlobalSettings();

        public List<ObjectSettings> Objects { get; private set; }

        //public TableOfContentsSettings TableOfContents { get; set; }

        public GlobalSettings GlobalSettings
        {
            get
            {
                return this.global;
            }
            set
            {
                this.AssertNotNull(value);
                this.global = value;
            }
        }

        private void AssertNotNull(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
        }

        internal void ApplyToConverter(out IntPtr converter)
        {
            converter = IntPtr.Zero;

            var config = ImageStatic.CreateGlobalSetting();

            SettingApplicator.ApplySettings(config, this.global, true);


            var setting = this.Objects.Single();
            if (setting != null)
            {
                setting.ApplyToConverter(converter);
                converter = ImageStatic.CreateConverter(config, setting.RawData);
            }
        }

        void IHtmlToSomethingDocument.ApplyToConverter(out IntPtr converter)
        {
            this.ApplyToConverter(out converter);
        }
    }
}
