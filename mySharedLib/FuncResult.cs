using System.Text;

namespace mySharedLib
{
    public class FuncResult
    {
        public bool Success { get; private set; } = true;
        private readonly StringBuilder _stringBuilder = new();

        public int ErrorCount { get; private set; }
        public string Message => _stringBuilder.ToString().TrimEnd();

        public static implicit operator bool(FuncResult d) => d.Success;
        public void Set(bool ret, string msg)
        {
            Success = Success && ret;
            if (!ret) ErrorCount++;
            _stringBuilder.AppendLine(msg);
        }

        public void Log(string msg)
        {
            _stringBuilder.AppendLine(msg);
        }

        public void False(string msg)
        {
            Set(false, msg);
        }
    }
}
