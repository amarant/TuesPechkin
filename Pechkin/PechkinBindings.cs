using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using TuesPechkin.Properties;
using TuesPechkin.Util;

namespace TuesPechkin
{
    [Serializable]
    internal static class PechkinBindings
    {
        public static string TocXslFilename { get; private set; }

        static PechkinBindings()
        {
            var raw = (IntPtr.Size == 8) ? Resources.wkhtmltox_64_dll : Resources.wkhtmltox_32_dll;

            SetupUnmanagedAssembly("wkhtmltox.dll", raw);
        }

        /// <summary>
        /// CAPI(void) wkhtmltopdf_add_object(wkhtmltopdf_converter * converter, wkhtmltopdf_object_settings * setting, const char * data);
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_add_object(IntPtr converter, IntPtr objectSettings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String data);

        /// <summary>
        /// CAPI(void) wkhtmltopdf_add_object(wkhtmltopdf_converter * converter, wkhtmltopdf_object_settings * setting, const char * data);
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_add_object(IntPtr converter, IntPtr objectSettings, byte[] data);

        /// <summary>
        /// CAPI(int) wkhtmltopdf_convert(wkhtmltopdf_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_convert(IntPtr converter);

        /// <summary>
        /// CAPI(int) wkhtmltoimage_convert(wkhtmltoimage_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_convert(IntPtr converter);

        /// <summary>
        /// CAPI(wkhtmltopdf_converter *) wkhtmltopdf_create_converter(wkhtmltopdf_global_settings * settings)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_create_converter(IntPtr globalSettings);

        /// <summary>
        /// CAPI(wkhtmltoimage_converter *) wkhtmltoimage_create_converter(wkhtmltoimage_global_settings * settings, const char * data)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltoimage_create_converter(IntPtr globalSettings, byte[] data);

        /// <summary>
        /// CAPI(wkhtmltopdf_global_settings *) wkhtmltopdf_create_global_settings()
        /// </summary>
        /// <returns></returns>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_create_global_settings();

        /// <summary>
        /// CAPI(wkhtmltoimage_global_settings *) wkhtmltoimage_create_global_settings()
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltoimage_create_global_settings();

        /// <summary>
        /// CAPI(wkhtmltopdf_object_settings *) wkhtmltopdf_create_object_settings()
        /// </summary>
        /// <returns></returns>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_create_object_settings();

        /// <summary>
        /// CAPI(int) wkhtmltopdf_current_phase(wkhtmltopdf_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_current_phase(IntPtr converter);

        /// <summary>
        /// CAPI(int) wkhtmltoimage_current_phase(wkhtmltoimage_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_current_phase(IntPtr converter);

        /// <summary>
        /// CAPI(int) wkhtmltopdf_deinit()
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_deinit();

        /// <summary>
        /// CAPI(int) wkhtmltoimage_deinit()
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_deinit();

        /// <summary>
        /// CAPI(void) wkhtmltopdf_destroy_converter(wkhtmltopdf_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_destroy_converter(IntPtr converter);

        /// <summary>
        /// CAPI(void) wkhtmltoimage_destroy_converter(wkhtmltoimage_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_destroy_converter(IntPtr converter);

        /// <summary>
        /// CAPI(int) wkhtmltopdf_extended_qt()
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_extended_qt();

        /// <summary>
        /// CAPI(int) wkhtmltoimage_extended_qt()
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_extended_qt();

        /// <summary>
        /// CAPI(int) wkhtmltopdf_get_global_setting(wkhtmltopdf_global_settings * settings, const char * name, char * value, int vs)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_get_global_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [In]
            [Out]
            ref byte[] value, int valueSize);

        /// <summary>
        /// CAPI(int) wkhtmltoimage_get_global_setting(wkhtmltoimage_global_settings * settings, const char * name, char * value, int vs)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_get_global_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [In]
            [Out]
            ref byte[] value, int valueSize);

        /// <summary>
        /// CAPI(int) wkhtmltopdf_get_object_setting(wkhtmltopdf_object_settings * settings, const char * name, char * value, int vs)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_get_object_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [In]
            [Out]
            ref byte[] value, int vs);


        /// <summary>
        /// CAPI(long) wkhtmltopdf_get_output(wkhtmltopdf_converter * converter, const unsigned char **)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_get_output(IntPtr converter, out IntPtr data);

        /// <summary>
        /// CAPI(long) wkhtmltoimage_get_output(wkhtmltoimage_converter * converter, const unsigned char **)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_get_output(IntPtr converter, out IntPtr data);

        /// <summary>
        /// CAPI(int) wkhtmltopdf_http_error_code(wkhtmltopdf_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_http_error_code(IntPtr converter);

        /// <summary>
        /// CAPI(int) wkhtmltoimage_http_error_code(wkhtmltoimage_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_http_error_code(IntPtr converter);

        /// <summary>
        /// CAPI(int) wkhtmltopdf_init(int use_graphics);
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_init(int useGraphics);

        /// <summary>
        /// CAPI(int) wkhtmltoimage_init(int use_graphics)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_init(int useGraphics);

        /// <summary>
        /// CAPI(int) wkhtmltopdf_phase_count(wkhtmltopdf_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_phase_count(IntPtr converter);

        /// <summary>
        /// CAPI(int) wkhtmltoimage_phase_count(wkhtmltoimage_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_phase_count(IntPtr converter);

        /// <summary>
        /// CAPI(const char *) wkhtmltopdf_phase_description(wkhtmltopdf_converter * converter, int phase)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_phase_description(IntPtr converter, int phase);

        /// <summary>
        /// CAPI(const char *) wkhtmltoimage_phase_description(wkhtmltoimage_converter * converter, int phase)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltoimage_phase_description(IntPtr converter, int phase);

        /// <summary>
        /// CAPI(const char *) wkhtmltopdf_progress_string(wkhtmltopdf_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_progress_string(IntPtr converter);

        /// <summary>
        /// CAPI(const char *) wkhtmltoimage_progress_string(wkhtmltoimage_converter * converter)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltoimage_progress_string(IntPtr converter);

        /// <summary>
        /// CAPI(void) wkhtmltopdf_set_error_callback(wkhtmltopdf_converter * converter, wkhtmltopdf_str_callback cb)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_error_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                 StringCallback callback);

        /// <summary>
        /// CAPI(void) wkhtmltoimage_set_error_callback(wkhtmltoimage_converter * converter, wkhtmltoimage_str_callback cb)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_error_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                 StringCallback callback);

        /// <summary>
        /// CAPI(void) wkhtmltopdf_set_finished_callback(wkhtmltopdf_converter * converter, wkhtmltopdf_int_callback cb)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_finished_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                    IntCallback callback);

        /// <summary>
        /// CAPI(void) wkhtmltoimage_set_finished_callback(wkhtmltoimage_converter * converter, wkhtmltoimage_int_callback cb);
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_finished_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                    IntCallback callback);

        /// <summary>
        /// CAPI(int) wkhtmltopdf_set_global_setting(wkhtmltopdf_global_settings * settings, const char * name, const char * value)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_set_global_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String value);

        /// <summary>
        /// CAPI(int) wkhtmltoimage_set_global_setting(wkhtmltoimage_global_settings * settings, const char * name, const char * value)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_set_global_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String value);

        /// <summary>
        /// CAPI(int) wkhtmltopdf_set_object_setting(wkhtmltopdf_object_settings * settings, const char * name, const char * value)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_set_object_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String value);

        /// <summary>
        /// CAPI(void) wkhtmltopdf_set_phase_changed_callback(wkhtmltopdf_converter * converter, wkhtmltopdf_void_callback cb)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_phase_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                         VoidCallback callback);
        /// <summary>
        /// CAPI(void) wkhtmltoimage_set_phase_changed_callback(wkhtmltoimage_converter * converter, wkhtmltoimage_void_callback cb)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_phase_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                         VoidCallback callback);

        /// <summary>
        /// CAPI(void) wkhtmltopdf_set_progress_changed_callback(wkhtmltopdf_converter * converter, wkhtmltopdf_int_callback cb)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_progress_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                            IntCallback callback);

        /// <summary>
        /// CAPI(void) wkhtmltoimage_set_progress_changed_callback(wkhtmltoimage_converter * converter, wkhtmltoimage_int_callback cb)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_progress_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                            IntCallback callback);

        /// <summary>
        /// CAPI(void) wkhtmltopdf_set_warning_callback(wkhtmltopdf_converter * converter, wkhtmltopdf_str_callback cb)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_warning_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                   StringCallback callback);

        /// <summary>
        /// CAPI(void) wkhtmltoimage_set_warning_callback(wkhtmltoimage_converter * converter, wkhtmltoimage_str_callback cb)
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_warning_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                   StringCallback callback);

        /// <summary>
        /// CAPI(const char *) wkhtmltopdf_version();
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern String wkhtmltopdf_version();

        /// <summary>
        /// CAPI(const char *)wkhtmltoimage_version()
        /// </summary>
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern String wkhtmltoimage_version();

        private static void SetupUnmanagedAssembly(string fileName, byte[] assemblyRaw)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var basePath = Path.Combine(
                Path.GetTempPath(),
                String.Format(
                    "{0}{1}_{2}_{3}",
                    assemblyName.Name.ToString(),
                    assemblyName.Version.ToString(),
                    IntPtr.Size == 8 ? "x64" : "x86",
                    String.Join(String.Empty, AppDomain.CurrentDomain.BaseDirectory.Split(Path.GetInvalidFileNameChars()))));

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            fileName = Path.Combine(basePath, fileName);

            WriteStreamToFile(fileName, () => new GZipStream(new MemoryStream(assemblyRaw), CompressionMode.Decompress));

            TocXslFilename = Path.Combine(basePath, "toc.xsl");

            WriteStreamToFile(TocXslFilename, () => new MemoryStream(Resources.toc));

            WinApiHelper.LoadLibrary(fileName);
        }

        private static void WriteStreamToFile(string fileName, Func<Stream> streamFactory)
        {
            if (!File.Exists(fileName))
            {
                var stream = streamFactory();
                var writeBuffer = new byte[8192];
                var writeLength = 0;

                using (var newFile = File.Open(fileName, FileMode.Create))
                {
                    while ((writeLength = stream.Read(writeBuffer, 0, writeBuffer.Length)) > 0)
                    {
                        newFile.Write(writeBuffer, 0, writeLength);
                    }
                }
            }
        }
    }
}