using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySharedLib
{
    public class FuncResult
    {
        public bool Ret { get; private set; } = true;
        private readonly StringBuilder _stringBuilder = new();

        public int ErrorCount { get; private set; }
        public string Message => _stringBuilder.ToString().TrimEnd();

        public static implicit operator bool(FuncResult d) => d.Ret;
        public void Set(bool ret, string msg)
        {
            Ret = Ret && ret;
            if (!ret) ErrorCount++;
            _stringBuilder.AppendLine(msg);
        }

        public void False(string msg)
        {
            Set(false, msg);
        }
    }
}
