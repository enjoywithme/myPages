using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace mySharedLib
{
    public static class Utility
    {
        private static string[] _badWords;
        private static string[] _badNames;
        public static string[] _btItemPrefix;

        private static string _currentPath;

        public static void UtilityInit(string currentPath)
        {
            _currentPath = currentPath;
            var badWordsFilePath = Path.Combine(_currentPath, "BAD_WORDS.txt");
            _badWords = File.Exists(badWordsFilePath) ? File.ReadAllLines(badWordsFilePath) : null;

            var badNamesFilePath = Path.Combine(_currentPath, "BAD_NAMES.txt");
            _badNames = File.Exists(badWordsFilePath) ? File.ReadAllLines(badNamesFilePath) : null;

            _btItemPrefix = null;
            var btPrefixFile = Path.Combine(_currentPath, "BtItemPrefix.txt");
            if (File.Exists(btPrefixFile))
            {
                _btItemPrefix = File.ReadAllLines(btPrefixFile);
            }
        }

        //标准化文件名
        public static string MakeValidFileName(this string name)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidRegStr = $@"([{invalidChars}]*\.+$)|([{invalidChars}]+)";

            return Regex.Replace(name, invalidRegStr, "_");
        }

        //延迟执行某个操作
        //https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program
        //Note Forms.Timer and Timer() have similar implementations. 
        public static void DelayAction(int millisecond, Action action)
        {
            var timer = new Timer();;
            timer.Tick += delegate

            {
                timer.Stop();
                action.Invoke();
            };

            timer.Interval = millisecond;
            timer.Start();
        }

        //从app.config中获取设置项
        public static T GetSetting<T>(string key, T defaultValue = default(T)) where T : IConvertible
        {
            var val = ConfigurationManager.AppSettings[key] ?? "";
            var result = defaultValue;
            if (string.IsNullOrEmpty(val)) return result;
            var typeDefault = default(T);
            if (typeof(T) == typeof(string))
            {
                typeDefault = (T)(object)string.Empty;
            }
            result = (T)Convert.ChangeType(val, typeDefault.GetTypeCode());
            return result;
        }

        //保存配置到app.config https://www.codeproject.com/Articles/14744/Read-Write-App-Config-File-with-NET-2-0?display=Print
        public static void SaveSetting(string key, string value)
        {
            // Open App.Config of executable
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings.AllKeys.Contains(key))
                config.AppSettings.Settings[key].Value = value;
            else
                // Add an Application Setting.
                config.AppSettings.Settings.Add(key,value);

            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }

        //清洗字符串
        public static string Purify(this string text,string replace=" ")
        {
            if(_badWords==null || _badWords.Length==0) return text;
            {
                foreach (var badWord in _badWords)
                {
                    if (string.IsNullOrEmpty(badWord.Trim())) continue;

                    var splits = badWord.Split();
                    switch (splits.Length)
                    {
                        case 0:
                           continue;//只能是1或者2
                        case 2:
                            replace = splits[1];
                            break;
                    }

                    if (splits[0].StartsWith("regx:"))
                    {
                        splits[0] = splits[0].Replace("regx:", "");
                        text =  Regex.Replace(text, splits[0], replace, RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        text = text.Replace(splits[0], replace);
                    }
                }
            }
            //var cleaned = Regex.Replace(text, "\\b" + String.Join("\\b|\\b", PRE_CLEARS) + "\\b", "", RegexOptions.IgnoreCase);
            //cleaned = BAD_CHARS.Replace(cleaned, " ");
            //cleaned = Regex.Replace(cleaned, "\\b" + String.Join("\\b|\\b", BAD_WORDS) + "\\b", "", RegexOptions.IgnoreCase);
            //cleaned = Regex.Replace(cleaned, "\\b" + String.Join("\\b|\\b", RELEASE_GROUPS) + "\\b", "", RegexOptions.IgnoreCase);
            //替换多个空格为一个空格 https://stackoverflow.com/questions/1615559/convert-a-unicode-string-to-an-escaped-ascii-string
            text = Regex.Replace(text, "[ ]{2,}"," ");
            return text.Trim();
        }

        //清洗文件名
        public static string PurifyName(this string text, string replace = "")
        {
            if (_badNames == null || _badNames.Length == 0) return text;
            {
                foreach (var badName in _badNames)
                {
                    if (string.IsNullOrEmpty(badName.Trim())) continue;

                    var splits = badName.Split();
                    switch (splits.Length)
                    {
                        case 0:
                            continue;//只能是1或者2
                        case 2:
                            replace = splits[1];
                            break;
                    }

                    if (splits[0].StartsWith("regx:"))
                    {
                        splits[0] = splits[0].Replace("regx:", "");
                        text = Regex.Replace(text, splits[0], replace, RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        text = text.Replace(splits[0], replace);
                    }
                }
            }
            return text.Trim();
        }

        //当前执行程序集目录
        public static string ExecutingAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        //标准化最终的种子文件名

        public static string NormalizeTorrentFileName(this string fileName)
        {
            fileName = Regex.Replace(fileName, @"\s+", ".");//替换空白为.
            if (fileName.StartsWith("[")) return fileName;
            var match = Regex.Match(fileName, "(^[^\\.]*?[\\u4e00-\\u9fa5：·]+\\d*)\\.");
            if (!match.Success) return fileName;
            var i = match.Groups[1].Length;
            return $"[{fileName.Substring(0, i)}]{fileName.Substring(i, fileName.Length - i)}";

        }

        //去除非法文件名
        public static string SanitizeFileName(this string fileName)
        {
            fileName = fileName.Replace("/", " ");
            fileName = fileName.Replace(":", "：");

            var invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            return invalid.Aggregate(fileName, (current, c) => current.Replace(c.ToString(), ""));
        }

        //抽取空格隔开的第一个字节
        public static string ExtractFirstToken(this string text)
        {
            var match = Regex.Match(text, "^(.+?)\\s");
            return match.Success ? match.Groups[1].Value : "";
        }

        //查找字符串的第一个年份
        public static int ExtractYear(this string text)
        {
            var match = Regex.Match(text, "(\\d{4})");
            var year =  !match.Success ? 0 : (from Group matchGroup in match.Groups select int.Parse(matchGroup.Value)).FirstOrDefault(d => d != 1080 && d != 2160);
            return year is < 1900 or >= 3000 ? 0 : year;
        }

        //Convert a Unicode string to an escaped ASCII string
        //https://stackoverflow.com/questions/1615559/convert-a-unicode-string-to-an-escaped-ascii-string
        public static string EncodeNonAsciiCharacters(this string value)
        {
            var sb = new StringBuilder();
            foreach (var c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    var encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string DecodeEncodedNonAsciiCharacters(this string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m => ((char)Int32.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString());
        }

        /// <summary>
        /// 检查是否有效的图片连接地址，包含完整文件名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidateImageUrl(string url)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) return false;
            if(!url.StartsWith("http",StringComparison.InvariantCultureIgnoreCase)) return false;
            return true;
        }


        /// <summary>
        /// 格式化文件大小
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static string FormatFileSize(this long fileSize)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = fileSize;
            var order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            var result = $"{len:0.##} {sizes[order]}";
            return result;
        }

        public static string FormatModifiedDateTime(this DateTime modifiedDateTime)
        {
            return modifiedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ClearPathPrefix(this string path)
        {
            while (path.StartsWith("\\") || path.StartsWith("/"))
            {
                path = path[1..];
            }
            return path;    
        }
    }

    
}
