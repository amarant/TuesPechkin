﻿using System;
using System.Collections.Generic;
namespace TuesPechkin
{
    [Serializable]
    public class HtmlToPdfDocument : IHtmlToSomethingDocument
    {
        public HtmlToPdfDocument()
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

            var config = PechkinStatic.CreateGlobalSetting();

            SettingApplicator.ApplySettings(config, this.global, true);

            converter = PechkinStatic.CreateConverter(config);

            //if (this.TableOfContents != null)
            //{
            //    this.TableOfContents.ApplyToConverter(converter);
            //}

            foreach (var setting in this.Objects)
            {
                if (setting != null)
                {
                    setting.ApplyToConverter(converter);
                }
            }
        }

        void IHtmlToSomethingDocument.ApplyToConverter(out IntPtr converter)
        {
            this.ApplyToConverter(out converter);
        }
    }
}