using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using TuesPechkin.Util;

namespace TuesPechkin
{
    /// <summary>
    /// Static class with utility methods for the interface.
    /// Acts mostly as a facade over PechkinBindings with log tracing.
    /// </summary>
    [Serializable]
    internal static class ImageStatic
    {
        public static IntPtr CreateGlobalSetting()
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Creating global settings (wkhtmltoimage_create_global_settings)");

            return PechkinBindings.wkhtmltoimage_create_global_settings();
        }

        public static int SetGlobalSetting(IntPtr setting, string name, string value)
        {
            Tracer.Trace(
                String.Format(
                    "T:{0} Setting global setting '{1}' to '{2}' for config {3}",
                    Thread.CurrentThread.Name,
                    name,
                    value,
                    setting));

            return PechkinBindings.wkhtmltoimage_set_global_setting(setting, name, value);
        }

        public static string GetGlobalSetting(IntPtr setting, string name)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Getting global setting (wkhtmltoimage_get_global_setting)");

            byte[] buf = new byte[2048];

            PechkinBindings.wkhtmltoimage_get_global_setting(setting, name, ref buf, buf.Length);

            int walk = 0;
            while (walk < buf.Length && buf[walk] != 0)
            {
                walk++;
            }

            byte[] buf2 = new byte[walk];
            Array.Copy(buf, 0, buf2, 0, walk);

            return Encoding.UTF8.GetString(buf2);
        }

        public static IntPtr CreateConverter(IntPtr globalSettings, byte[] data)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Creating converter (wkhtmltoimage_create_converter)");

            return PechkinBindings.wkhtmltoimage_create_converter(globalSettings, data);
        }

        public static void DestroyConverter(IntPtr converter)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Destroying converter (wkhtmltoimage_destroy_converter)");

            PechkinBindings.wkhtmltoimage_destroy_converter(converter);
        }

        public static void SetWarningCallback(IntPtr converter, StringCallback callback)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Setting warning callback (wkhtmltoimage_set_warning_callback)");
            
            PechkinBindings.wkhtmltoimage_set_warning_callback(converter, callback);
        }

        public static void SetErrorCallback(IntPtr converter, StringCallback callback)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Setting error callback (wkhtmltoimage_set_error_callback)");
            
            PechkinBindings.wkhtmltoimage_set_error_callback(converter, callback);
        }

        public static void SetFinishedCallback(IntPtr converter, IntCallback callback)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Setting finished callback (wkhtmltoimage_set_finished_callback)");

            PechkinBindings.wkhtmltoimage_set_finished_callback(converter, callback);
        }

        public static void SetPhaseChangedCallback(IntPtr converter, VoidCallback callback)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Setting phase change callback (wkhtmltoimage_set_phase_changed_callback)");

            PechkinBindings.wkhtmltoimage_set_phase_changed_callback(converter, callback);
        }

        public static void SetProgressChangedCallback(IntPtr converter, IntCallback callback)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Setting progress change callback (wkhtmltoimage_set_progress_changed_callback)");

            PechkinBindings.wkhtmltoimage_set_progress_changed_callback(converter, callback);
        }

        public static bool PerformConversion(IntPtr converter)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Starting conversion (wkhtmltoimage_convert)");

            return PechkinBindings.wkhtmltoimage_convert(converter) != 0;
        }

        public static int GetPhaseNumber(IntPtr converter)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Requesting current phase (wkhtmltoimage_current_phase)");

            return PechkinBindings.wkhtmltoimage_current_phase(converter);
        }

        public static int GetPhaseCount(IntPtr converter)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Requesting phase count (wkhtmltoimage_phase_count)");

            return PechkinBindings.wkhtmltoimage_phase_count(converter);
        }

        public static string GetPhaseDescription(IntPtr converter, int phase)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Requesting phase description (wkhtmltoimage_phase_description)");

            return Marshal.PtrToStringAnsi(PechkinBindings.wkhtmltoimage_phase_description(converter, phase));
        }

        public static string GetProgressDescription(IntPtr converter)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Requesting progress string (wkhtmltoimage_progress_string)");

            return Marshal.PtrToStringAnsi(PechkinBindings.wkhtmltoimage_progress_string(converter));
        }

        public static int GetHttpErrorCode(IntPtr converter)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Requesting http error code (wkhtmltoimage_http_error_code)");

            return PechkinBindings.wkhtmltoimage_http_error_code(converter);
        }

        public static byte[] GetConverterResult(IntPtr converter)
        {
            Tracer.Trace("T:" + Thread.CurrentThread.Name + " Requesting converter result (wkhtmltoimage_get_output)");

            IntPtr tmp;
            var len = PechkinBindings.wkhtmltoimage_get_output(converter, out tmp);
            var output = new byte[len];
            Marshal.Copy(tmp, output, 0, output.Length);
            return output;
        }
    }
}
