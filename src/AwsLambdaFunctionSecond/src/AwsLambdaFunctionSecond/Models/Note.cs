using System.Text;

namespace AwsLambdaFunctionSecond.Models
{
    public class Note
    {
        public string Title { get; set; } = "N/A";
        public string Description { get; set; } = "N/A";
        public string Author { get; set; } = "N/A";
        public DateTime CreatedDate { get; set; }
        public bool IsInEnglish { get; set; }
        public byte Priority { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Title: {Title}")
                .AppendLine($"Description: {Description}")
                .AppendLine($"Author: {Author}")
                .AppendLine($"CreatedDate: {CreatedDate.ToString("yyyy-MM-dd HH/mm/ss")}")
                .AppendLine($"IsInEnglish: {IsInEnglish}")
                .AppendLine($"Priority: {Priority}")
                .ToString();
        }
    }
}
