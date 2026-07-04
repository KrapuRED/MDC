using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueHighlighter : MonoBehaviour
{
    // Pola Regex untuk mendeteksi {teks}(warna)
    // \{([^}]+)\}  -> Menangkap teks di dalam kurung kurawal ke Group 1
    // \(([^)]+)\)  -> Menangkap teks warna di dalam kurung biasa ke Group 2
    private static readonly string pattern = @"\{([^}]+)\}\(([^)]+)\)";

    public static string ApplyHighlights(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        // Melakukan replace berdasarkan pattern Regex
        text = Regex.Replace(text, pattern, match =>
        {
            string cleanText = match.Groups[1].Value; // Isi dari {text}
            string colorName = match.Groups[2].Value.ToUpper().Trim(); // Isi dari (color)

            // Mengonversi string warna standar menjadi Hex HTML
            string colorHex = GetHexFromColorName(colorName);

            // Mengembalikan format Rich Text Unity
            return $"<color=#{colorHex}>{cleanText}</color>";
        });

        return text;
    }

    private static string GetHexFromColorName(string colorName)
    {
        // Default color jika warna tidak dikenali (Putih)
        Color finalColor = Color.white;

        // Cek warna standar Unity
        switch (colorName)
        {
            case "RED": finalColor = Color.red; break;
            case "GREEN": finalColor = Color.green; break;
            case "BLUE": finalColor = Color.blue; break;
            case "YELLOW": finalColor = Color.yellow; break;
            case "CYAN": finalColor = Color.cyan; break;
            case "MAGENTA": finalColor = Color.magenta; break;
            case "GRAY": case "GREY": finalColor = Color.gray; break;
            case "BLACK": finalColor = Color.black; break;
            default:
                // Jika user memasukkan Hex langsung seperti (FF0055), gunakan ColorUtility
                if (ColorUtility.TryParseHtmlString("#" + colorName, out Color customColor))
                {
                    finalColor = customColor;
                }
                else if (ColorUtility.TryParseHtmlString(colorName.ToLower(), out Color namedColor))
                {
                    // Mendukung warna HTML standard bawaan Unity seperti "orange", "purple", dll.
                    finalColor = namedColor;
                }
                break;
        }

        return ColorUtility.ToHtmlStringRGB(finalColor);
    }
}
