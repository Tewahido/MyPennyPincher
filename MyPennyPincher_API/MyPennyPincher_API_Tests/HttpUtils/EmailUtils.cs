using System.Text;

namespace MyPennyPincher_API_Tests.HttpUtils;

public class EmailUtils
{
    public static string DecodeQuotedPrintable(string input)
    {
        var output = new MemoryStream();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '=')
            {
                if (i + 2 < input.Length && input[i + 1] != '\r' && input[i + 1] != '\n')
                {
                    string hex = input.Substring(i + 1, 2);
                    output.WriteByte(Convert.ToByte(hex, 16));
                    i += 2;
                }
                else
                {
                    // soft line break (e.g. "=\r\n")
                    while (i + 1 < input.Length && (input[i + 1] == '\r' || input[i + 1] == '\n'))
                        i++;
                }
            }
            else
            {
                output.WriteByte((byte)input[i]);
            }
        }
        return Encoding.UTF8.GetString(output.ToArray());
    }

}
