using System;
using System.Diagnostics;
using System.Drawing;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Diplom
{
    class TextWord
    {
        /*Создание заключения*/
        public static void CreateWordDesktop(Human human)
        {
            string fileNameAndPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\\{human.Surname}{human.Name.Substring(0, 1).ToUpper()}.{human.Patronymic.Substring(0, 1).ToUpper()}.docx".Replace("\\", "/");
            DocX doc = DocX.Create(fileNameAndPath);
            HeadlineText(doc, human);
            TopTextContent(doc);
            MiddleTextContent(HistoryForm.result, doc);
            FooterTextContent(HistoryForm.result, doc);
            FooterlineText(doc);
            doc.Save();
            Process.Start(fileNameAndPath);
        }
        /*Верхняя часть заключения*/
        private static void HeadlineText(DocX doc, Human human)
        {
            string headtext;
            headtext = HeadText(human);
            Paragraph headlineFormat = doc.InsertParagraph(headtext);
            headlineFormat.LineSpacingAfter = 10;
            headlineFormat.LineSpacing=13.8f;
            headlineFormat.Color(Color.Black);
            headlineFormat.Font("Times New Roman");
            headlineFormat.Alignment = Alignment.center;
            headlineFormat.Bold();
            headlineFormat.FontSize(14D);
        }
        /*Подвал заключения*/
        private static void FooterlineText(DocX doc)
        {
            string footerlineTextDate = $"{DateTime.Now.ToShortDateString()}";
            string footerlineTextPsychologist = "Психолог: Овчинникова Е. В.";
            Paragraph footerlineFormat = doc.InsertParagraph();
            footerlineFormat.LineSpacingAfter = 10;
            footerlineFormat.LineSpacing = 13.8f;
            footerlineFormat.Append($"\t{footerlineTextDate}").Bold().Append("\t");
            footerlineFormat.InsertText($"\t\t\t\t\t   { footerlineTextPsychologist}");
            footerlineFormat.Color(Color.Black);
            footerlineFormat.Font("Times New Roman");
            footerlineFormat.Alignment = Alignment.both;
            footerlineFormat.FontSize(12D);
        }
        /*Верхняя часть середины заключения*/
        private static void TopTextContent(DocX doc)
        {
            string topTextContent = "    Контакту доступен. Жалоб не предъявляет. Фон настроения в беседе и при прохождении исследования устойчив.";
            Paragraph topTextContentP = doc.InsertParagraph();
            topTextContentP.Append($"{topTextContent}");
            topTextContentP.LineSpacingAfter = 10;
            topTextContentP.LineSpacing = 13.8f;
            topTextContentP.Color(Color.Black);
            topTextContentP.Font("Times New Roman");
            topTextContentP.Alignment = Alignment.both;
            topTextContentP.FontSize(12D);
        }
        /*Центр середины заключения*/
        private static void MiddleTextContent(string[] result, DocX doc)
        {
            string middleTextContent;
            string middleTextContentUp="";
            string middleTextContentTest;
            string resultScales;
            //Опросник исследования агрессии
            if (result.Length == 5)
            {
                string aggressivenessIndex = "";
                string cosAggressia = "";
                string negativizm = "";
                string hostilityIndex = "";
                string remorse = "";
                middleTextContentTest = "    При прохождении опросника уровня агрессивности";
                if (int.Parse(result[0]) <= 27)
                    aggressivenessIndex = " индекса агрессивности";
                if (int.Parse(result[1]) <= 14)
                    cosAggressia = " косвенной агрессии";
                if (int.Parse(result[2]) <= 30)
                    negativizm = " негативизма";
                if (int.Parse(result[3]) <= 14)
                    hostilityIndex = " индекса враждебности";
                if (int.Parse(result[4]) <= 30)
                    remorse = " чувства вины";
                if ((int.Parse(result[0]) > 27) && (int.Parse(result[1]) > 14) && (int.Parse(result[2]) > 30) && (int.Parse(result[3]) > 14) && (int.Parse(result[4]) > 30))
                    middleTextContent = " получены достоверные данные. По результатам опросника низкого уровня не выявлено.";
                else
                {
                    resultScales = $" {aggressivenessIndex}, {cosAggressia}, {negativizm}, {hostilityIndex}, {remorse}".Replace(" ,", "").Replace("  ", " ");
                    middleTextContent = $" получены достоверные данные. По результатам низкий уровень выявлен для{resultScales}.".Replace(", .", ".");
                }
                if (int.Parse(result[0]) >= 50)
                    aggressivenessIndex = " индекса агрессивности";
                if (int.Parse(result[1]) >= 37)
                    cosAggressia = " косвенной агрессии";
                if (int.Parse(result[2]) >= 53)
                    negativizm = " негативизма";
                if (int.Parse(result[3]) >= 37)
                    hostilityIndex = " индекса враждебности";
                if (int.Parse(result[4]) >= 53)
                    remorse = " чувства вины";
                if((int.Parse(result[0]) >= 50) || (int.Parse(result[1]) >= 37) || (int.Parse(result[2]) >= 53) || (int.Parse(result[3]) >= 37) || (int.Parse(result[4]) >= 53))
                {
                    resultScales = $" {aggressivenessIndex}, {cosAggressia}, {negativizm}, {hostilityIndex}, {remorse}".Replace(" ,", "").Replace("  ", " ");
                    middleTextContentUp = $" Повышенный уровень выявлен для{resultScales}.".Replace(", .", ".");
                }
                Paragraph middleTextContentP = doc.InsertParagraph();
                middleTextContentP.Append($"{middleTextContentTest}").Bold().Append(",");
                middleTextContentP.InsertText($"{middleTextContent}{middleTextContentUp}");
                middleTextContentP.LineSpacingAfter = 10;
                middleTextContentP.LineSpacing = 13.8f;
                middleTextContentP.Color(Color.Black);
                middleTextContentP.Font("Times New Roman");
                middleTextContentP.Alignment = Alignment.both;
                middleTextContentP.FontSize(12D);
            }
            //Характерологический опросник
            if (result.Length == 10)
            {
                string hypertimal = "";
                string excitable = "";
                string emotional = "";
                string pedantic = "";
                string disturbing = "";
                string cyclotic = "";
                string demonstrative = "";
                string unbalanced = "";
                string dysthymal = "";
                string exalted = "";
                middleTextContentTest = "    При прохождении характерологического опросника";
                if (int.Parse(result[0]) >= 13)
                    hypertimal = " гипертимости";
                if (int.Parse(result[1]) >= 13)
                    excitable = " возбудимости";
                if (int.Parse(result[2]) >= 13)
                    emotional = " эмотивности";
                if (int.Parse(result[3]) >= 13)
                    pedantic = " педантичности";
                if (int.Parse(result[4]) >= 13)
                    disturbing = " тревожности";
                if (int.Parse(result[5]) >= 13)
                    cyclotic = " циклотивности";
                if (int.Parse(result[6]) >= 13)
                    demonstrative = " демонстративности";
                if (int.Parse(result[7]) >= 13)
                    unbalanced = " неуравновешенности";
                if (int.Parse(result[8]) >= 13)
                    dysthymal = " дистимности";
                if (int.Parse(result[9]) >= 13)
                    exalted = " экзальтированности";
                if ((int.Parse(result[0]) < 13) && (int.Parse(result[1]) < 13) && (int.Parse(result[2]) < 13) && (int.Parse(result[3]) < 13) && (int.Parse(result[4]) < 13)
                    && (int.Parse(result[5]) < 13) && (int.Parse(result[6]) < 13) && (int.Parse(result[7]) < 13) && (int.Parse(result[8]) < 13) && (int.Parse(result[9]) < 13))
                    middleTextContent = " получены достоверные данные. По результатам опросника высоких значений не выявлено.";
                else
                {
                    resultScales = $" {hypertimal}, {excitable}, {emotional}, {pedantic}, {disturbing}, {cyclotic}, {demonstrative}, {unbalanced}, {dysthymal}, {exalted}".Replace(" ,", "").Replace("  ", " ");
                    middleTextContent = $" получены достоверные данные. По результатам значения выше среднего выявлены для{resultScales}.".Replace(", .", ".");
                }
                Paragraph middleTextContentP = doc.InsertParagraph();
                middleTextContentP.Append($"{middleTextContentTest}").Bold().Append(",");
                middleTextContentP.InsertText($"{middleTextContent}");
                middleTextContentP.Color(Color.Black);
                middleTextContentP.LineSpacingAfter = 10;
                middleTextContentP.LineSpacing = 13.8f;
                middleTextContentP.Font("Times New Roman");
                middleTextContentP.Alignment = Alignment.both;
                middleTextContentP.FontSize(12D);
            }
        }
        /*Нижняя часть середины заключения*/
        private static void FooterTextContent(string[] result, DocX doc)
        {
            string footerTextContent;
            string resultScales;
            //Опросник исследования агрессии
            if (result.Length == 5)
            {
                string aggressivenessIndex = "";
                string cosAggressia = "";
                string negativizm = "";
                string hostilityIndex = "";
                string remorse = "";
                if (int.Parse(result[0]) <= 27)
                    aggressivenessIndex = " низкий уровень агрессивности";
                else if (int.Parse(result[0]) >= 50)
                    aggressivenessIndex = " повышенный уровень агрессивности";
                if (int.Parse(result[1]) <= 14)
                    cosAggressia = " низкий уровень агрессивного поведения, направленного против какого-то человека";
                else if (int.Parse(result[1]) >= 37)
                    cosAggressia = " повышенный уровень агрессивного поведения, направленного против какого-то человека";
                if (int.Parse(result[2]) <= 30)
                    negativizm = "  низкий уровень оппозиционной манеры поведения";
                else if(int.Parse(result[2]) >= 53)
                    negativizm = "  повышенный уровень оппозиционной манеры поведения";
                if (int.Parse(result[3]) <= 14)
                    hostilityIndex = " низкий уровень враждебности";
                else if(int.Parse(result[3]) >= 37)
                    hostilityIndex = " повышенный уровень враждебности";
                if (int.Parse(result[4]) <= 30)
                    remorse = " низкий уровень зависти";
                else if (int.Parse(result[4]) >= 53)
                    negativizm = "  повышенный уровень зависти";
                resultScales = $" {aggressivenessIndex}, {cosAggressia}, {negativizm}, {hostilityIndex}, {remorse}".Replace(" ,", "").Replace("  ", " ");
                footerTextContent = $"    По данным опросника у испытуемого можно предположить следующие индивидуально – психологические особенности:{resultScales}.".Replace(", .", ".");
                Paragraph middleTextContentP = doc.InsertParagraph();
                middleTextContentP.InsertText($"{footerTextContent}");
                middleTextContentP.Color(Color.Black);
                middleTextContentP.LineSpacingAfter = 10;
                middleTextContentP.LineSpacing = 13.8f;
                middleTextContentP.Font("Times New Roman");
                middleTextContentP.Alignment = Alignment.both;
                middleTextContentP.FontSize(12D);
            }
            //Характерологический опросник
            if (result.Length == 10)
            {
                string hypertimal = "";
                string excitable = "";
                string emotional = "";
                string pedantic = "";
                string disturbing = "";
                string cyclotic = "";
                string demonstrative = "";
                string unbalanced = "";
                string dysthymal = "";
                string exalted = "";
                if (int.Parse(result[0]) >= 13)
                    hypertimal = " повышенный фон настроения в сочетании с жаждой деятельности, высокой активностью, предприимчивостью";
                else if (int.Parse(result[0]) < 13)
                    excitable = " умеренную активность";
                if (int.Parse(result[1]) >= 13)
                    excitable = " повышенную импульсивность";
                else if (int.Parse(result[1]) < 13)
                    excitable = " умеренную импульсивность";
                if (int.Parse(result[2]) >= 13)
                    emotional = " глубокие переживания в области тонких эмоций в духовной жизни человека";
                else if (int.Parse(result[2]) < 13)
                    excitable = " умеренную эмоциональность";
                if (int.Parse(result[3]) >= 13)
                    pedantic = " долгие переживания травмирующих событий";
                if (int.Parse(result[4]) >= 13)
                    disturbing = " повышенную робость и пугливость, высокий уровень тревожности";
                else if (int.Parse(result[4]) < 13)
                    disturbing = " умеренную тревожность";
                if (int.Parse(result[5]) >= 13)
                    cyclotic = " частые периодические смены настроения, а также зависимость от внешних событий";
                if (int.Parse(result[6]) >= 13)
                    demonstrative = " повышенную способность к вытеснению, демонстративности поведения";
                else if (int.Parse(result[6]) < 13)
                    excitable = " умеренную демонстративность поведения";
                if (int.Parse(result[7]) >= 13)
                    unbalanced = " склонность к нравоучениям, неразговорчивость";
                if (int.Parse(result[8]) >= 13)
                    dysthymal = " фиксацию на мрачных сторонах жизни";
                if (int.Parse(result[9]) >= 13)
                    exalted = " большой диапазон эмоциональных состояний";
                resultScales = $" {hypertimal}, {excitable}, {emotional}, {pedantic}, {disturbing}, {cyclotic}, {demonstrative}, {unbalanced}, {dysthymal}, {exalted}".Replace(" ,", "").Replace("  ", " ");
                footerTextContent = $"    По данным опросника у испытуемого можно предположить следующие индивидуально – психологические особенности:{resultScales}.".Replace(", .", ".");
                Paragraph middleTextContentP = doc.InsertParagraph();
                middleTextContentP.InsertText($"{footerTextContent}");
                middleTextContentP.Color(Color.Black);
                middleTextContentP.LineSpacingAfter = 10;
                middleTextContentP.LineSpacing = 13.8f;
                middleTextContentP.Font("Times New Roman");
                middleTextContentP.Alignment = Alignment.both;
                middleTextContentP.FontSize(12D);
            }
        }
        /*Верхняя часть заключения*/
        private static string HeadText(Human human)
        {
            string last = human.Surname.Remove(0, human.Surname.Length - 1);
            string lastTwo = human.Surname.Remove(0, human.Surname.Length - 2);
            if (human.Gender == "мужской")
            {
                if (last.Equals("а") || (lastTwo == "их") || (lastTwo == "ых") || last.Equals("е")
                    || last.Equals("и") || last.Equals("о") || last.Equals("у") || last.Equals("ы")
                    || last.Equals("э") || last.Equals("ю") || last.Equals("ь") || last.Equals("ъ"))
                    return $"Заключение по данным экспериментально-психологического исследования {human.Surname} {human.Name.Substring(0, 1).ToUpper()}. {human.Patronymic.Substring(0, 1).ToUpper()}. {human.DateOfBirth.Year} г. р. ";
                else if (lastTwo == "ий")
                    return $"Заключение по данным экспериментально-психологического исследования {human.Surname.Replace(lastTwo, "ого")} {human.Name.Substring(0, 1).ToUpper()}. {human.Patronymic.Substring(0, 1).ToUpper()}. {human.DateOfBirth.Year} г. р. ";
                else if (last.Equals("й"))
                    return $"Заключение по данным экспериментально-психологического исследования {human.Surname.Replace(last, "я")} {human.Name.Substring(0, 1).ToUpper()}. {human.Patronymic.Substring(0, 1).ToUpper()}. {human.DateOfBirth.Year} г. р. ";
                else
                    return $"Заключение по данным экспериментально-психологического исследования {human.Surname}а {human.Name.Substring(0, 1).ToUpper()}. {human.Patronymic.Substring(0, 1).ToUpper()}. {human.DateOfBirth.Year} г. р. ";
            }
            else
            {
                if (last.Equals("а") || (lastTwo == "их") || (lastTwo == "ых") || last.Equals("е")
                    || last.Equals("и") || last.Equals("о") || last.Equals("у") || last.Equals("ы")
                    || last.Equals("э") || last.Equals("ю") || last.Equals("б")
                    || last.Equals("в") || last.Equals("г") || last.Equals("д")
                    || last.Equals("ж") || last.Equals("з") || last.Equals("й")
                    || last.Equals("к") || last.Equals("л") || last.Equals("м") || last.Equals("н")
                    || last.Equals("п") || last.Equals("р") || last.Equals("с")
                    || last.Equals("т") || last.Equals("ф") || last.Equals("ц") || last.Equals("ч")
                    || last.Equals("ш") || last.Equals("ц") || last.Equals("ь") || last.Equals("ъ"))
                    return $"Заключение по данным экспериментально-психологического исследования {human.Surname} {human.Name.Substring(0, 1).ToUpper()}. {human.Patronymic.Substring(0, 1).ToUpper()}. {human.DateOfBirth.Year} г. р. ";
                else if (lastTwo == "ая")
                    return $"Заключение по данным экспериментально-психологического исследования {human.Surname.Replace(lastTwo, "ой")} {human.Name.Substring(0, 1).ToUpper()}. {human.Patronymic.Substring(0, 1).ToUpper()}. {human.DateOfBirth.Year} г. р. ";
                else
                    return $"Заключение по данным экспериментально-психологического исследования {human.Surname}а {human.Name.Substring(0, 1).ToUpper()}. {human.Patronymic.Substring(0, 1).ToUpper()}. {human.DateOfBirth.Year} г. р. ";
            }
        }
    }
}
